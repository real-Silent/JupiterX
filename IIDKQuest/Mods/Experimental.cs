using easyInputs;
using ExitGames.Client.Photon;
using GorillaNetworking;
using HarmonyLib;
using Il2CppSystem;
using Il2CppSystem.Net;
using JupiterX.Classes;
using JupiterX.Menu;
using Photon.Pun;
using PlayFab;
using System.IO;
using System.Linq;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.UI;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX.Mods
{
    internal class Experimental
    {
        public static void BalloonSpam()
        {
            if (PhotonNetwork.InRoom)
            {
                if (EasyInputs.GetTriggerButtonDown(EasyHand.RightHand))
                {
                    foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerListOthers)
                    {
                        PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                        foreach (BalloonHoldable balloonHoldable in GameObject.FindObjectsOfType<BalloonHoldable>())
                        {
                            balloonHoldable.OnOwnerChangeCb(PhotonNetwork.LocalPlayer, player);
                            balloonHoldable.OwnerPopBalloon();
                            balloonHoldable.PopBalloon();
                            balloonHoldable.OnActivate();
                            VRRig[] rigs = GorillaParent.instance.vrrigs.ToArray();
                            for (int i = 0; i < rigs.Length; i++)
                            {
                                balloonHoldable.OnHover(null, rigs[i].rightHandTransform.gameObject);
                                balloonHoldable.OwnerPopBalloon();
                            }
                            balloonHoldable.OwnerPopBalloon();
                            balloonHoldable.PopBalloonRemote();
                            balloonHoldable.photonView.RPC("RPCWorldShareable", RpcTarget.All, null);
                        }
                    }
                }
            }
        }

        public static void SetGameMode(string gameModeHash)
        {
            Hashtable roomHash = new Hashtable();
            //roomHash.Add("forestcitycanyoncavesmountainsskyjungle", "gameMode" + GorillaComputer.instance.currentQueue + gameModeHash);
            roomHash.Add("gameMode", gameModeHash);
            Utility.SetMaster(Utility.MyPlayer());
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
        }

        public static void SpazForestTargets()
        {
            foreach (HitTargetWithScoreCounter target in GameObject.FindObjectsOfType<HitTargetWithScoreCounter>())
            {
                target.digitsChange = true;
                target.hitCooldownTime = 0;
                target.UpdateTargetState();
            }
        }

        public static void UnBanSelf()
        {
            string titleId = PlayFabSettings.TitleId;      
            string customId = Utility.Generate(16);    

            string url = $"https://{titleId}.playfabapi.com/Client/LoginWithCustomID";

            // JSON payload
            string jsonData = $@"{{
            ""CustomId"": ""{customId}"",
            ""CreateAccount"": true,
            ""TitleId"": ""{titleId}""
        }}";

            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/json");

            try
            {
                string response = client.UploadString(url, "POST", jsonData);
                OnLogin();
            }
            catch
            {
                OnError();
            }
        }

        static void OnError()
        {
            NotificationManager.SendNotification("red", "ERROR", "Unable to connect to playfab");
        }

        static void OnLogin()
        {
            GorillaTagger.Instance.offlineVRRig.GetUserCosmeticsAllowed();
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.ConnectToRegion("usw");
            NotificationManager.SendNotification("green", "LOGIN", "Successfully logged in");
            PhotonNetworkController phc = GameObject.Find("Photon Manager").GetComponent<PhotonNetworkController>();
            phc.InitiateConnection();
        }

        public static void GetFuckedNetPlayers()
        {
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.98f, -64.83f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.98f, -65.17f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.98f, -66.25f), Quaternion.Euler(270.50f, 180.00f, 180.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.17f, -66.25f), Quaternion.Euler(270.50f, 180.00f, 180.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.70f, -65.71f), Quaternion.Euler(88.75f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.36f, -65.71f), Quaternion.Euler(88.75f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.33f, -62.22f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.98f, -64.98f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.98f, -65.52f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.50f, -67.36f), Quaternion.Euler(88.75f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.85f, -67.04f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.92f, -62.41f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.89f, -67.18f), Quaternion.Euler(46.59f, 180.00f, 180.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.75f, -67.31f), Quaternion.Euler(46.59f, 180.00f, 180.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.67f, -62.41f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.51f, -65.71f), Quaternion.Euler(88.75f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.83f, -66.40f), Quaternion.Euler(323.86f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.34f, -66.38f), Quaternion.Euler(308.60f, 180.00f, 180.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.74f, -63.55f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.74f, -63.74f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.50f, -62.41f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.85f, -66.84f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.70f, -67.36f), Quaternion.Euler(88.75f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.88f, -67.14f), Quaternion.Euler(48.74f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.17f, -67.36f), Quaternion.Euler(88.75f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.36f, -67.36f), Quaternion.Euler(88.75f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.90f, -66.96f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.98f, -65.33f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.19f, -66.26f), Quaternion.Euler(308.60f, 180.00f, 180.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.90f, -66.36f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.90f, -66.56f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.94f, -65.71f), Quaternion.Euler(88.75f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.75f, -65.71f), Quaternion.Euler(88.75f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.95f, -66.25f), Quaternion.Euler(323.86f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.84f, -62.41f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.50f, -61.26f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.98f, -64.13f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 3.00f, -62.60f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.98f, -63.54f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.52f, -65.33f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.52f, -64.98f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.52f, -65.17f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.52f, -64.63f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.52f, -65.52f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.52f, -64.83f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 3.00f, -62.41f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.16f, -62.41f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.50f, -62.60f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.49f, -63.55f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.76f, -63.74f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.67f, -62.60f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.84f, -60.95f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.49f, -63.74f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.76f, -63.55f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.84f, -62.60f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.84f, -61.31f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.23f, -63.74f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 3.00f, -62.24f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.33f, -61.26f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.23f, -63.55f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.74f, -62.41f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.95f, -63.55f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.95f, -63.74f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 3.00f, -62.05f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.16f, -62.60f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.84f, -60.70f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 3.00f, -61.75f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 3.00f, -61.94f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.67f, -61.26f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.33f, -62.00f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 3.26f, -61.52f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.87f, -61.26f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.98f, -63.16f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.33f, -62.60f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.04f, -65.71f), Quaternion.Euler(88.75f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.85f, -65.71f), Quaternion.Euler(88.75f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.98f, -60.48f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.98f, -63.74f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.85f, -65.52f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.85f, -65.33f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.98f, -63.93f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.85f, -64.98f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.33f, -62.41f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.85f, -65.17f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.98f, -63.35f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.92f, -62.60f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.85f, -64.63f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.74f, -62.60f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.85f, -64.83f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.98f, -64.63f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.85f, -66.44f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.85f, -66.63f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.17f, -65.71f), Quaternion.Euler(88.75f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.97f, -67.34f), Quaternion.Euler(48.74f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.82f, -67.22f), Quaternion.Euler(48.74f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.90f, -66.77f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.03f, -61.31f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 3.01f, -61.26f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 3.01f, -61.06f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 3.01f, -60.81f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.14f, -61.31f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 3.01f, -60.61f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.52f, -60.48f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.79f, -60.48f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 1.84f, -61.20f), Quaternion.Euler(0.00f, 0.00f, 0.00f));
            PhotonNetwork.Instantiate("Network Player", new Vector3(-56.75f, 2.27f, -60.48f), Quaternion.Euler(0.00f, 0.00f, 0.00f));

        }

        public static void ChangeMatIndexAll()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                Utility.BetaSetIndex(1, rig);
            }
        }

        public static void GetFucked() 
        {
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.03f, -66.21f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.30f, -67.61f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.59f, -64.67f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.84f, -66.21f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.48f, -63.55f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.84f, -62.50f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.02f, -60.26f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.02f, -67.22f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.72f, -62.50f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.02f, -67.03f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.35f, -65.72f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.02f, -66.95f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.03f, -65.68f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.62f, -58.71f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.62f, -58.26f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.83f, -58.71f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.27f, -58.71f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.22f, -61.75f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.84f, -66.68f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.48f, -63.33f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.03f, -65.77f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.56f, -60.26f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.95f, -63.29f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.27f, -66.21f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.06f, -66.21f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.08f, -66.82f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.81f, -60.26f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.75f, -67.24f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.21f, -60.26f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.62f, -65.76f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.56f, -62.01f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.82f, -64.67f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.75f, -66.81f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.29f, -63.55f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.03f, -65.98f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.87f, -67.44f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.71f, -63.55f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.89f, -67.41f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.56f, -61.75f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.99f, -59.35f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.27f, -62.50f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.75f, -67.06f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.51f, -67.61f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.83f, -66.21f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.35f, -57.66f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.97f, -62.50f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.62f, -65.59f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.71f, -67.61f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.62f, -66.00f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.07f, -67.61f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.59f, -63.55f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.99f, -61.75f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.85f, -63.55f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.95f, -63.55f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.40f, -60.26f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.67f, -66.21f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.62f, -58.09f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.06f, -58.71f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.50f, -58.71f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.56f, -61.34f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.50f, -66.21f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.05f, -56.31f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.62f, -66.21f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.77f, -60.26f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.00f, -65.09f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.78f, -61.34f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.75f, -57.66f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.03f, -65.68f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.38f, -61.34f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.04f, -64.67f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.78f, -61.75f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.35f, -65.98f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.50f, -62.50f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.29f, -63.55f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.48f, -63.07f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.91f, -60.64f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.59f, -60.26f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.00f, -64.90f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.70f, -59.71f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.00f, -64.68f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.00f, -64.43f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.20f, -61.34f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.07f, -63.55f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.20f, -59.85f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.60f, -56.95f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.95f, -63.09f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.92f, -61.75f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.58f, -57.66f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.90f, -60.26f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.00f, -64.25f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.79f, -64.67f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.67f, -61.75f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.55f, -64.67f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.75f, -61.34f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.29f, -64.67f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.56f, -61.34f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.16f, -57.66f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.56f, -62.28f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.02f, -57.66f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.78f, -61.75f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.91f, -61.34f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.81f, -57.66f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.96f, -57.66f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.60f, -57.21f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.99f, -61.34f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.95f, -62.90f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.04f, -62.50f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.45f, -61.75f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.57f, -62.50f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.84f, -58.71f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.03f, -58.48f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.35f, -58.48f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.60f, -57.66f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.60f, -57.44f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.91f, -61.10f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.40f, -60.02f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.91f, -60.83f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.60f, -56.73f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.00f, -57.25f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.02f, -59.65f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.76f, -59.48f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.91f, -60.64f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.56f, -61.34f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.56f, -60.84f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.55f, -59.89f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.55f, -60.64f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.55f, -61.10f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.67f, -58.71f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.03f, -58.71f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.03f, -58.27f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.03f, -58.18f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.35f, -58.22f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.62f, -58.50f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.00f, -57.46f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.60f, -59.30f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.82f, -59.53f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.72f, -56.59f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 6.86f, -56.45f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.25f, -56.31f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.43f, -56.31f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.58f, -56.31f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.71f, -56.48f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 7.84f, -56.68f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.00f, -57.05f), Quaternion.Euler(40.00f, 90.00f, 0.00f));
            PhotonNetwork.Instantiate("STICKABLE TARGET", new Vector3(-60.36f, 8.00f, -56.83f), Quaternion.Euler(40.00f, 90.00f, 0.00f));

        }

        public static void CumAll()
        {
            if (Utility.RTrigger)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                    {
                        PhotonNetwork.Instantiate("bulletPrefab", rig.transform.position + new Vector3(0, 0, 0.6f), rig.headConstraint.transform.rotation);
                    }
                }
            }
        }
        public static void eternalsugercookieSpammer()
        {
            if (Utility.RTrigger)
            {
                Utility.BetaSpawnPrefab("Eternal Sugar Cookie", Utility.RightHandTransform().position, Utility.RightHandTransform().rotation);
            }
        }

        static bool spawnedMeowOnce = false;
        static bool spawnedDickOnce = false;
        public static void MeowMeowCubeSpawn()
        {
            if (Utility.RTrigger)
            {
                if (!spawnedMeowOnce)
                {
                    Utility.BetaSpawnPrefab("MeowMeowMeowMeow", Utility.RightHandTransform().position, Utility.RightHandTransform().rotation);
                    spawnedMeowOnce = true;
                }
            }
            else
            {
                spawnedMeowOnce = false;
            }
        }
        public static void DickSpawn()
        {
            if (Utility.RTrigger)
            {
                if (!spawnedDickOnce)
                {
                    Utility.BetaSpawnPrefab("jstrumpchill", Utility.RightHandTransform().position, Utility.RightHandTransform().rotation);
                    spawnedDickOnce = true;
                }
            }
            else
            {
                spawnedDickOnce = false;
            }
        }

        public static void MuteGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
                RaycastHit Ray = GunData.Ray;


                if (Main.gunLocked && Main.lockTarget != null)
                {
                    Main.lockTarget.muted = true;
                }

                if (Main.GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who)
                    {
                        Main.gunLocked = true;
                        Main.lockTarget = who;
                    }
                }
            }
            else
            {
                if (Main.gunLocked)
                    Main.gunLocked = false;
                JupiterX.Menu.Main.DestroyGun();
            }
        }


        public static void DoPlayerStuff()
        {
            foreach (Photon.Realtime.Player plr in PhotonNetwork.PlayerListOthers)
            {
                RigManager.GetPlayerInfoAsString(plr);
            }
        }

        public static void UnMuteGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
                RaycastHit Ray = GunData.Ray;


                if (Main.gunLocked && Main.lockTarget != null)
                {
                    Main.lockTarget.muted = false;
                }

                if (Main.GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who)
                    {
                        Main.gunLocked = true;
                        Main.lockTarget = who;
                    }
                }
            }
            else
            {
                if (Main.gunLocked)
                    Main.gunLocked = false;
                JupiterX.Menu.Main.DestroyGun();
            }
        }

        public static void MuteAll()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                {
                    rig.muted = true;
                }
            }
        }
        public static void UnMuteAll()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                {
                    rig.muted = false;
                }
            }
        }
    }
}
