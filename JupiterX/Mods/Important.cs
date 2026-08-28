using ExitGames.Client.Photon;
using GorillaNetworking;
using JupiterX.Notifications;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Reflection;
using System.Text;
using System.Threading;
using UnityEngine;
using static JupiterX.Menu.Main;

namespace JupiterX.Mods
{
    public class Important
    {
        public static void QuitGame()
        {
            Application.Quit();
        }
        public static void AntiAFK()
        {
            if (!Utility.photonNetworkController)
                Utility.photonNetworkController.disableAFKKick = true;
        }

        public static void JoinDiscord() =>
            Application.OpenURL("https://discord.gg/dtQdz59FJG");

        public static void Reconnect()
        {
            PhotonNetwork.Disconnect();
            Utility.photonNetworkController.AttemptToJoinSpecificRoom(Menu.Main.lastRoom);
        }

        public static void ConnectToRegion(string region)
        {
            string currentRegion = PhotonNetwork.CloudRegion;
            if (!string.IsNullOrEmpty(currentRegion))
                currentRegion = currentRegion.Replace("/*", "");
            if (currentRegion != region)
                PhotonNetwork.ConnectToRegion(region);
        }

        private static float lastTime;
        public static void CapFPS(int fps)
        {
            float targetDelta = 1f / fps;
            float elapsed = Time.realtimeSinceStartup - lastTime;

            if (elapsed < targetDelta)
            {
                int sleepMs = Mathf.FloorToInt((targetDelta - elapsed) * 1000);
                if (sleepMs > 0)
                    Thread.Sleep(sleepMs);
            }

            lastTime = Time.realtimeSinceStartup;
        }

        public static void UncapFPS()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = int.MaxValue;
        }

        public static void Leave()
        {
            Utility.photonNetworkController.AttemptDisconnect();
        }
        public static void Jrr()
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }
        public static void JoinCode(string code)
        {
            Utility.photonNetworkController.AttemptToJoinSpecificRoom(code);
        }

        public static void LobbyHop()
        {
            if (PhotonNetwork.InRoom)
                PhotonNetwork.Disconnect();
            PhotonNetwork.JoinRandomRoom();
        }

        private class RoomConfig
        {
            public bool open;
            public bool bigroomname;
            public bool maxplayers;
        }

        public static void CreateRoom()
        {
            RoomConfig roomconfig = new RoomConfig();
            prompts.Clear();

            Prompt("Would you like to make a public room ?", () => 
            {
                roomconfig.open = true;
                Prompt("Big Room Name ?", () =>
                {
                    roomconfig.bigroomname = true;
                    Prompt("255 Players ?", () =>
                    {
                        roomconfig.maxplayers = true;
                        Prompt("Create Room", () =>
                        {
                            NotificationManager.SendNotification("Creating room.");
                            Hashtable roomprops = new Hashtable();
                            roomprops.Add("gameMode", "forestDEFAULTDEFAULTINFECTIOINFECTION");
                            RoomOptions roomOptions = new RoomOptions()
                            {
                                CustomRoomProperties = roomprops,
                                PublishUserId = true,
                                IsOpen = roomconfig.open,
                                IsVisible = roomconfig.open,
                                MaxPlayers = roomconfig.maxplayers ? (byte)255 : (byte)10,
                                SuppressPlayerInfo = true
                            };
                            PhotonNetwork.CreateRoom(RandomRoomName(roomconfig.bigroomname), roomOptions);
                            NotificationManager.SendNotification("Created room.");
                        }, () =>
                        {
                            NotificationManager.SendNotification("Not creating room.");
                        });
                    }, () =>
                    {
                        roomconfig.maxplayers = false;
                        NotificationManager.SendNotification("Creating room");
                    });
                }, () =>
                {
                    roomconfig.bigroomname = false;
                });
            }, () => 
            {
                roomconfig.open = false;
                Prompt("Big Room Name ?", () =>
                {
                    roomconfig.bigroomname = true;
                    Prompt("255 Players ?", () =>
                    {
                        roomconfig.maxplayers = true;
                        Prompt("Create Room", () =>
                        {
                            NotificationManager.SendNotification("Creating room.");
                            Hashtable roomprops = new Hashtable();
                            roomprops.Add("gameMode", "forestDEFAULTDEFAULTINFECTIOINFECTION");
                            RoomOptions roomOptions = new RoomOptions()
                            {
                                CustomRoomProperties = roomprops,
                                PublishUserId = true,
                                IsOpen = roomconfig.open,
                                IsVisible = roomconfig.open,
                                MaxPlayers = roomconfig.maxplayers ? (byte)255 : (byte)10,
                                SuppressPlayerInfo = true
                            };
                            PhotonNetwork.CreateRoom(RandomRoomName(roomconfig.bigroomname), roomOptions);
                            NotificationManager.SendNotification("Created room.");
                        }, () =>
                        {
                            NotificationManager.SendNotification("Not creating room.");
                        });
                    }, () =>
                    {
                        roomconfig.maxplayers = false;
                        NotificationManager.SendNotification("Creating room");
                    });
                }, () =>
                {
                    roomconfig.bigroomname = false;
                });
            });
        }

        public static string RandomRoomName(bool bigroomname = false)
        {
            while (true)
            {
                string text = Generate(bigroomname ? 22 : 4);
                if (GorillaComputer.instance.CheckAutoBanListForName(text))
                    return text;
            }
        }

        private static readonly System.Random Random = new System.Random();
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string Generate(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
                result.Append(Characters[Random.Next(Characters.Length)]);
            return result.ToString();
        }

        public static void DisableNetworkTriggers(bool disable)
        {
            GameObject mountainsBetaTriggers = GameObject.Find("NetworkTriggers/Networking Trigger");
            GameObject hallandspringNetworkTriggers = GameObject.Find("Global/NetworkTriggers/Networking Trigger");
            if (mountainsBetaTriggers != null)
                mountainsBetaTriggers.SetActive(!disable);
            if (hallandspringNetworkTriggers != null)
                hallandspringNetworkTriggers.SetActive(!disable);
        }

        public static void Turning()
        {
            Vector2 axis = Utility.RightJoystickAxis;
            if (axis.x > 0.6f)
                Turn(6f);
            if (axis.x < -0.6f)
                Turn(-6f);
        }

        private static void Turn(float degrees)
        {
            var playerType = Type.GetType("GorillaLocomotion.Player, Assembly-CSharp") ?? Type.GetType("GLocomotion.Player, Assembly-CSharp");
            if (playerType == null)
                return;
            var playerField = playerType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
            object playerInstance = playerField?.GetValue(null);
            if (playerInstance == null)
                return;
            var turnMethod = playerType.GetMethod("Turn", BindingFlags.Public | BindingFlags.Instance);
            if (turnMethod == null)
                return;
            turnMethod.Invoke(playerInstance, new object[] { degrees });
        }
    }
}
