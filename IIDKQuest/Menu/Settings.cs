using JupiterX.Classes;
using JupiterX.Menu;
using JupiterX.Mods;
using Mono.CSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static JupiterX.Menu.Main;
using static JupiterX.Utility;

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

            GetIndex("Change Gun Variation").overlapText = "Change Gun Variation <color=grey>[</color><color=green>" + VariationNames[gunVariation] + "</color><color=grey>]</color>";
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

            GetIndex("Change Gun Direction").overlapText = "Change Gun Direction <color=grey>[</color><color=green>" + DirectionNames[GunDirection] + "</color><color=grey>]</color>";
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
            GetIndex("Change Gun Line Quality").overlapText = "Change Gun Line Quality <color=grey>[</color><color=green>" + Names[gunLineQualityIndex] + "</color><color=grey>]</color>";
        }
    }
}
