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
            new ExtGradient{colors = GetSolidGradient(Color.grey)} // Enabled
        };
        public static Color[] textColors = new Color[]
        {
            Color.white, // Disabled
            Color.white // Enabled
        };

        public static Font currentFont = (Resources.GetBuiltinResource<Font>("Arial.ttf") as Font);

        public static bool fpsCounter = true;
        public static bool disconnectButton = true;
        public static bool rightHanded = false;
        public static bool stumptext = true;
        public static bool disableNotis = false;
        public static bool Rounding = false;

        public static KeyCode keyboardButton = KeyCode.Q;

        public static Vector3 menuSize = new Vector3(0.1f, 1f, 1f); // Depth, Width, Height
        public static int buttonsPerPage = 8;

        public static void Soundboard()
        {
            buttonsType = 14;
            pageNumber = 0;
            SoundBoard.LoadSoundboard();
        }

        public static void MovePage(int buttonType)
        {
            buttonsType = buttonType;
            pageNumber = 0;
        }

        public static void Players()
        {
            buttonsType = 15; // 15
            pageNumber = 0;

            List<ButtonInfo> buttons = new List<ButtonInfo> {
                new ButtonInfo {
                    buttonText = "Exit Players",
                    method =() => MovePage(0),
                    isTogglable = false,
                    toolTip = "Returns you back to the main page."
                }
            };


            if (!PhotonNetwork.InRoom)
                buttons.Add(new ButtonInfo { buttonText = "Not in a Room", isTogglable = false });
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

            Buttons.buttons[15] = buttons.ToArray();
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
            Buttons.buttons[16] = buttons.ToArray();
        }


        public static void RightHand()
        {
            rightHanded = true;
        }

        public static void LeftHand()
        {
            rightHanded = false;
        }

        public static void EnableFPSCounter()
        {
            fpsCounter = true;
        }

        public static void DisableFPSCounter()
        {
            fpsCounter = false;
        }

        public static void EnableStumpText()
        {
            stumptext = true;
        }

        public static void DisableStumpText()
        {
            stumptext = false;
        }

        public static void EnableDisconnectButton()
        {
            disconnectButton = true;
        }

        public static void DisableDisconnectButton()
        {
            disconnectButton = false;
        }

        public static void EnableNotis()
        {
            disableNotis = false;
        }

        public static void DisableNotis()
        {
            disableNotis = true;
            NotificationManager.ClearAllNotifications();
        }
    }
}
