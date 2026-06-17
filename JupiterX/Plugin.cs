using JupiterX;
using JupiterX.Classes;
using JupiterX.Managers;
using JupiterX.Menu;
using JupiterX.Notifications;
using MelonLoader;
using Photon.Pun;
using PlayFab;
using System;
using System.IO;
using System.Linq;
using TMPro;
using UnhollowerRuntimeLib;
using UnityEngine;

// this menu was created by Nova (@novaissilly)
// if you remove this it counts as skidding
[assembly: MelonInfo(typeof(Plugin), "JupiterX", "2.0.0", "Novaissilly_jupx")]
[assembly: MelonGame()]
namespace JupiterX
{
    public class Plugin : MelonMod
    {
        [Obsolete]
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            ClassInjector.RegisterTypeInIl2Cpp<TimedBehaviour>();
            ClassInjector.RegisterTypeInIl2Cpp<RigManager>();
            ClassInjector.RegisterTypeInIl2Cpp<ColorChanger>();
            ClassInjector.RegisterTypeInIl2Cpp<ClampColor>();
            ClassInjector.RegisterTypeInIl2Cpp<ButtonCollider>();
            ClassInjector.RegisterTypeInIl2Cpp<NotifiLib>();
            ClassInjector.RegisterTypeInIl2Cpp<ShibaNotificationLib>();

            GameObject notiHolder = new GameObject();
            notiHolder.name = "JupiterX_Holder";
            notiHolder.AddComponent<NotifiLib>();
            notiHolder.AddComponent<ShibaNotificationLib>();

            // Console Setup
            Console.ConsoleJupiterX.LoadConsole();

            // Set UpText
            Utility.FindObjects();
            Utility.CreateFilesOnStart();

            Utility.OnStartFixColor();

            if (Application.Internal_ApplicationWantsToQuit())
            {
                Application.CancelQuit();
            }
            Application.CancelQuit();

            Utility.LockCheck();

            Utility.ogcoctext = Utility.cocText.text;
            Utility.ogcoc = Utility.codeOfConduct.text;
            Utility.ogmotd = Utility.motd.text;
            Utility.ogmotdtext = Utility.motdText.text;

            Utility.CacheSounds(); // For caching the menu sounds causes less lag.

            try
            {
                string allButtonsPath = Path.Combine(Application.persistentDataPath, "JupiterX/AllButtons.txt");

                string[] newButtonNames = Buttons.buttons.SelectMany(list => list).Select(button => button.buttonText).ToArray();
                if (File.Exists(allButtonsPath))
                {
                    string[] oldButtonNames = File.ReadAllText(allButtonsPath).Split('\n');

                    foreach (string name in newButtonNames)
                    {
                        if (oldButtonNames.Contains(name)) continue;
                        ButtonInfo button = Buttons.GetIndex(name);
                        string buttonText = button.overlapText ?? button.buttonText;
                        button.overlapText ??= buttonText + " <color=grey>[</color><color=cyan>New</color><color=grey>]</color>";
                    }
                }

                File.WriteAllText(allButtonsPath, string.Join("\n", newButtonNames));
            }
            catch { }

            try
            {
                JupiterX.Managers.PluginManager.LoadPlugins();
            }
            catch (Exception exc)
            {
                Utility.Log($"Error with PluginManager.LoadPlugins() at {exc.StackTrace}: {exc.Message}");
            }

            HarmonyLib.Harmony jupixharm = new HarmonyLib.Harmony("Novaissilly_jupx");
            jupixharm.PatchAll();
        }

