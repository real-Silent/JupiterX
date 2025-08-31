using easyInputs;
using ExitGames.Client.Photon;
using GorillaNetworking;
using Harmony;
using HarmonyLib;
using Il2CppSystem.Net;
using JupiterX.Classes;
using JupiterX.Menu;
using JupiterX.Mods;
using Mono.Cecil;
using Mono.CSharp;
using OVR;
using Photon.Pun;
using PlayFab;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;


// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX
{
    // plz ignore all this shitty ass code you see >w<
    public class Utility
    {
        public static void Log(string msg, int type)
        {
            switch (type)
            {
                case 0: File.WriteAllText(LogPath, LogMain + msg); break;
                case 1: File.WriteAllText(LogPath, LogWarningMain + msg); break;
                case 2: File.WriteAllText(LogPath, LogSuccessMain + msg); break;
                case 3: File.WriteAllText(LogPath, LogErrorMain + msg); break;
            }
        }

        public static void MoveStumpTextGun()
        {
            if (Menu.Main.GetGunInput(false))
            {
                var GunData = Menu.Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;

                if (Menu.Main.GetGunInput(true))
                {
                    Plugin.StumpText.transform.position = NewPointer.transform.position + new Vector3(0, 0.7f, 0);
                }
            }
            else
            {
                Main.DestroyGun();
            }
        }


        static Hashtable jupiterxProp = new Hashtable();
        public static void ThisGuyIsUsingJupiter()
        {
            if (!jupiterxProp.ContainsKey("jupiterxusersosigma"))
            {
                jupiterxProp.Add("jupiterxusersosigma", "jupiterxusersosigma");
            }

            if (PhotonNetwork.InRoom)
            {
                if (!GorillaTagger.Instance.myVRRig.photonView.Controller.CustomProperties.ContainsKey("jupiterxusersosigma"))
                {
                    GorillaTagger.Instance.myVRRig.photonView.Controller.SetCustomProperties(jupiterxProp);
                }

                if (GorillaTagger.Instance.myVRRig.photonView.Controller.CustomProperties.ContainsKey("jupiterxusersosigma"))
                {
                    string name = "[JUPITERX] " + GorillaTagger.Instance.myVRRig.photonView.Controller.NickName;
                    GorillaTagger.Instance.myVRRig.playerText.text = name;
                    GorillaTagger.Instance.myVRRig.playerText.color = Color.yellow;
                    GorillaTagger.Instance.offlineVRRig.playerText.text = name;
                    GorillaTagger.Instance.offlineVRRig.playerText.color = Color.yellow;
                }

                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                    {
                        string nickname = rig.photonView.Owner.NickName;
                        string userId = rig.photonView.Owner.UserId;

                        if (rig.photonView.Controller.CustomProperties.ContainsKey("jupiterxusersosigma"))
                        {
                            rig.playerText.text = "[JUPITERX] " + nickname;
                            rig.playerText.color = Color.yellow;
                        }
                    }
                }
            }
        }


        static void OnError(PlayFabError re)
        {
            NotificationManager.SendNotification("red", "FAILED", $"Error {re.Error} , EMessage: {re.ErrorMessage}");
        }

        public static PhotonView GetOwnerShip(Photon.Realtime.Player newOwner)
        {
            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
            {
                PhotonView objectView = obj.GetComponent<PhotonView>();
                if (objectView != null)
                {
                    objectView.Owner = newOwner;
                    objectView.Controller = newOwner;
                    return objectView;
                }
            }
            return null;
        }

        public static void DestroyAllPhotonViews()
        {
            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
            {
                SetMaster(MyPlayer());
                PhotonView objectView = obj.GetComponent<PhotonView>();
                if (objectView != null)
                {
                    PhotonNetwork.Destroy(objectView);
                }
            }
        }

        public static Photon.Realtime.Player MyPlayer()
        {
            return PhotonNetwork.LocalPlayer;
        }

        public static void BanAll()
        {
            Plugin.DoCoroun(BetaBanAllWithDelay());

            if (Utility.RTrigger)
            {
                Main.GetIndex("Ban All").enabled = false;
            }
        }

        static System.Collections.IEnumerator BetaBanAllWithDelay()
        {
            yield return new WaitForSeconds(2);
            foreach (Photon.Realtime.Player plr in PhotonNetwork.PlayerListOthers)
            {
                BetaBanAll(plr.UserId);
            }
        }

        public static void BetaBanAll(string userid) // release december 1st 25 -- current date 31/08/2025 releasing today ig
        {
            string photonId = userid;
            string titleId = PlayFabSettings.staticSettings.TitleId;
            string msg = "Modding";
            string rsn = "Cheating";

            string url = "https://api-colossal-quest.vercel.app/api/runcloudscript"; // abuse this i will take it down

            WebClient client = new WebClient();
            try
            {
                client.Headers.Add("Content-Type", "application/json");
                string payload = $"{{\"playerId\":\"{photonId}\", \"titleId\":\"{titleId}\", \"msg\":\"{msg}\", \"rsn\":\"{rsn}\"}}";

                string response = client.UploadString(url, "POST", payload);
                MelonLoader.MelonLogger.Msg("Cloud Script Response: " + response);
            }
            catch (Exception ex)
            {
                MelonLoader.MelonLogger.Error("Error sending Cloud Script: " + ex.Message);
            }
        }

        public static void BetaTPToSling()
        {
            Slingshot slingshot = GorillaTagger.Instance.offlineVRRig.slingshot;
            if (slingshot != null)
            {
                SlingshotProjectile slingproj = slingshot.projectilePrefab.GetComponent<SlingshotProjectile>();
                if (slingproj != null)
                {
                    GorillaLocomotion.Player.Instance.transform.position = slingproj.transform.position;
                }
            }
        }

        public static string FindVRRigFromPlayerId(VRRig who)
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                    {
                        return rig.photonView.Owner.UserId;
                    }
                }
            }
            return null;
        }

        public static void BetaSpamMuteAll()
        {
            for (int I = 0; I < 9; I++)
            {
                lastfreezegarbadge = !lastfreezegarbadge;
                foreach (GorillaPlayerScoreboardLine line in GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>())
                {
                    line.SetReportState(true, GorillaPlayerLineButton.ButtonType.Mute);
                    line.muteButton.testPress = lastfreezegarbadge;
                }
            }
        }

        public static void BetaCrashAllV2(VRRig target) 
        {
            Utility.SetMaster(PhotonNetwork.LocalPlayer);
            if (target != null)
            {
                PhotonNetwork.Destroy(target.photonView);
                PhotonNetwork.DestroyPlayerObjects(target.photonView.Owner);
                PhotonNetwork.DestroyPlayerObjects(target.photonView.Controller);
                PhotonNetwork.SendDestroyOfPlayer(target.photonView.Owner.ActorNumber);
                PhotonNetwork.SendDestroyOfPlayer(target.photonView.Controller.ActorNumber);
            }
        }

        static bool lastfreezegarbadge;
        public static void PacketStresser()
{
            for (int I = 0; I < 9; I++)
            {
                lastfreezegarbadge = !lastfreezegarbadge;
                foreach (GorillaPlayerScoreboardLine line in GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>())
                {
                    line.muteButton.testPress = lastfreezegarbadge;
                    line.SetReportState(lastfreezegarbadge, GorillaPlayerLineButton.ButtonType.Mute);
                }
            }
        }

        private static readonly System.Random _random = new System.Random();
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public static string Generate(int length)
        {
            char[] buffer = new char[length];
            for (int i = 0; i < length; i++)
            {
                buffer[i] = _chars[_random.Next(_chars.Length)];
            }
            return new string(buffer);
        }
        public static GorillaTagManager BetaDoSmthWithTag(int wat, Photon.Realtime.Player whoToTag = null)
        {
            GorillaTagManager tagman = GorillaGameManager.instance.GetComponent<GorillaTagManager>();

            switch (wat)
            {
                case 0: tagman.InfectionEnd(); break;
                case 1: tagman.UpdateInfectionState(); break;
                case 2: tagman.UpdateTagState(); break;
                case 3: tagman.SetisCurrentlyTag(true); break;
                case 4: tagman.ChangeCurrentIt(whoToTag); break;
            }
            return tagman;
        }


        public static void FixGhostRig()
        {
            if (myVRRig().enabled == false)
                myVRRig().enabled = true;
        }

        static Vector3 closePosition;
        public static void FreezePlayerInMenu()
        {
            if (Main.menu != null)
            {
                if (closePosition == Vector3.zero)
                    closePosition = GorillaTagger.Instance.GetComponent<Rigidbody>().transform.position;
                else
                    GorillaTagger.Instance.GetComponent<Rigidbody>().transform.position = closePosition;
                GorillaTagger.Instance.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            }
            else
                closePosition = Vector3.zero;
        }
        public static void GhostInMenu()
        {
            if (Main.menu != null)
                myVRRig().enabled = false;
            else
                myVRRig().enabled = true;
        }

        public static bool hasTriggeredOnceL = false;
        public static bool hasTriggeredOnceR = false;

        public static string[] PageTypes = { "Side", "Bottom", "Triggers" };
        public static int PageType = 0;
        public static bool isTriggers = false;
        public static Vector3 PageObjectPosRight = new Vector3(0.56f, 0.65f, 0);
        public static Vector3 PageObjectPosLeft = new Vector3(0.56f, -0.65f, 0);
        public static Vector3 PageTextPosRight = new Vector3(0.064f, -0.195f, 0f);
        public static Vector3 PageTextPosLeft = new Vector3(0.064f, 0.195f, 0f);
        public static Vector3 PageObjScale = new Vector3(0.09f, 0.2f, 0.9f);
        public static void ChangePageType()
        {
            PageType = (PageType + 1) % PageTypes.Length;

            switch (PageType)
            {
                case 0:
                    isTriggers = false;
                    PageObjectPosRight = new Vector3(0.56f, 0.65f, 0);
                    PageObjectPosLeft = new Vector3(0.56f, -0.65f, 0);
                    PageObjScale = new Vector3(0.09f, 0.2f, 0.9f);
                    PageTextPosLeft = new Vector3(0.064f, 0.195f, 0f);
                    PageTextPosRight = new Vector3(0.064f, -0.195f, 0f);
                    break;
                case 1:
                    isTriggers = false;
                    PageObjectPosRight = new Vector3(0.56f, -0.44f, -0.6f);
                    PageObjectPosLeft = new Vector3(0.56f, 0.44f, -0.6f);
                    PageTextPosLeft = new Vector3(0.062f, 0.132f, -0.23f);
                    PageTextPosRight = new Vector3(0.062f, -0.130f, -0.23f);
                    PageObjScale = new Vector3(0.1f, 0.2f, 0.1f);
                    break;
                case 2:
                    isTriggers = true;
                    PageObjectPosRight = new Vector3(0f, -0f, -0f);
                    PageObjectPosLeft = new Vector3(0f, 0f, -0f);
                    PageObjScale = new Vector3(0, 0, 0);
                    PageTextPosLeft = new Vector3(222222f, -22222222f, -222222222f);
                    PageTextPosRight = new Vector3(222222f, -22222222f, -222222222f);
                    break;
            }
            Main.GetIndex("Change Page Type").overlapText = "Change Page Type <color=cyan>[" + PageTypes[PageType] + "]</color>"; 
        }

        public static void BetaEmojiName(int emoji)
        {
            PhotonNetwork.LocalPlayer.NickName = "\n\n<size=4532><sprite=" + emoji + "></size>";
        }

        public static void BetaSpawnPrefab(string prefabName, Vector3 Position, Quaternion Roation)
        {
            //PhotonNetwork.MAX_VIEW_IDS = 222222222; // .int.MaxValue // breaks photon 
            PhotonNetwork.Instantiate(prefabName, Position, Roation, 0, null);
        }
        public static void SetMaster(Photon.Realtime.Player newMaster)
        {
            PhotonNetwork.SetMasterClient(newMaster);
            GorillaNot.instance.currentMasterClient = newMaster;
            GorillaNot.instance.OnMasterClientSwitched(newMaster);
        }

        public static void MakeMeMaster()
        {
            if (Utility.IsMaster() == false)
                Utility.SetMaster(Utility.MyPlayer());
        }


        static GameObject sphereeR = null;
        static GameObject sphereeL = null;

        public static void GhostView(bool enabled)
        {
            if (enabled)
            {
                if (sphereeL == null)
                {
                    sphereeL = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphereeL.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    sphereeL.transform.SetParent(Utility.LeftHandTransform(), false);
                    sphereeL.transform.localRotation = Quaternion.identity;
                    sphereeL.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    sphereeL.GetComponent<Renderer>().material.color = Color.grey;
                    GameObject.Destroy(sphereeL.GetComponent<Collider>());
                }

                if (sphereeR == null)
                {
                    sphereeR = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphereeR.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    sphereeR.transform.SetParent(Utility.RightHandTransform(), false);
                    sphereeR.transform.localRotation = Quaternion.identity;
                    sphereeR.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    sphereeR.GetComponent<Renderer>().material.color = Color.grey;
                    GameObject.Destroy(sphereeR.GetComponent<Collider>());
                }
            }
            else
            {
                if (sphereeL != null)
                {
                    GameObject.Destroy(sphereeL);
                    sphereeL = null;
                }
                if (sphereeR != null)
                {
                    GameObject.Destroy(sphereeR);
                    sphereeR = null;
                }
            }
        }


        public static bool IsMaster()
        {
            return PhotonNetwork.IsMasterClient;
        }

        public static void BetaDestroyPlayers(Photon.Realtime.Player who)
        {
            if (!IsMaster())
                SetMaster(PhotonNetwork.LocalPlayer);

            PhotonNetwork.DestroyPlayerObjects(who);
            PhotonNetwork.DestroyPlayerObjects(who);
        }

        static List<GameObject> Prefabs = new List<GameObject>();
        public static void BetaDoPrefab(string prefabName)
        {
            GameObject prefab = PhotonNetwork.Instantiate(prefabName, Vector3.zero, Quaternion.identity);
            Prefabs.Add(prefab);
            foreach (GameObject gameObject in Prefabs)
            {
                if (gameObject != null)
                    GameObject.Destroy(gameObject);
            }
        }

        static string[] RPCNames = { "SetTaggedTime", "UpdatePlayerCosmetics", "RequestCosmetics", "ReportTagRPC" };
        static string[] prefabNames = { "gorillaprefabs/gorillaenemy", "Network Player", "STICKABLE TARGET", "bulletPrefab" };

        public static void SlowPlayer(Photon.Realtime.Player who)
        {
            Utility.myVRRig().photonView.RPC("SetTaggedTime", who, null);
        }

        public static void TagPlayer(Photon.Realtime.Player who)
        {
            foreach (GorillaTagManager tagman in GameObject.FindObjectsOfType<GorillaTagManager>())
            {
                MakeMeMaster();
                tagman.AddInfectedPlayer(who);
                tagman.AddInfectedPlayer(who);
                tagman.AddInfectedPlayer(who);
            }
        }
        public static void BetaCrashPlayer(Photon.Realtime.Player crash)
        {
            myVRRig().photonView.RPC(RPCNames[0], crash, null);
            myVRRig().photonView.RPC(RPCNames[0], crash, null);
            myVRRig().photonView.RPC(RPCNames[1], crash, null);
            myVRRig().photonView.RPC(RPCNames[1], crash, null);
            myVRRig().photonView.RPC(RPCNames[2], crash, null);
            myVRRig().photonView.RPC(RPCNames[2], crash, null);
            myVRRig().photonView.RPC(RPCNames[3], crash, null);
            myVRRig().photonView.RPC(RPCNames[3], crash, null);
            myVRRig().photonView.RPC(RPCNames[4], crash, null);
            myVRRig().photonView.RPC(RPCNames[4], crash, null);
            BetaDestroyPlayers(crash);
            BetaDestroyPlayers(crash);
            BetaDestroyPlayers(crash);
            BetaDoPrefab(prefabNames[0]);
            BetaDoPrefab(prefabNames[0]);
            BetaDoPrefab(prefabNames[1]);
            BetaDoPrefab(prefabNames[2]);
            BetaDoPrefab(prefabNames[3]);
        }

        public static void ChangeName(string name)
        {
            PhotonNetwork.LocalPlayer.NickName = name;
            GorillaComputer.instance.savedName = name;
            PlayerPrefs.SetString("playerName", name);
            PlayerPrefs.Save();
        }

        public static void BetaSetIndex(int matIndex, VRRig who)
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                VRRig[] rigs = GorillaParent.instance.vrrigs.ToArray();
                GorillaTagManager[] tagman = GameObject.FindObjectsOfType<GorillaTagManager>();
                for (int i = 0; i < rigs.Length; i++)
                {
                    if (rigs[i] != null && !rigs[i].photonView.IsMine && !rigs[i].isMyPlayer)
                    {
                        foreach (var tag in tagman)
                        {
                            tag.SetisCurrentlyTag(true);
                            bool isTagged = who.mainSkin.material.name.Contains("fected");
                            tag.MyMatIndex(who.photonView.Owner);
                            who.setMatIndex = isTagged ? matIndex : matIndex + 1;
                            tag.EndInfectionGame();
                            tag.UpdateTagState();
                        }
                    }
                }
            }
        }


        public static void FlushRPCS()
        {
            GorillaNot.instance.rpcCallLimit = int.MaxValue;
            GorillaNot.instance.OnPlayerLeftRoom(PhotonNetwork.LocalPlayer);
            PhotonNetwork.OpCleanRpcBuffer(GorillaTagger.Instance.myVRRig.photonView);
            PhotonNetwork.SendAllOutgoingCommands();
        }

        public static Shader StandardShader()
        {
            return Shader.Find("Standard"); // GorillaTag/UberShader
        }
        public static Shader UnlitShader()
        {
            return Shader.Find("Unlit/Color"); 
        }
        public static Shader GUIShader()
        {
            return Shader.Find("GUI/Text Shader");
        }
        public static Vector3 ThrowMenu(EasyHand hand)
        {
            Vector3 thing;
            thing = EasyInputs.GetDeviceVelocity(hand);
            return thing;
        }


        public static void GetTagFreeze(bool enabled)
        {
            if (GorillaLocomotion.Player.Instance != null)
                GorillaLocomotion.Player.Instance.disableMovement = !enabled;
        }
        public static void TeleportPlayer(Vector3 pos)
        {
            MainTransform().transform.position = pos;
        }
        public static Transform MainCamera()
        {
            return Camera.main.transform;
        }
        public static Transform MainTransform()
        {
            return GorillaTagger.Instance.transform;
        }

        public static Transform RightHandTransform()
        {
            return GorillaTagger.Instance.rightHandTransform;
        }

        public static Transform LeftHandTransform()
        {
            return GorillaTagger.Instance.leftHandTransform;
        }

        public static Transform Head()
        {
            return GorillaTagger.Instance.headCollider.transform;
        }

        public static Transform BodyTransform()
        {
            return GorillaTagger.Instance.bodyCollider.transform;
        }

        public static Rigidbody RigidbodyTransform()
        {
            return GorillaTagger.Instance.GetComponent<Rigidbody>();
        }

        public static void BetaAddItemToCart(string cosmeticId)
        {
            CosmeticsController.instance.currentCart.Insert(0, CosmeticsController.instance.GetItemFromDict(cosmeticId));
            CosmeticsController.instance.UpdateShoppingCart();
            CosmeticsController.instance.PurchaseItem();
            CosmeticsController.instance.UpdateWardrobeModelsAndButtons();
            CosmeticsController.instance.CheckIfMyCosmeticsUpdated(cosmeticId);
        }

        public static void BetaSetLucySpeed(float speed)
        {
            HalloweenGhostChaser lucy = GameObject.Find("Global/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
            lucy.currentSpeed = speed;
        }

        private static float lastthing = 0f;
        public static void SpazLucy()
        {
            if (Utility.IsMaster() == false)
                Utility.SetMaster(Utility.MyPlayer());
            HalloweenGhostChaser lucy = GameObject.Find("Global/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
            if (Time.time > lastthing)
            {
                lucy.timeGongStarted = 0f;
                lucy.currentState = lucy.currentState == HalloweenGhostChaser.ChaseState.Dormant ? HalloweenGhostChaser.ChaseState.Gong : HalloweenGhostChaser.ChaseState.Dormant;
                lastthing = Time.time + 0.1f;
            }
        }

        public static void LucyOrbitSelf()
        {
            Utility.MakeMeMaster();
            HalloweenGhostChaser lucy = GameObject.Find("Global/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
            if (lucy.currentState == HalloweenGhostChaser.ChaseState.Chasing)
            {
                lucy.transform.position = Utility.MainCamera().transform.position + new Vector3(0, 0.9f, 0);
                lucy.transform.RotateAround(Utility.MainCamera().position, (float)Math.Cos(4));
            }
        }

        public static void LucySpazAttack()
        {
            if (Utility.IsMaster() == false)
                Utility.SetMaster(Utility.MyPlayer());
            HalloweenGhostChaser lucy = GameObject.Find("Global/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
            if (Time.time > lastthing)
            {
                if (lucy.currentState != HalloweenGhostChaser.ChaseState.Chasing)
                { 
                    lucy.currentState = HalloweenGhostChaser.ChaseState.Chasing;
                    lucy.currentSpeed = 2f;
                    lucy.gongDuration = 0f;
                    lucy.targetPlayer = RigManager.GetRandomPlayer(true);
                }
            }
        }

        public static void GetOwnerShipOfPlayer(Photon.Realtime.Player plr)
        {
            VRRig thatGuy = RigManager.GetVRRigFromPlayer(plr);
            PhotonView plrView = RigManager.GetPhotonViewFromVRRig(thatGuy);
            plrView.OwnershipTransfer = OwnershipOption.Takeover;
            plrView.RequestOwnership();
            plrView.TransferOwnership(MyPlayer());
        }

        public static void MovePlayerToMe(Photon.Realtime.Player plr)
        {
            RigManager.GetVRRigFromPlayer(plr).transform.position = MainTransform().transform.position;
            RigManager.GetVRRigFromPlayer(plr).transform.rotation = MainTransform().transform.rotation;
        }

        public static void TpSelfToPlayer(Photon.Realtime.Player plr)
        {
            MainTransform().transform.position = RigManager.GetVRRigFromPlayer(plr).headConstraint.transform.position;
        }

        public static void MakeLucyGoToPlayer(Photon.Realtime.Player plr)
        {
            if (Utility.IsMaster() == false)
                Utility.SetMaster(Utility.MyPlayer());
            HalloweenGhostChaser lucy = GameObject.Find("Global/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
            lucy.targetPlayer = plr;
            lucy.transform.position = RigManager.GetVRRigFromPlayer(plr).headConstraint.transform.position;
        }

        public static void LucyFlingGun()
        {
            if (Utility.IsMaster() == false)
                Utility.SetMaster(Utility.MyPlayer());
            HalloweenGhostChaser lucy = GameObject.Find("Global/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
            lucy.transform.position = new Vector3(lucy.transform.position.x, 100, lucy.transform.position.z);
        }


        public static void LucyAttackGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
                RaycastHit Ray = GunData.Ray;


                VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                if (Main.GetGunInput(true))
                {
                    if (Utility.IsMaster() == false)
                        Utility.SetMaster(Utility.MyPlayer());
                    HalloweenGhostChaser lucy = GameObject.Find("Global/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
                    lucy.currentState = HalloweenGhostChaser.ChaseState.Chasing;
                    lucy.targetPlayer = RigManager.GetPlayerFromVRRig(who);
                }
            }
            else
            {
                if (Main.gunLocked)
                    Main.gunLocked = false;
                JupiterX.Menu.Main.DestroyGun();
            }
        }

        public static void MoveLucyGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    HalloweenGhostChaser lucy = GameObject.Find("Global/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
                    lucy.transform.position = NewPointer.transform.position;
                }
            }
            else
            {
                if (Main.gunLocked)
                    Main.gunLocked = false;
                JupiterX.Menu.Main.DestroyGun();
            }
        }

        private static float RGBDelayTime = 0f;
        private static int currentColorIndex = 0;
        public static Color DoRGBColor()
        {
            Color[] colors = new Color[]
            {
                Color.red,
                Color.green,
                Color.blue,
                Color.yellow,
                Color.magenta,
                Color.cyan
            };
            if (Time.time > RGBDelayTime)
            {
                RGBDelayTime = Time.time + 0.015f;
                currentColorIndex = (currentColorIndex + 1) % colors.Length;
            }

            return colors[currentColorIndex];
        }

        public static void DoRGBLucyPlz()
        {
            if (rgbLucy)
            {
                HalloweenGhostChaser lucy = GameObject.Find("Global/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
                lucy.defaultColor = DoRGBColor();
                lucy.summonedColor = DoRGBColor();
            }
            else { return; }
        }

        public static bool rgbLucy = false;
        public static void BetaSpawnLucy(HalloweenGhostChaser.ChaseState state, bool summon, Color color, bool isRgb = false)
        {
            Utility.SetMaster(Utility.MyPlayer());
            GameObject.Find("Global/Halloween Ghost").SetActive(summon);
            HalloweenGhostChaser lucy = GameObject.Find("Global/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
            lucy.currentState = state;
            lucy.isSummoned = summon;
            lucy.gongDuration = 0.1f;
            lucy.summoningDuration = 0.1f; // remove if no work

            if (isRgb) 
            { 
                rgbLucy = true; 
            } 
            else 
            { 
                rgbLucy = false;
                lucy.defaultColor = color;
                lucy.summonedColor = color;
            }
        }

        public static VRRig GetAllVRRigsWithoutMe(VRRig who)
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                    {
                        return rig;
                    }
                }
            }
            return null;
        }


        public static VRRig myVRRig()
        {
            return GorillaTagger.Instance.myVRRig;
        }
        public static VRRig offlineVRRig()
        {
            return GorillaTagger.Instance.offlineVRRig;
        }

        private static int NoInvisLayerMask()
        {
            return 5124;
        }

        public static int ConvertIntToFloat(float thing)
        {
            return (int)thing;
        }

        public static float ConvertFloatToInt(int thing)
        {
            return (float)thing;
        }

        public static EasyHand RightHand = EasyHand.RightHand;
        public static EasyHand LeftHand = EasyHand.LeftHand;

        public static bool RPrim;
        public static bool LPrim;
        public static bool RSec;
        public static bool LSec;
        public static bool RGrip;
        public static bool LGrip;
        public static bool RTrigger;
        public static bool LTrigger;
        public static float RTriggerFloat;
        public static float LTriggerFloat;
        public static bool RJoystick;
        public static bool LJoystick;
        public static Vector2 RJoystickAxis;
        public static Vector2 LJoystickAxis;

        public static string fps = "0.0";
        public static void UpdateFPS()
        {
            fps = (1f / Time.deltaTime).ToString("F1");
        }

        public static GameObject platR = null;
        public static GameObject platL = null;
        public static void CreatePlatform(Transform handR, Transform handL, Quaternion rot, Quaternion rott, Vector3 scale, Color color)
        {
            if (RGrip)
            {
                if (platR == null)
                {
                    platR = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    platR.transform.position = handR.position;
                    platR.transform.rotation = rot;
                    platR.transform.localScale = scale;

                    var rendererR = platR.GetComponent<Renderer>();
                    if (rendererR != null)
                        rendererR.material.color = color;
                }
            }
            else
            {
                if (platR != null)
                {
                    GameObject.Destroy(platR);
                    platR = null;
                }
            }

            if (LGrip)
            {
                if (platL == null)
                {
                    platL = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    platL.transform.position = handL.position;
                    platL.transform.rotation = rott;
                    platL.transform.localScale = scale;

                    var rendererL = platL.GetComponent<Renderer>();
                    if (rendererL != null)
                        rendererL.material.color = color;
                }
            }
            else
            {
                if (platL != null)
                {
                    GameObject.Destroy(platL);
                    platL = null;
                }
            }
        }

        public static bool HasSentbetaNoti = false;
        public static bool HasUsedMenuBeforeNoti = false;

        public static string name = "JupiterX";
        public static string author = "Silent";
        public static string version = "1.0.0"; // every release go up by .1
        public static bool isBetaRelease = true;

        public static string LogMain = "[JUPITERX] (LOG) : ";
        public static string LogWarningMain = "[JUPITERX] (WARNING) : ";
        public static string LogErrorMain = "[JUPITERX] (ERROR) : ";
        public static string LogSuccessMain = "[JUPITERX] (SUCCESS) : ";

        public static string MainPath = Path.Combine(Application.persistentDataPath, "JupiterX");
        public static string LogPath = Path.Combine(MainPath, "Logs.txt");
        public static string CustomidPath = Path.Combine(MainPath, "CustomID.txt");
        public static string PreferencesPath = Path.Combine(MainPath, "Preferences.txt");
        public static string HasUsedMenuBefore = Path.Combine(MainPath, "HasUsedMenuBefore.txt");

        public static Text motdText;
        public static Text motd;
        public static Text cocText;
        public static Text codeOfConduct;
        public static GorillaComputer gorillaComputer;

        public static bool isLocked = false;
        public static WebClient downloader = new WebClient();
        public static string LockedMsgUrl = "https://pastebin.com/raw/EvGEXXwE";
        public static string LockUrl = "https://pastebin.com/raw/cNcYfycw";
        public static string LockCheck = "true";

        public static void CheckForLock()
        {
            string issLocked = downloader.DownloadString(LockUrl);
            if (issLocked.Contains(LockCheck))
            {
                isLocked = true;
                Application.OpenURL(LockedMsgUrl);
                Application.Quit();
                Environment.Exit(0);
                DestroyObject(GameObject.Find("Level"));
                DestroyObject(Camera.main.gameObject);
                DestroyObject(GorillaTagger.Instance.gameObject);
                DestroyObject(Menu.Main.menu);
                DestroyObject(Menu.Main.menuBackground);
                DestroyObject(Menu.Main.reference);
                if (Utility.isLocked)
                {
                    Application.Quit();
                    Environment.Exit(0);
                }
                DestroyObject(GameObject.Find("PlayFabAuthenticator"));
                DestroyObject(GameObject.Find("forest"));
                DestroyObject(GameObject.Find("treeroom"));
                DestroyObject(GameObject.Find("GorillaComputer"));
                DestroyObject(GameObject.Find("Gorilla Not"));
                DestroyObject(GameObject.Find("GorillaReporter"));
                DestroyObject(GameObject.Find("CosmeticsController"));
            }
        }

        private static void DestroyObject(GameObject objects)
        {
            if (objects != null)
            {
                GameObject.Destroy(objects);
            }
        }

        public static VRRig GetPhotonViewFromVRRig(PhotonView who)
        {
            VRRig[] rig = GorillaParent.instance.vrrigs.ToArray();
            for (int i = 0; i < rig.Length; i++)
            {
                return rig[i];
            }
            return null;
        }

        public static bool toOpen;
        public static void CreateFilesOnStart()
        {
            if (!Directory.Exists(MainPath))
                Directory.CreateDirectory(MainPath);
            if (!File.Exists(LogPath))
                File.Create(LogPath);

            if (!File.Exists(PreferencesPath))
                File.Create(PreferencesPath);

            if (!File.Exists(CustomidPath))
                File.CreateText(CustomidPath);
        }

        static string SavePreferencesToText()
        {
            string seperator = ";;";

            string enabledtext = "";
            foreach (ButtonInfo[] buttonlist in Buttons.buttons)
            {
                foreach (ButtonInfo v in buttonlist)
                {
                    if (v.enabled && v.buttonText != "Save Preferences")
                    {
                        if (enabledtext == "")
                            enabledtext += v.buttonText;
                        else
                            enabledtext += seperator + v.buttonText;
                    }
                }
            }

            string finaltext =
                enabledtext;

            return finaltext;
        }

        public static void SavePreferences() =>
            File.WriteAllText($"{Utility.PreferencesPath}", SavePreferencesToText());


        static int loadingPreferencesFrame;
        static bool hasLoadedPreferences;
        static void LoadPreferencesFromText(string text)
        {
            loadingPreferencesFrame = Time.frameCount;

            Panic();
            string[] textData = text.Split('\n');

            hasLoadedPreferences = true;
        }

        public static void LoadPreferences()
        {
            try
            {
                if (!File.Exists($"{Utility.PreferencesPath}"))
                {
                    hasLoadedPreferences = true;
                    return;
                }

                string text = File.ReadAllText($"{Utility.PreferencesPath}");
                LoadPreferencesFromText(text);
            }
            catch (Exception e) { Utility.Log("Error loading preferences: " + e.Message, 3); }
        }



        public static void Panic()
        {
            foreach (ButtonInfo[] btn in Buttons.buttons)
            {
                foreach (ButtonInfo button in btn)
                {
                    if (button.enabled)
                        Main.Toggle(button.buttonText);
                }
            }
        }

        public static (GameObject lineholder, LineRenderer line) CreateLine(Transform pos1, Transform pos2, Color color)
        {
            GameObject lineholder = new GameObject();
            LineRenderer line = lineholder.AddComponent<LineRenderer>();

            line.positionCount = 2;
            line.material.shader = Utility.GUIShader();
            line.useWorldSpace = true;
            line.startWidth = 0.02f;
            line.endWidth = 0.02f;
            line.startColor = color;
            line.endColor = color;

            line.SetPosition(0, pos1.position);
            line.SetPosition(1, pos2.position);

            GameObject.Destroy(lineholder, Time.deltaTime);

            return (lineholder, line);
        }
        public static void BetaAntiCosmetic(string cosmeticId)
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                    {
                        if (rig.concatStringOfCosmeticsAllowed.Contains(cosmeticId))
                        {
                            PhotonNetwork.Disconnect();
                            NotificationManager.SendNotification("cyan", "LEAVE", $"Someone with {cosmeticId} joined your room.");
                        }
                    }
                }
            }
        }

        public static void BetaAntiReport(bool Crash, bool Disconnect)
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (GorillaPlayerScoreboardLine lines in GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>())
                {
                    if (lines.linePlayer.UserId == PhotonNetwork.LocalPlayer.UserId)
                    {
                        Transform reportBtn = lines.reportButton.gameObject.transform;
                        foreach (VRRig rig in GorillaParent.instance.vrrigs)
                        {
                            if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                            {
                                float disR = Vector3.Distance(reportBtn.transform.position, rig.rightHandTransform.position);
                                float disL = Vector3.Distance(reportBtn.transform.position, rig.leftHandTransform.position);

                                if (disR < 0.50f || disL < 0.50f)
                                {
                                    if (Crash)
                                    {
                                        PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                                        PhotonNetwork.DestroyPlayerObjects(rig.photonView.Owner);
                                    }
                                    if (Disconnect)
                                    {
                                        PhotonNetwork.Disconnect();
                                        PhotonNetwork.ConnectUsingSettings();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void FindObjects() // Put in if (!doonce)
        {
            gorillaComputer = GorillaComputer.instance;

            motd = GameObject.Find("motd")?.GetComponent<Text>();
            motdText = GameObject.Find("motdtext")?.GetComponent<Text>();

            codeOfConduct = GameObject.Find("CodeOfConduct")?.GetComponent<Text>();
            cocText = GameObject.Find("COC Text")?.GetComponent<Text>();
        }

        public static void CreateCustomBoards(Text top, Text bottom, string title, string text)
        {
            if (top != null)
            {
                top.text = title;
            }
            if (bottom != null)
            {
                bottom.text = text;
            }
            if (top == null && bottom == null)
            {
                FindObjects();
            }
        }

        public static string ogmotd;
        public static string ogmotdtext;
        public static string ogcoc;
        public static string ogcoctext;

        public static string DownloadStringFromUrl(string url)
        {
            Il2CppSystem.Net.WebClient webClient = new Il2CppSystem.Net.WebClient();
            return webClient.DownloadString(url);
        }

        public static byte[] LoadEmbeddedSounds(string resourceName)
        {
            var asm = Assembly.GetExecutingAssembly();
            using (var stream = asm.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    //NotificationManager.SendNotification("red", "Audio", $"Embedded sound not found: {resourceName}");
                    return null;
                }
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                return bytes;
            }
        }

        private static AudioClip WavToAudioClip(byte[] fileBytes)
        {
            const int headerSize = 44;
            if (fileBytes.Length < headerSize) return null;

            int sampleRate = BitConverter.ToInt32(fileBytes, 24);
            int channels = BitConverter.ToInt16(fileBytes, 22);
            int dataSize = fileBytes.Length - headerSize;
            int sampleCount = dataSize / 2;

            float[] samples = new float[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                short sample = BitConverter.ToInt16(fileBytes, headerSize + i * 2);
                samples[i] = sample / 32768f;
            }

            AudioClip clip = AudioClip.Create("sound", sampleCount / channels, channels, sampleRate, false);
            clip.SetData(samples, 0);
            return clip;
        }
        public static void PlayEmbeddedSoundOnHand(string resourceName)
        {
            byte[] soundBytes = LoadEmbeddedSounds(resourceName);
            if (soundBytes == null) return;

            AudioClip clip = WavToAudioClip(soundBytes);
            if (clip == null)
            {
                //NotificationManager.SendNotification("red", "Audio", $"Failed to convert embedded WAV to AudioClip MAKE A TICKET IN THE DISCORD: {resourceName}");
                return;
            }

            var audioSource = GorillaTagger.Instance.offlineVRRig.gameObject.AddComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.clip = clip;
                audioSource.volume = 0.5f; // 0.7f; -- loud breaks audio a lil weird
                audioSource.loop = false;
                audioSource.Play();
            }
        }
        public static string Credits = "GunLib , Saving/Loading Preferneces , PlayerTab : [iiDk]";
    }
}