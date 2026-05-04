using JupiterX.Classes;
using JupiterX.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static JupiterX.Menu.Main;
using static JupiterX.Extensions.StringExtensions;

namespace JupiterX.Notifications
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class NotifiLib : MonoBehaviour
    {
        public NotifiLib(IntPtr e) : base(e) { }
        public static NotifiLib instance;
        public GameObject HUDObj;
        public GameObject HUDObj2;

        private GameObject MainCamera;

        private Material AlertText = new Material(Shader.Find("GUI/Text Shader"));

        public static string PreviousNotifi;

        public static Dictionary<string, string> information = new Dictionary<string, string> { };

        public static Text NotifiText;
        public static Text ModText;
        public static Text StatsText;

        private bool HasInit;

        public static int NotifiCounter = 0;

        public static float notificationDecayTime = 1f;

        public virtual void Start()
        {
            instance = this;
        }

        private void Init()
        {
            MainCamera = Camera.main.gameObject;
            HUDObj = new GameObject();
            HUDObj2 = new GameObject
            {
                name = "NOTIFICATIONLIB_HUD_OBJ"
            };
            HUDObj.name = "NOTIFICATIONLIB_HUD_OBJ";
            HUDObj.AddComponent<Canvas>();
            HUDObj.AddComponent<CanvasScaler>();
            HUDObj.AddComponent<GraphicRaycaster>();
            HUDObj.GetComponent<Canvas>().enabled = true;
            HUDObj.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            HUDObj.GetComponent<Canvas>().worldCamera = MainCamera.GetComponent<Camera>();
            HUDObj.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 5f);
            HUDObj.GetComponent<RectTransform>().position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
            HUDObj2.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z - 4.6f);
            HUDObj.transform.parent = HUDObj2.transform;
            HUDObj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1.6f);
            Vector3 eulerAngles = HUDObj.GetComponent<RectTransform>().rotation.eulerAngles;
            eulerAngles.y = -270f;
            HUDObj.transform.localScale = Vector3.one;
            HUDObj.GetComponent<RectTransform>().rotation = Quaternion.Euler(eulerAngles);
            NotifiText = new GameObject
            {
                transform =
                {
                    parent = HUDObj.transform
                }
            }.AddComponent<Text>();
            NotifiText.text = "";
            NotifiText.fontSize = 30;
            NotifiText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            NotifiText.rectTransform.sizeDelta = new Vector2(450f, 210f);
            NotifiText.alignment = TextAnchor.LowerLeft;
            NotifiText.verticalOverflow = VerticalWrapMode.Overflow;
            NotifiText.rectTransform.localScale = new Vector3(0.00333333333f, 0.00333333333f, 0.33333333f);
            NotifiText.rectTransform.localPosition = new Vector3(-1f, -1f, -0.5f);
            NotifiText.material = AlertText;

            ModText = new GameObject
            {
                transform =
                {
                    parent = HUDObj.transform
                }
            }.AddComponent<Text>();
            ModText.text = "";
            ModText.fontSize = 20;
            ModText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            ModText.rectTransform.sizeDelta = new Vector2(450f, 1000f);
            ModText.alignment = TextAnchor.UpperLeft;
            ModText.rectTransform.localScale = new Vector3(0.00333333333f, 0.00333333333f, 0.33333333f);
            ModText.rectTransform.localPosition = new Vector3(-1f, -1f, -0.5f);
            ModText.material = AlertText;

            StatsText = new GameObject
            {
                transform =
                {
                    parent = HUDObj.transform
                }
            }.AddComponent<Text>();
            StatsText.text = "";
            StatsText.fontSize = 30;
            StatsText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            StatsText.rectTransform.sizeDelta = new Vector2(450f, 1000f);
            StatsText.alignment = TextAnchor.UpperRight;
            StatsText.rectTransform.localScale = new Vector3(0.00333333333f, 0.00333333333f, 0.33333333f);
            StatsText.rectTransform.localPosition = new Vector3(-1f, -1f, 0.5f);
            StatsText.material = AlertText;
        }

        public virtual void FixedUpdate()
        {
            try
            {
                if (!HasInit && Camera.main != null)
                {
                    Init();
                    HasInit = true;
                }

                HUDObj.GetComponent<CanvasScaler>().dynamicPixelsPerUnit = Settings.lowqualttext ? 1f : 2f;

                HUDObj2.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
                HUDObj2.transform.rotation = MainCamera.transform.rotation;
                try
                {
                    ModText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                    ModText.fontStyle = FontStyle.Italic;

                    NotifiText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                    NotifiText.fontStyle = FontStyle.Italic;

                    StatsText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                    StatsText.fontStyle = FontStyle.Italic;

                    if (Settings.advancedArraylist)
                        ModText.fontStyle = (FontStyle)((int)FontStyle.Italic % 2);
                }
                catch { }
                ModText.rectTransform.localPosition = new Vector3(-1f, -1f, Settings.flipArraylist ? 0.5f : -0.5f);
                ModText.alignment = Settings.flipArraylist ? TextAnchor.UpperRight : TextAnchor.UpperLeft;

                StatsText.rectTransform.localPosition = new Vector3(-1f, -1f, Settings.flipArraylist ? -0.5f : 0.5f);
                StatsText.alignment = Settings.flipArraylist ? TextAnchor.UpperLeft : TextAnchor.UpperRight;

                if (information.Count > 0)
                {
                    Color targetColor = Settings.backgroundColor.GetCurrentColor(); //GetIndex("Swap GUI Colors").enabled ? GetBDColor(0f) : GetBGColor(0f);

                    List<string> statsAlphabetized = information
                        .Select(item => $"<color=#{ColorToHex(targetColor)}>{item.Key}</color> <color=#{ColorToHex(Settings.textColors[0])}>{item.Value}</color>")
                        .OrderByDescending(item => item.Length)
                        .ToList();

                    StatsText.text = string.Join("\n", statsAlphabetized.ToArray());
                    StatsText.color = Color.white;

                    if (Settings.lowercaseMode)
                        StatsText.text = StatsText.text.ToLower();
                }
                else
                    StatsText.text = "";

                if (Settings.showEnabledModsVR)
                {
                    string enabledModsText = "";
                    List<string> alphabetized = new List<string>();
                    int categoryIndex = 0;
                    foreach (ButtonInfo[] buttonlist in Buttons.buttons)
                    {
                        foreach (ButtonInfo v in buttonlist)
                        {
                            try
                            {
                                if (v.enabled)
                                {
                                    string buttonText = v.overlapText ?? v.buttonText;

                                    if (Settings.lowercaseMode)
                                        buttonText = buttonText.ToLower();

                                    alphabetized.Add(buttonText);
                                }
                            }
                            catch { }
                        }
                        categoryIndex++;
                    }

                    ModText.text = enabledModsText;
                    ModText.color = Settings.backgroundColor.GetCurrentColor(); //GetIndex("Swap GUI Colors").enabled ? textColor : GetBGColor(0f);
                }
                else
                    ModText.text = "";

                if (Settings.lowercaseMode)
                {
                    ModText.text = ModText.text.ToLower();
                    NotifiText.text = NotifiText.text.ToLower();
                }
                //HUDObj.layer = GetIndex("Hide Notifications on Camera").enabled ? 19 : 0;
            }
            catch (Exception e) { Utility.Log(e.Message); }
        }

        public static void SendNotification(string NotificationText, float clearTime = -1f)
        {
            if (clearTime < 0)
                clearTime = notificationDecayTime; // make sure this is also in seconds now

            if (Settings.Notifications)
            {
                try
                {
                    NotifiCounter = 0;

                    PreviousNotifi = NotificationText;
                    if (!NotificationText.Contains(Environment.NewLine))
                        NotificationText += Environment.NewLine;

                    NotifiText.text += NotificationText;

                    MelonLoader.MelonCoroutines.Start(
                        TrackCoroutine(ClearHolder(clearTime))
                    );

                    if (Settings.lowercaseMode)
                        NotifiText.text = NotifiText.text.ToLower();

                    NotifiText.supportRichText = true;
                }
                catch (Exception e)
                {
                    Utility.Log($"Notification failed, object probably nil due to third person ; {NotificationText} {e.Message}");
                }
            }
        }

        public static void ClearAllNotifications() =>
            NotifiText.text = "";

        public static void ClearPastNotifications(int amount)
        {
            string text = "";
            foreach (string text2 in Enumerable.Skip(NotifiText.text.Split(Environment.NewLine.ToCharArray()), amount))
            {
                if (text2 != "")
                    text = text + text2 + "\n";
            }
            NotifiText.text = text;
        }

        private static IEnumerator TrackCoroutine(IEnumerator routine)
        {
            Coroutine self = null;

            IEnumerator Wrapper()
            {
                self = (Coroutine)MelonLoader.MelonCoroutines.Start(routine);
                clearCoroutines.Add(self);
                yield return self;
                clearCoroutines.Remove(self);
            }

            yield return Wrapper();
        }

        public static IEnumerator ClearHolder(float time = 1f)
        {
            yield return new WaitForSeconds(time);
            ClearPastNotifications(1);
        }

        public static void CancelClear(Coroutine coroutine)
        {
            if (clearCoroutines.Contains(coroutine))
            {
                clearCoroutines.Remove(coroutine);
                MelonLoader.MelonCoroutines.Stop(coroutine);
            }
        }

        public static List<Coroutine> clearCoroutines = new List<Coroutine> { };

    }
}