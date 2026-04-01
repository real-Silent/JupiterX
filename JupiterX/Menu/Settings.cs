using Il2CppSystem.Net;
using JupiterX.Classes;
using JupiterX.Menu;
using JupiterX.Mods;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using static JupiterX.Menu.Main;

namespace JupiterX
{
    internal class Settings
    {
        public static ExtGradient backgroundColor = new ExtGradient{isRainbow = false};
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
        public static bool DisconnectButton = false;
        public static bool MenuTitle = true;
        public static bool homeButton = false;
        public static bool RightHanded = false;
        public static bool StumpText = true;
        public static bool Notifications = true;
        public static bool Rounding = false;

        public static KeyCode keyboardButton = KeyCode.Q;

        public static Vector3 menuSize = new Vector3(0.1f, 1f, 1f); // Depth, Width, Height
        public static int buttonsPerPage = 8;

        public static void Soundboard()
        {
            currentCategoryName = "Soundboard";
            pageNumber = 0;
            SoundBoard.LoadSoundboard();
        }

        public static void Players()
        {
            currentCategoryName = "Players";

            List<ButtonInfo> buttons = new List<ButtonInfo> {
                new ButtonInfo {
                    buttonText = "Exit Players",
                    method =() => currentCategoryName = "Main",
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

            Buttons.buttons[GetCategory("Players")] = buttons.ToArray();
        }

        static void NavigatePlayer(Photon.Realtime.Player plr)
        {
            string TargetName = plr.NickName.ToUpper();

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
                    buttonText = "TP Lucy To",
                    overlapText = $"TP Lucy To {TargetName}",
                    method =() => Utility.MakeLucyGoToPlayer(plr),
                    isTogglable = false,
                },

                new ButtonInfo {
                    buttonText = "TP Self To",
                    overlapText = $"TP Self To {TargetName}",
                    method =() => Utility.TpSelfToPlayer(plr),
                    isTogglable = false,
                },

                new ButtonInfo {
                    buttonText = "Get Ownership Of",
                    overlapText = $"Get Ownership Of {TargetName}",
                    method =() => Utility.GetOwnerShipOfPlayer(plr),
                    isTogglable = false,
                },

                new ButtonInfo {
                    buttonText = "Move Player To Self",
                    overlapText = $"Move Player To Self {TargetName} [Ownership] W?",
                    method =() => Utility.MovePlayerToMe(plr),
                    isTogglable = false,
                },
            };
            Buttons.buttons[GetCategory("Temporary Category")] = buttons.ToArray();
        }
    }
}
