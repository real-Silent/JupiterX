using JupiterX.Classes;
using JupiterX.Menu;
using JupiterX.Notifications;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static JupiterX.Menu.Main;

namespace JupiterX.Managers
{
    public static class AchievementManager
    {
        private static List<Achievement> _achievements;
        public static List<Achievement> Achievements
        {
            get
            {
                if (_achievements != null) return _achievements;
                _achievements = new List<Achievement>();

                string achievementsFolder = Path.Combine(Application.persistentDataPath, "JupiterX", "Achievements");
                string[] files = Directory.GetFiles(achievementsFolder);
                foreach (string file in files)
                {
                    if (file.EndsWith(".json"))
                        _achievements.Add(Achievement.FromJObject(JObject.Parse(File.ReadAllText(file))));
                }

                return _achievements;
            }
            set => _achievements = value;
        }

        public static void EnterAchievementTab()
        {
            int achievementCount = Achievements.Count;

            List<ButtonInfo> achievementButtons = new List<ButtonInfo> { new ButtonInfo { buttonText = "Exit Achievements", method = () => Buttons.CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page." } };

            if (achievementCount <= 0)
                achievementButtons.Add(
                    new ButtonInfo
                    {
                        buttonText = "You have no achievements.",
                        label = true
                    });
            else
                for (int i = 0; i < achievementCount; i++)
                {
                    Achievement achievement = Achievements[i];
                    achievementButtons.Add(
                        new ButtonInfo
                        {
                            buttonText = $"Achievement{i}",
                            overlapText = achievement.name,
                            method = () => PromptSingle($"Well done for getting {achievement.name}!", null, "Done"),
                            isTogglable = false,
                            toolTip = achievement.description
                        });
                }

            Buttons.buttons[Buttons.GetCategory("Achievements")] = achievementButtons.ToArray();
            Buttons.CurrentCategoryName = "Achievements";
        }

        public static bool HasAchievement(string name) =>
            Achievements.Any(a => a.name == name);

        public static void UnlockAchievement(Achievement achievement)
        {
            if (HasAchievement(achievement.name))
                return;

            Utility.PlaySound(Utility.achievementSound);
            NotificationManager.SendNotification($"<color=grey>[</color><color=purple>ACHIEVEMENT</color><color=grey>]</color> Achievement unlocked! \"{achievement.name}\"\n{achievement.description}", 5f);

            Achievements.Add(achievement);
            string achievementsFolder = Path.Combine(Application.persistentDataPath, "JupiterX", "Achievements");
            if (!Directory.Exists(achievementsFolder))
                Directory.CreateDirectory(achievementsFolder);
            string filePath = Path.Combine(achievementsFolder, $"{achievement.name}.json");
            File.WriteAllText(filePath, achievement.ToJObject().ToString());
        }

        public struct Achievement
        {
            public string name;

            public string description;

            public readonly JObject ToJObject() => new JObject
            {
                ["name"] = name,

                ["description"] = description
            };

            public static Achievement FromJObject(JObject obj) => new Achievement
            {
                name = (string)obj["name"],
                description = (string)obj["description"]
            };
        }
    }
}