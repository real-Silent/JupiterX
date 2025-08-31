﻿using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX
{
    public class NotificationManager // og creds zinx - saturn - some others idfk atp
    {
        public static float normtime = 0f;
        public static float cooldown = 0.2f;
        public static float deltaTime;
        public static GameObject Notifications;
        public static GameObject Notifications2;
        public static GameObject MainCamera;
        public static Text Text;
        public static Color DropColor = Color.black;
        public static string PreviousNotifi;
        public static string[] Notifilines;
        public static int NotificationDecayTime = 221;
        public static int NotificationDecayTimeCounter = 221;
        public static int NoticationThreshold = 5;
        public static string newtext;
        public static float ropedelay;
        private static Text text_0;

        public static void LoadNotifications()
        {
            if (Notifications == null)
            {
                MainCamera = GameObject.Find("Main Camera");
                Notifications = new GameObject();
                Notifications2 = new GameObject();
                Notifications2.name = "CLIENT_HUB_NOTIFICATIONS_MAX";
                Notifications.name = "CLIENT_HUB_NOTIFICATIONS_MAX";
                Notifications.AddComponent<Canvas>();
                Notifications.AddComponent<CanvasScaler>();
                Notifications.AddComponent<GraphicRaycaster>();
                Notifications.GetComponent<Canvas>().enabled = true;
                Notifications.GetComponent<Canvas>().worldCamera = MainCamera.GetComponent<Camera>();
                Notifications.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 5f);
                Notifications.GetComponent<RectTransform>().position = MainCamera.transform.position;
                Notifications2.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z - 4.6f);
                Notifications.transform.parent = Notifications2.transform;
                Notifications.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1.6f);
                Vector3 eulerAngles = Notifications.GetComponent<RectTransform>().rotation.eulerAngles;
                eulerAngles.y = -270f;
                Notifications.transform.localScale = Vector3.one;
                Notifications.GetComponent<RectTransform>().rotation = Quaternion.Euler(eulerAngles);

                Text = new GameObject { transform = { parent = Notifications.transform } }.AddComponent<Text>();
                Text.fontSize = 10;
                Text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                Text.rectTransform.sizeDelta = new Vector2(260f, 160f);
                Text.rectTransform.localScale = new Vector3(0.01f, 0.01f, 1f);
                Text.rectTransform.localPosition = new Vector3(-2.4f, -1.5f, 0f);
                Text.material = new Material(Shader.Find("GUI/Text Shader"));
                text_0 = Text;
            }


            if (!Settings.disableNotis)
            {
                if (Text.text != "")
                {
                    NotificationDecayTimeCounter++;
                    if (NotificationDecayTimeCounter > NotificationDecayTime)
                    {
                        Notifilines = null;
                        newtext = "";
                        NotificationDecayTimeCounter = 0;
                        Notifilines = Text.text.Split(Environment.NewLine.ToCharArray()).Skip(1).ToArray();
                        foreach (string line in Notifilines)
                        {
                            if (line != "")
                            {
                                newtext += line + "\n";
                            }
                        }
                        Text.text = newtext;
                    }
                }
                else
                {
                    NotificationDecayTimeCounter = 0;
                }
            }
       
            if (Notifications2 != null)
            {
                Notifications2.transform.position = MainCamera.transform.position;
                Notifications2.transform.rotation = MainCamera.transform.rotation;
            }
        }

        public static void SendNoti(string notiText)
        {
            if (!Settings.disableNotis)
            {
                if (Time.time - normtime > cooldown)
                {
                    normtime = Time.time;
                    notiText = $"{notiText}{Environment.NewLine}";
                    Text.text += notiText;
                    PreviousNotifi = notiText;
                    Text.color = Color.white;
                }
            }
        }


        public static void SendNotification(string colortt, string Type, string NotificationText)
        {
            try
            {
                if (!Settings.disableNotis)
                {
                    if (Time.time - normtime > cooldown)
                    {
                        normtime = Time.time;
                        NotificationText = $"[<color={colortt}>{Type}</color>] {NotificationText}{Environment.NewLine}";
                        Text.text += NotificationText;
                        PreviousNotifi = NotificationText;
                        Text.color = Color.white;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public static void SendNotification2(string NotificationText)
        {
            try
            {
                if (!Settings.disableNotis)
                {
                    if (Time.time - normtime > cooldown)
                    {
                        normtime = Time.time;
                        NotificationText = $"{NotificationText}{Environment.NewLine}";
                        Text.text += NotificationText;
                        PreviousNotifi = NotificationText;
                        Text.color = Color.white;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public static void SendNotification1(string Type, string NotificationText)
        {
            try
            {
                if (Time.time - normtime > cooldown)
                {
                    normtime = Time.time;
                    NotificationText = "[<color=red>" + Type + "</color>] > " + NotificationText;
                    if (!NotificationText.Contains(Environment.NewLine))
                    {
                        NotificationText += Environment.NewLine;
                    }
                    Text.text += NotificationText;
                    PreviousNotifi = NotificationText;
                    Text.color = Color.yellow;
                }
            }
            catch
            {
                throw;
            }
        }

        public static void ClearAllNotifications()
        {
            Text.text = "";
        }

        public static void ClearPastNotifications(int amount)
        {
            string result = "";
            string[] lines = Text.text.Split(Environment.NewLine.ToCharArray()).Skip(amount).ToArray();
            foreach (string line in lines)
            {
                if (line != "")
                {
                    result += line + "\n";
                }
            }
            Text.text = result;
        }
    }
}