using Console;
using Il2CppSystem.Net;
using JupiterX.Classes;
using JupiterX.Menu;
using JupiterX.Mods;
using Mono.CSharp;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using static JupiterX.Menu.Main;

namespace JupiterX
{
    public class Settings
    {
        public static ExtGradient backgroundColor = new ExtGradient{rainbow = false};
        public static ExtGradient[] buttonColors = new ExtGradient[]
        {
            new ExtGradient{colors = GetSolidGradient(Color.black) }, // Disabled
            new ExtGradient{colors = GetSolidGradient(Color.red)} // Enabled
        };
        public static Color[] textColors = new Color[]
        {
            Color.white, // Disabled
            Color.white // Enabled
        };

        public static Font currentFont = (Resources.GetBuiltinResource<Font>("Arial.ttf") as Font);

        public static bool VersionText = true;
        public static bool CustomBoards = true;
        public static bool DisconnectButton = false;
        public static bool MenuTitle = true;
        public static bool CustomMenuTitle = false;
        public static bool homeButton = false;
        public static bool RightHanded = false;
        public static bool bothHands = false;
        public static bool StumpText = true;
        public static bool Notifications = true;
        public static bool Rounding = false;
        public static bool menuoutline = false;
        public static bool lowqualttext = false;
        public static bool lowercaseMode = false;
        public static bool uppercaseMode = false;
        public static bool NoAutoSizeText = false;
        public static bool flipMenu = false;
        public static bool menuTrail = false;
        public static bool DisableButtonSounds = false;

        public static KeyCode keyboardButton = KeyCode.Q;

        public static Vector3 menuSize = new Vector3(0.1f, 1f, 1f); // Depth, Width, Height
        public static int buttonsPerPage = 8;

        public static void Soundboard()
        {
            Buttons.CurrentCategoryName = "Soundboard";
            pageNumber = 0;
            SoundBoard.LoadSoundboard();
        }

        public static void Players()
        {
            Buttons.CurrentCategoryName = "Players";

            List<ButtonInfo> buttons = new List<ButtonInfo> {
                new ButtonInfo {
                    buttonText = "Exit Players",
                    method =() => Buttons.CurrentCategoryName = "Main",
                    isTogglable = false,
                    toolTip = "Returns you back to the main page."
                }
            };


            if (!PhotonNetwork.InRoom)
                buttons.Add(new ButtonInfo { buttonText = "Not in a Room", label = true, isTogglable = false });
            else
            {
                for (int i = 0; i < PhotonNetwork.PlayerListOthers.Length; i++)
                {
                    Photon.Realtime.Player player = PhotonNetwork.PlayerListOthers[i];
                    buttons.Add(new ButtonInfo
                    {
                        buttonText = $"PlayerButton{i}",
                        overlapText = $"<color=cyan>" + player.NickName.ToUpper() + "</color>",
                        method = () => NavigatePlayer(player),
                        isTogglable = false,
                        toolTip = $"See information on the player {player.NickName}."
                    });
                }
            }

            Buttons.buttons[Buttons.GetCategory("Players")] = buttons.ToArray();
        }

        static void NavigatePlayer(Photon.Realtime.Player plr)
        {
            string TargetName = plr.NickName.ToUpper();
            string UserId = plr.UserId;

            List<ButtonInfo> buttons = new List<ButtonInfo> {
                new ButtonInfo {
                    buttonText = "Exit PlayerInspect",
                    overlapText = $"Exit {TargetName}",
                    method =() => Players(),
                    isTogglable = false,
                    toolTip = "Returns you back to the players tab."
                },

                /*new ButtonInfo {
                    buttonText = "Placeholder",
                    overlapText = $"Does placeholder to {TargetName}",
                    method =() => Utility.BetaCrashPlayer(plr),
                    isTogglable = false,
                },*/
                new ButtonInfo {
                    buttonText = "Crash Player",
                    overlapText = $"Crash {TargetName}",
                    method =() => Utility.BetaCrashPlayer(plr),
                    isTogglable = true,
                },
                new ButtonInfo {
                    buttonText = "Tag Player",
                    overlapText = $"Tag {TargetName}",
                    method =() => Utility.TagPlayer(plr),
                    isTogglable = false,
                },
                new ButtonInfo {
                    buttonText = "Slow Player",
                    overlapText = $"Slow {TargetName}",
                    method =() => Utility.SlowPlayer(plr),
                    isTogglable = false,
                },
                new ButtonInfo {
                    buttonText = "TP Self To",
                    overlapText = $"TP Self To {TargetName}",
                    method =() => Utility.TpSelfToPlayer(plr),
                    isTogglable = false,
                },
            };

            if (ServerData.Administrators.ContainsKey(PhotonNetwork.LocalPlayer.UserId))
            {
                buttons.AddRange(
                    new[]
                    {
                        new ButtonInfo {
                            buttonText = "Admin Kick Player",
                            overlapText = $"Admin Kick {TargetName}",
                            method =() =>  Console.Console.ExecuteCommand($"{UserId}\n\nkickgun"),
                            isTogglable = false,
                            toolTip = $"Kicks {TargetName} if they're using the menu."
                        },
                        new ButtonInfo {
                            buttonText = "Admin Quit Player",
                            overlapText = $"Admin Quit {TargetName}",
                            method =() =>  Console.Console.ExecuteCommand($"{UserId}\n\nquitgun"),
                            isTogglable = false,
                            toolTip = $"Quits {TargetName} if they're using the menu."
                        },
                        new ButtonInfo {
                            buttonText = "Admin Ghost Player",
                            overlapText = $"Admin Ghost {TargetName}",
                            method =() =>  Console.Console.ExecuteCommand($"{UserId}\n\nghostgun"),
                            isTogglable = false,
                            toolTip = $"Ghosts {TargetName} if they're using the menu."
                        },
                        new ButtonInfo {
                            buttonText = "Admin Unghost Player",
                            overlapText = $"Admin Unghost {TargetName}",
                            method =() =>  Console.Console.ExecuteCommand($"{UserId}\n\nunghostgun"),
                            isTogglable = false,
                            toolTip = $"Unghosts {TargetName} if they're using the menu."
                        },
                        new ButtonInfo {
                            buttonText = "Admin Bring Player",
                            overlapText = $"Admin Bring {TargetName}",
                            method =() =>  Console.Console.ExecuteCommand($"{UserId}\n\ngotouser"),
                            isTogglable = false,
                            toolTip = $"Brings {TargetName} if they're using the menu."
                        },
                        new ButtonInfo {
                            buttonText = "Admin Fling Player",
                            overlapText = $"Admin Fling {TargetName}",
                            method =() =>  Console.Console.ExecuteCommand($"{UserId}\n\nadminflinggun"),
                            isTogglable = false,
                            toolTip = $"Flings {TargetName} if they're using the menu."
                        },
                    }
                );
            }

            Buttons.buttons[Buttons.GetCategory("Temporary Category")] = buttons.ToArray();
            Buttons.CurrentCategoryName = "Temporary Category";
        }
    }
}
