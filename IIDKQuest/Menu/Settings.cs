using JupiterX.Classes;
using JupiterX.Menu;
using JupiterX.Mods;
using Mono.CSharp;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static JupiterX.Menu.Main;
using static JupiterX.Utility;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX
{
    internal class Settings
    {
        public static ExtGradient backgroundColor = new ExtGradient{isRainbow = false};
        public static ExtGradient[] buttonColors = new ExtGradient[]
        {
            new ExtGradient{colors = GetSolidGradient(Color.black) }, // Disabled
            new ExtGradient{colors = GetSolidGradient(Color.black)} // Enabled
        };
        public static Color[] textColors = new Color[]
        {
            Color.white, // Disabled
            Color.grey // Enabled
        };

        public static Font currentFont = (Resources.GetBuiltinResource<Font>("Arial.ttf") as Font);

        public static bool fpsCounter = true;
        public static bool disconnectButton = true;
        public static bool rightHanded = false;
        public static bool stumptext = true;
        public static bool disableNotis = false;

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
                    string playerColor = "#ffffff";
                    try
                    {
                        playerColor = $"#{RigManager.GetVRRigFromPlayer(player).mainSkin.material.color}";
                    }
                    catch { }

                    buttons.Add(new ButtonInfo
                    {
                        buttonText = $"PlayerButton{i}",
                        overlapText = $"<color={playerColor}>" + player.NickName.ToUpper() + "</color>",
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
        }



        // Credits to Scintilla for the idea
        public static void ChangeGunVariation(bool positive = true)
        {
            string[] VariationNames = new string[]
            {
                "Default",
                "Lightning",
                "Wavy",
                "Blocky",
                "Zigzag",
                "Spring",
                "Bouncy",
                "Audio",
                "Bezier"
            };

            if (positive)
                gunVariation++;
            else
                gunVariation--;

            gunVariation %= VariationNames.Length;
            if (gunVariation < 0)
                gunVariation = VariationNames.Length - 1;

            GetIndex("Change Gun Variation").overlapText = "Change Gun Variation <color=cyan>[" + VariationNames[gunVariation] + "]</color>";
        }

        public static void ChangeGunDirection(bool positive = true)
        {
            string[] DirectionNames = new string[]
            {
                "Default",
                "Legacy",
                "Laser",
                "Finger",
                "Face"
            };

            if (positive)
                GunDirection++;
            else
                GunDirection--;

            GunDirection %= DirectionNames.Length;
            if (GunDirection < 0)
                GunDirection = DirectionNames.Length - 1;

            GetIndex("Change Gun Direction").overlapText = "Change Gun Direction <color=cyan>[" + DirectionNames[GunDirection] + "]</color>";
        }

        private static int gunLineQualityIndex = 2;
        public static void ChangeGunLineQuality(bool positive = true)
        {
            string[] Names = new string[]
            {
                "Potato",
                "Low",
                "Normal",
                "High",
                "Extreme"
            };

            int[] Qualities = new int[]
            {
                10,
                25,
                50,
                100,
                250
            };

            if (positive)
                gunLineQualityIndex++;
            else
                gunLineQualityIndex--;

            gunLineQualityIndex %= Names.Length;
            if (gunLineQualityIndex < 0)
                gunLineQualityIndex = Names.Length - 1;

            GunLineQuality = Qualities[gunLineQualityIndex];
            GetIndex("Change Gun Line Quality").overlapText = "Change Gun Line Quality <color=cyan>[" + Names[gunLineQualityIndex] + "]</color>";
        }
    }
}
