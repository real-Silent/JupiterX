using Console;
using Il2CppSystem.Net;
using JupiterX;
using JupiterX.Classes;
using JupiterX.Menu;
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
[assembly: MelonInfo(typeof(Plugin), "JupiterX", "2.0.0", "Novaissilly")]
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

            // Console Setup
            Console.Console.LoadConsole();

            // Set UpText
            Utility.FindObjects();
            Utility.CreateFilesOnStart();

            Utility.OnStartFixColor();

            if (Application.Internal_ApplicationWantsToQuit())
            {
                Application.CancelQuit();
            }
            Application.CancelQuit();

            Utility.ogcoctext = Utility.cocText.text;
            Utility.ogcoc = Utility.codeOfConduct.text;
            Utility.ogmotd = Utility.motd.text;
            Utility.ogmotdtext = Utility.motdText.text;

            if (File.Exists(Utility.HasUsedMenuBefore))
                Utility.HasUsedMenuBeforeNoti = true;
            else
                Utility.HasUsedMenuBeforeNoti = false;

            PlayerPrefs.SetString("tutorial", "done");

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
                        button.overlapText ??= buttonText + " <color=grey>[</color><color=green>New</color><color=grey>]</color>";
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
                Utility.Log(
                $"Error with PluginManager.LoadPlugins() at {exc.StackTrace}: {exc.Message}");
            }
        }

        [Obsolete]
        public override void OnApplicationLateStart()
        {
            base.OnApplicationLateStart();

            if (File.Exists($"{Utility.PreferencesPath}"))
            {
                Utility.LoadPreferences();
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Menu.Main.Prefix();
            Utility.UpdateFPS();
            NotificationManager.LoadNotifications();

            if (Utility.updateneeded)
            {
                NotificationManager.SendNotification2("<color=cyan>JupiterX is outdated please go to the discord and update it</color>");
            }

            if (!Utility.HasUsedMenuBeforeNoti)
            {
                if (!File.Exists(Utility.HasUsedMenuBefore))
                    File.Create(Utility.HasUsedMenuBefore);
                File.WriteAllText(Utility.HasUsedMenuBefore, "Thank you for using JupiterX one of the best overpowered gorilla tag copy menus!");
                NotificationManager.SendNoti("Thank you for using JupiterX V2 one of the best overpowered gorilla tag copy menus!");
            }

            if (File.Exists(Utility.HasUsedMenuBefore))
                Utility.HasUsedMenuBeforeNoti = true;

            if (Utility.isBetaRelease)
            {
                if (!Utility.HasSentbetaNoti)
                {
                    NotificationManager.SendNotification("yellow", "BETA", "Thank you for using the beta\nsome stuff may not work or be buggy!");
                    Utility.HasSentbetaNoti = true;
                }
            }
            else
            {
                if (!Utility.HasUsedMenuBeforeNoti) // finally added this
                {
                    if (!Utility.HasSentbetaNoti)
                    {
                        NotificationManager.SendNotification("green", "THANK YOU!", "Thank you for using JupiterX one of the best overpowered gorilla tag copy menus!");
                        Utility.HasSentbetaNoti = true;
                    }
                    Utility.HasUsedMenuBeforeNoti = true;
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
FPS: {(1f/Time.deltaTime).ToString("F1")}
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

            string stumpText = $"<color=#00ffff>JupiterX V2</color>\n<size=1>Thank you for using JupiterX V2\nThe <color=lime>Best</color> Gorilla Tag Copy Menu\n<color=#ff00ff>Version: [{(Utility.updateneeded ? "<color=red>UPDATE NEEDED</color>" : Utility.version)}] | Beta: {Utility.isBetaRelease}</color></size>";
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

            if (PhotonNetwork.InRoom)
            {
                Utility.DoRGBLucyPlz();
            }

            JupiterX.Managers.PluginManager.ExecuteUpdate();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            Utility.ThisGuyIsUsingJupiter();
        }

        static TextMeshPro sstumpText = null;
        public static GameObject StumpText = null;

        public static void DoCoroun(System.Collections.IEnumerator coroutine)
        {
            MelonCoroutines.Start(coroutine);
        }
    }
}