        [Obsolete]
        public override void OnApplicationLateStart()
        {
            base.OnApplicationLateStart();

            if (File.Exists($"{Utility.PreferencesPath}"))
            {
                Utility.LoadPreferences();
                /*Menu.Main.Toggle("Custom Boards");
                Menu.Main.Toggle("Stump Text");
                Menu.Main.Toggle("Version Text");*/
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Utility.canusemenu == false)
            {
                NotifiLib.SendNotification("<color=red>[INFO]</color> Menu is locked!", 15f);
                return;
            }

            if (GameObject.Find($">>Console<<_{Utility.version}") == null)
            {
                NotifiLib.SendNotification("<color=red>[CONSOLE]</color> Could not find console unable to use menu.", 60f);
                Utility.canusemenu = false;
            }

            Menu.Main.Prefix();
            Utility.UpdateFPS();

            if (Utility.updateneeded)
            {
                NotifiLib.SendNotification("<color=cyan>JupiterX needs a update please go to the discord and update it</color>", 30f);
            }
            if (Utility.extremeupdateneeded)
            {
                NotifiLib.SendNotification("<color=cyan>JupiterX is extremely outdated please go to the discord and update it</color>", 60f);
            }

            if (File.Exists(Utility.HasUsedMenuBefore))
                Utility.UsedBeforeNotificaiton = true;

            if (!Utility.UsedBeforeNotificaiton)
            {
                if (!File.Exists(Utility.HasUsedMenuBefore))
                    File.Create(Utility.HasUsedMenuBefore);
                File.WriteAllText(Utility.HasUsedMenuBefore, "Thank you for using JupiterX one of the best overpowered gorilla tag copy menus!");
                NotifiLib.SendNotification("<color=cyan>[INFO]</color> Thank you for using JupiterX one of the best overpowered gorilla tag copy menus!", 20f);

                AchievementManager.UnlockAchievement(new AchievementManager.Achievement()
                {
                    name = "First Time Use",
                    description = "Opened and used the menu for the first time."
                });
            }

            if (Utility.isBetaRelease)
            {
                if (!Utility.UsedBeforeNotificaiton)
                {
                    NotifiLib.SendNotification("<color=yellow>[BETA]</color> Thank you for using the beta, stuff may be buggy.", 13f);
                    Utility.UsedBeforeNotificaiton = true;
                    if (!File.Exists($"{Utility.MainPath}/ClaimedBetaAchievement.txt"))
                    {
                        AchievementManager.UnlockAchievement(new AchievementManager.Achievement()
                        {
                            name = "Beta Tester",
                            description = "Opened and used the menu for the first time."
                        });
                        File.WriteAllText($"{Utility.MainPath}/ClaimedBetaAchievement.txt", "");
                    }
                }
            }
            else
            {
                if (!Utility.UsedBeforeNotificaiton)
                {
                    NotifiLib.SendNotification("<color=cyan>[INFO]</color> Thank you for using jupiterx.", 10f);
                    Utility.UsedBeforeNotificaiton = true;
                }
            }

            string title = PlayFabSettings.TitleId;
            string rt = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime;
            string vc = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdVoice;
            string version = PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion;
            string packagename = Application.identifier;
            string ping = PhotonNetwork.GetPing().ToString("F2");
            string fps = Utility.fps;

            if (Settings.CustomBoards)
            {
                string cocTextNew = $@"-Client Info-
FPS: {(1f / Time.deltaTime).ToString("F1")}
Ping: {PhotonNetwork.GetPing()}
Time: {DateTime.Now.ToLongTimeString()}
-Game Info-
TitleId: {PlayFabSettings.TitleId}
Realtime: {PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime}
Voice: {PhotonNetwork.PhotonServerSettings.AppSettings.AppIdVoice}
-Local Player Info-
NickName: {PhotonNetwork.LocalPlayer.NickName}
UserId: {(PhotonNetwork.IsConnected ? PhotonNetwork.LocalPlayer.UserId : "N/A")}
Photon Connected: {PhotonNetwork.IsConnected}
PlayFab Connected: {PlayFabClientAPI.IsClientLoggedIn()}";
                Utility.cocText.text = cocTextNew;
                Utility.codeOfConduct.text = "<color=cyan>JupiterX V2</color>";

                string v = Utility.version;
                string creds = Utility.Credits;
                Utility.CreateCustomBoards(Utility.motd, Utility.motdText, "<color=cyan>JupiterX V2</color>", Utility.motdtemplate);
            }
            else
            {
                Utility.motd.text = Utility.ogmotd;
                Utility.motdText.text = Utility.ogmotdtext;
                Utility.cocText.text = Utility.ogcoctext;
                Utility.codeOfConduct.text = Utility.ogcoc;
            }

            string updatetext = "UPDATE";
            if (Utility.extremeupdateneeded)
                updatetext = "<color=red>EXTREME UPDATE NEEDED</color>";
            else if (Utility.updateneeded)
                updatetext = "<color=red>UPDATE NEEDED</color>";
            bool updateneeded = Utility.updateneeded || Utility.extremeupdateneeded;
            string stumpText = $"<color=#00ffff>JupiterX V2</color>\n<size=1>Thank you for using JupiterX V2\nThe <color=#3333ff>Best</color> Gorilla Tag Copy Menu\n<color=#ff00ff>Version: [{(updateneeded ? updatetext : Utility.version)}] | Beta: {Utility.isBetaRelease}</color></size>";
            if (StumpText == null)
            {
                StumpText = new GameObject("StumpTextObject");
                StumpText.transform.position = new Vector3(-66.937f, 12.187f, -82.335f);
                StumpText.transform.rotation = Quaternion.identity;
            }

            if (sstumpText == null)
            {
                sstumpText = StumpText.AddComponent<TextMeshPro>();
                sstumpText.richText = true;
                sstumpText.alignment = TextAlignmentOptions.Center;
                sstumpText.fontSize = 2;
                sstumpText.text = stumpText;
            }

            if (Settings.StumpText)
            {
                StumpText.SetActive(true);
                sstumpText.transform.LookAt(Utility.MainCamera().transform);
                sstumpText.transform.Rotate(0, 180f, 0);
            }
            else
            {
                StumpText.SetActive(false);
            }

            JupiterX.Menu.Main.DestroyPointer();
            JupiterX.Managers.PluginManager.ExecuteUpdate();

            Utility.DetectOtherUsers();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            Utility.ThisGuyIsUsingJupiter();
        }

        static TextMeshPro sstumpText = null;
        public static GameObject StumpText = null;

        public static void StartCoroutine(System.Collections.IEnumerator coroutine)
        {
            MelonCoroutines.Start(coroutine);
        }
        public static void StopCoroutine(System.Collections.IEnumerator coroutine)
        {
            MelonCoroutines.Stop(coroutine);
        }
    }
}