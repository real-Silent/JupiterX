using GorillaNetworking;
using HarmonyLib;
using JupiterX;
using JupiterX.Classes;
using MelonLoader;
using Photon.Pun;
using PlayFab;
using System;
using System.IO;
using TMPro;
using UnhollowerRuntimeLib;
using UnityEngine;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
[assembly: MelonInfo(typeof(Plugin), "JupiterX", "1.0.0", "Silent")]
[assembly: MelonGame()]
namespace JupiterX
{
    // this menu was created by Silent (@s1lnt)
    // if you remove this it counts as skidding
    internal class Plugin : MelonMod
    {
        [Obsolete]
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            ClassInjector.RegisterTypeInIl2Cpp<TimedBehaviour>();
            ClassInjector.RegisterTypeInIl2Cpp<RigManager>();
            ClassInjector.RegisterTypeInIl2Cpp<ColorChanger>();
            ClassInjector.RegisterTypeInIl2Cpp<JupiterX.Classes.Button>();

            // Set UpText
            Utility.FindObjects();
            Utility.CreateFilesOnStart();

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

            if (Utility.isLocked)
            {
                Application.Quit();
                Environment.Exit(0);
            }

            PlayerPrefs.SetString("tutorial", "done");
            GorillaTagger.Instance.disableTutorial = true;
        }


        public override void OnUpdate()
        {
            base.OnUpdate();
            Menu.Main.Prefix();
            if (Utility.isLocked)
            {
                Application.Quit();
                Environment.Exit(0);
            }
            Utility.UpdateFPS();

            NotificationManager.LoadNotifications();

            if (!Utility.HasUsedMenuBeforeNoti)
            {
                if (!File.Exists(Utility.HasUsedMenuBefore))
                    File.Create(Utility.HasUsedMenuBefore);
                File.WriteAllText(Utility.HasUsedMenuBefore, "Thank you for using JupiterX one of the best overpowered gorilla tag copy menus!");
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

            if (JupiterX.Menu.Main.GetIndex("Custom Boards").enabled) // do ! if buggy tf
            {
                string COCTextText = $"--GameInfo--\nTitleId: {title}\nRealtime: {rt}\nVoice: {vc}\nVersion: {version}\nPackageName: {packagename}\n--Client--\nPing: {ping}\nFPS: {fps}";

                Utility.cocText.text = COCTextText;
                Utility.codeOfConduct.text = "JupiterX";

                string v = Utility.version;
                string creds = Utility.Credits;
                string MOTDText = $"thank you for using <b>jupiterx</b> you are using version {v}\nthis menu is my second paid menu to be released after qolossal\nthis menu works in spring cleaning and gorilla tag horror type games (i think)\nany bugs report to the discord\ncredits: {creds}\njoin the discord : discord.gg/zmbGV74y8W".ToUpper().Replace("DISCORD.GG/ZMBGV74Y8W", "discord.gg/zmbGV74y8W").Replace("JUPITERX", "JupiterX");
                Utility.CreateCustomBoards(Utility.motd, Utility.motdText, "JupiterX", MOTDText);
            }
            else
            {
                Utility.motd.text = Utility.ogmotd;
                Utility.motdText.text = Utility.ogmotdtext;
                Utility.cocText.text = Utility.ogcoctext;
                Utility.codeOfConduct.text = Utility.ogcoc;
            }

            string stumpText = $"JupiterX\n<size=1>Thank you for using JupiterX\nThe <color=red>Best</color> Gorilla Tag Copy Dll\n<color=yellow>Version: [{Utility.version}] , Beta Build: {Utility.isBetaRelease}</color></size>";
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

            if (Settings.stumptext)
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
