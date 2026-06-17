using GorillaNetworking;
using Photon.Pun;
using System;
using System.Reflection;
using System.Threading;
using UnityEngine;

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
            PhotonNetworkController phc = GameObject.Find("Photon Manager").GetComponent<PhotonNetworkController>();
            phc.disableAFKKick = true;
        }

        public static void Reconnect()
        {
            PhotonNetwork.Disconnect();
            PhotonNetworkController phc = GameObject.Find("Photon Manager").GetComponent<PhotonNetworkController>();
            phc.AttemptToJoinSpecificRoom(Menu.Main.lastRoom);
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


        public static void LobbyHop()
        {
            if (PhotonNetwork.InRoom)
                PhotonNetwork.Disconnect();
            PhotonNetwork.JoinRandomRoom();
        }


        public static void DisableNetworkTriggers(bool disable)
        {
            GameObject mountainsBetaTriggers = GameObject.Find("NetworkTriggers/Networking Trigger");
            GameObject hallandspringNetworkTriggers = GameObject.Find("Global/NetworkTriggers/Networking Trigger");

            if (mountainsBetaTriggers != null)
                mountainsBetaTriggers.SetActive(disable);

            if (hallandspringNetworkTriggers != null)
                hallandspringNetworkTriggers.SetActive(disable);
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
