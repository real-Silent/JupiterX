using System.IO;

namespace JupiterX.Mods
{
    public static class Presets
    {
        public static void SaveCustomPreset(int id)
        {
            if (!Directory.Exists($"{Utility.MainPath}/SavedPresets"))
                Directory.CreateDirectory($"{Utility.MainPath}/SavedPresets");

            File.WriteAllText($"{Utility.MainPath}/SavedPresets/Preset_" + id + ".txt", Utility.SavePreferencesToText());
        }

        public static void LoadCustomPreset(int id)
        {
            if (Directory.Exists($"{Utility.MainPath}/SavedPresets"))
            {
                string text = File.ReadAllText($"{Utility.MainPath}/SavedPresets/Preset_" + id + ".txt");
                Utility.LoadPreferencesFromText(text);
            }
        }
    }
}