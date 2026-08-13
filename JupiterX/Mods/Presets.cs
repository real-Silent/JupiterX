using JupiterX.Menu;
using JupiterX.Notifications;
using System.IO;

namespace JupiterX.Mods
{
    public static class Presets
    {
        public static void SaveCustomPreset(int id)
        {
            if (!Directory.Exists($"{Utility.BaseDirectory}/SavedPresets"))
                Directory.CreateDirectory($"{Utility.BaseDirectory}/SavedPresets");

            File.WriteAllText($"{Utility.BaseDirectory}/SavedPresets/Preset_" + id + ".txt", Utility.SavePreferencesToText());
        }

        public static void LoadCustomPreset(int id)
        {
            if (Directory.Exists($"{Utility.BaseDirectory}/SavedPresets"))
            {
                string text = File.ReadAllText($"{Utility.BaseDirectory}/SavedPresets/Preset_" + id + ".txt");
                Utility.LoadPreferencesFromText(text);
            }
        }


        public static void NovaPreset()
        {
            string[] presetMods =
            {
                "Freeze Player In Menu",
                "Menu Trail",
                "See Others Menus",
                "Menu Outline",
                "Custom Boards",
                "Version Text",
                "Stump Text",
                "FPS Text",
                "Anti AFK",
                "Turning",
                "Excel Fly",
                "Long Arms",
                "No Tag Freeze",
                "Name Tags",
                "FPS Overlay",
                "Ping Overlay"
            };

            Utility.PageType = -1;
            Utility.currentTheme = 8;
            Utility.MainDropType = -1;
            Movement.FlySpeedAmount = 2;
            Movement.ArmSizeAmount = -1;
            Utility.currentFontStyleChoice = 0;

            Utility.ChangePageType();
            Utility.ChangeMenuTheme();
            Utility.ChangeDropType();
            Movement.ChangeFlySpeed();
            Movement.ChangeArmLength();
            Utility.ChangeFontStyle();

            Utility.Panic();

            foreach (string mod in presetMods)
                Main.Toggle(mod);

            NotificationManager.SendNotification("<color=grey>[</color><color=purple>PRESET</color><color=grey>]</color> Nova preset enabled successfully.");
        }
    }
}