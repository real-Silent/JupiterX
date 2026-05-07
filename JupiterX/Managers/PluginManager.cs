using MelonLoader;
using JupiterX.Menu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using JupiterX.Classes;
using JupiterX.Notifications;

namespace JupiterX.Managers
{
    public class PluginManager
    {
        public class Plugin
        {
            public string FileName;
            public bool Enabled;
            public string Name;
            public string Description;
            public Assembly Assembly;
            public List<MelonMod> Instances = new List<MelonMod>();
        }
        public interface IMenuExtension
        {
            ButtonInfo[] GetButtons();
        }
        public static readonly List<Plugin> Plugins = new List<Plugin>();
        private static string PluginsPath => Path.Combine(Application.persistentDataPath, "JupiterX/Plugins");
        public static void ReloadPlugins()
        {
            NotifiLib.SendNotification("Reloading plugins...", 500);
            Utility.SavePreferences();
            try
            {
                LoadPlugins();
                MelonLogger.Msg("[JupiterX] Reloaded plugins.");
            }
            catch (Exception e)
            {
                MelonLogger.Error("[JupiterX] Reload failed: " + e);
            }
            Utility.LoadPreferences();
            Buttons.CurrentCategoryName = "Main";
        }
        public static void LoadPlugins()
        {
            if (!Directory.Exists(PluginsPath))
                Directory.CreateDirectory(PluginsPath);
            foreach (var p in Plugins)
            {
                foreach (var mod in p.Instances)
                {
                    try {  mod.GetType().GetMethod("OnUnload")?.Invoke(mod, null); } catch { }
                }
                p.Instances.Clear();
            }
            Plugins.Clear();
            int category = Buttons.GetCategory("Plugin Settings");
            Buttons.buttons[category] = new ButtonInfo[]
            {
                new ButtonInfo
                {
                    buttonText = "Exit Plugin Settings",
                    method = () => Buttons.CurrentCategoryName = "Settings",
                    isTogglable = false,
                    toolTip = "Return to settings"
                }
            };
            string[] files = Directory.GetFiles(PluginsPath, "*.dll");
            foreach (string file in files)
            {
                try
                {
                    Assembly assembly = Assembly.Load(File.ReadAllBytes(file));
                    var modTypes = assembly.GetTypes().Where(t => typeof(MelonMod).IsAssignableFrom(t) && !t.IsAbstract);
                    Plugin plugin = new Plugin()
                    {
                        FileName = Path.GetFileName(file),
                        Assembly = assembly,
                        Enabled = true
                    };
                    foreach (var type in modTypes)
                    {
                        MelonMod mod = (MelonMod)Activator.CreateInstance(type);

                        mod.OnInitializeMelon();
                        plugin.Instances.Add(mod);

                        var attr = type.Assembly.GetCustomAttribute<MelonInfoAttribute>();
                        plugin.Name = attr?.Name ?? type.Name;
                        plugin.Description = attr?.Author ?? "No description";
                        if (mod is IMenuExtension ext)
                        {
                            foreach (var btn in ext.GetButtons())
                                Buttons.AddButton(category, btn);
                        }
                    }
                    Plugins.Add(plugin);
                }
                catch (Exception e)
                {
                    MelonLogger.Error($"[JupiterX] Failed loading {file}: {e}");
                }
            }
            foreach (var plugin in Plugins)
            {
                Buttons.AddButton(category, new ButtonInfo
                {
                    buttonText = plugin.FileName,
                    overlapText = GetStatus(plugin),
                    method = () => TogglePlugin(plugin),
                    isTogglable = false,
                    toolTip = plugin.Description
                });
            }
            Buttons.AddButton(category, new ButtonInfo
            {
                buttonText = "Reload Plugins",
                method = ReloadPlugins,
                isTogglable = false,
                toolTip = "Reload all plugins"
            });
        }
        private static string GetStatus(Plugin p)
        {
            return (p.Enabled ? "<color=grey>[</color><color=cyan>ON</color><color=grey>]</color> " : "<color=grey>[</color><color=red>OFF</color><color=grey>]</color> ") + p.Name;
        }
        public static void TogglePlugin(Plugin plugin)
        {
            plugin.Enabled = !plugin.Enabled;
            foreach (var mod in plugin.Instances)
            {
                try
                {
                    if (plugin.Enabled)
                        mod.OnInitializeMelon();
                    else
                        mod.GetType().GetMethod("OnUnload")?.Invoke(mod, null);
                }
                catch (Exception e)
                {
                    MelonLogger.Error("[JupiterX] Toggle error: " + e);
                }
            }
            var btn = Buttons.GetIndex(plugin.FileName);
            if (btn != null)
                btn.overlapText = GetStatus(plugin);
            MelonLogger.Msg($"[JupiterX] {plugin.Name} -> {(plugin.Enabled ? "Enabled" : "Disabled")}");
        }
        public static void ExecuteUpdate()
        {
            foreach (var plugin in Plugins.Where(p => p.Enabled))
            {
                foreach (var mod in plugin.Instances)
                {
                    try { mod.OnUpdate(); } catch { }
                }
            }
        }
        public static void ExecuteOnGUI()
        {
            foreach (var plugin in Plugins.Where(p => p.Enabled))
            {
                foreach (var mod in plugin.Instances)
                {
                    try { mod.OnGUI(); } catch { }
                }
            }
        }
    }
}