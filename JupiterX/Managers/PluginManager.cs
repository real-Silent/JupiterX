using MelonLoader;
using JupiterX.Menu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using System.Diagnostics;
using JupiterX.Classes;

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

        public static readonly List<Plugin> Plugins = new List<Plugin>();

        private static string PluginsPath => Path.Combine(Application.persistentDataPath, "JupiterX/Plugins");

        public static void ReloadPlugins()
        {
            NotificationManager.SendNotification2("<color=yellow>[SYSTEM]</color> Reloading all plugins...");
            Utility.SavePreferences();

            try
            {
                LoadPlugins();
                MelonLogger.Msg("Plugins reloaded successfully.");
            }
            catch (Exception e)
            {
                MelonLogger.Error("Failed to reload: " + e.Message);
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
                    try
                    {
                        mod.GetType().GetMethod("OnUnload")?.Invoke(mod, null);
                    }
                    catch { }
                }
                p.Instances.Clear();
            }

            Plugins.Clear();

            string[] files = Directory.GetFiles(PluginsPath, "*.dll");
            foreach (string file in files)
            {
                try
                {
                    byte[] data = File.ReadAllBytes(file);
                    Assembly assembly = Assembly.Load(data);

                    var modTypes = assembly.GetTypes()
                        .Where(t => typeof(MelonMod).IsAssignableFrom(t) && !t.IsAbstract);

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
                    }

                    Plugins.Add(plugin);
                }
                catch (Exception e)
                {
                    MelonLogger.Msg($"[JupiterX] Error loading {file}: {e}");
                }
            }

            Buttons.buttons[Buttons.GetCategory("Plugin Settings")] = new[]
            {
                new ButtonInfo
                {
                    buttonText = "Exit Plugin Settings",
                    method = () => Buttons.CurrentCategoryName = "Settings",
                    isTogglable = false,
                    toolTip = "Return to settings"
                }
            };

            foreach (var plugin in Plugins)
            {
                try
                {
                    Buttons.AddButton(
                        Buttons.GetCategory("Plugin Settings"),
                        new ButtonInfo
                        {
                            buttonText = plugin.FileName,
                            overlapText = (plugin.Enabled ? "<color=grey>[</color><color=green>ON</color><color=grey>]</color> " : "<color=grey>[</color><color=red>OFF</color><color=grey>]</color> ") + plugin.Name,
                            method = () => TogglePlugin(plugin),
                            isTogglable = false,
                            toolTip = plugin.Description
                        });
                }
                catch (Exception e)
                {
                    MelonLogger.Msg($"[JupiterX] UI error for {plugin.Name}: {e}");
                }
            }

            Buttons.AddButton(Buttons.GetCategory("Plugin Settings"),
                new ButtonInfo
                {
                    buttonText = "Reload Plugins",
                    method = ReloadPlugins,
                    isTogglable = false,
                    toolTip = "Reload all plugins"
                });

            Buttons.AddButton(Buttons.GetCategory("Plugin Settings"),
                new ButtonInfo
                {
                    buttonText = "Open Plugins Folder",
                    method = () => Process.Start(PluginsPath),
                    isTogglable = false,
                    toolTip = "Open plugin directory"
                });
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
                catch { }
            }
            var btn = Buttons.GetIndex(plugin.FileName);
            if (btn != null)
            {
                btn.overlapText = (plugin.Enabled ? "<color=grey>[</color><color=green>ON</color><color=grey>]</color> " : "<color=grey>[</color><color=red>OFF</color><color=grey>]</color> ") + plugin.Name;
            }
            MelonLogger.Msg($"[JupiterX] {plugin.Name} is now {(plugin.Enabled ? "Enabled" : "Disabled")}");
        }
    }
}