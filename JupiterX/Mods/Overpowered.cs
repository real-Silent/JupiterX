using ExitGames.Client.Photon;
using GorillaNetworking;
using JupiterX.Classes;
using JupiterX.Menu;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace JupiterX.Mods
{
    public class Overpowered
    {
        public static void BetaChangeShinyRock(int ammount)
        {
            CosmeticsWrapper.AddCurrency(ammount);
        }

        public static void SpazRocks()
        {
            BetaChangeShinyRock(UnityEngine.Random.RandomRangeInt(int.MinValue, int.MaxValue));
        }

        public static void RigSpam()
        {
            SetMaster();
            if (Utility.RTrigger)
            {
                PhotonNetwork.Destroy(Utility.myVRRig().gameObject);
            }
        }

        public static void SetMaster()
        {
            Utility.SetMaster(PhotonNetwork.LocalPlayer);
        }

        public static void AlawysMaster()
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            PhotonNetwork.CurrentRoom.SetMasterClient(PhotonNetwork.LocalPlayer);
            GorillaNot.instance.currentMasterClient = PhotonNetwork.LocalPlayer;
            GorillaNot.instance.OnMasterClientSwitched(PhotonNetwork.LocalPlayer);
        }

        public static void LongNamePub()
        {
            CreatePublic(" JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX  JUPITERX ");
        }

        public static void CreatePublic(string roomName)
        {
            Hashtable customProps = new Hashtable();
            customProps.Add("gameMode", GorillaComputer.instance.currentQueue + GorillaComputer.instance.currentGameMode);
            RoomOptions roomOptions = new RoomOptions()
            {
                SuppressPlayerInfo = false,
                MaxPlayers = 10,
                PublishUserId = false,
                CustomRoomProperties = customProps
            };
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }

        public static void CreatePublic(byte playercount)
        {
            Hashtable customProps = new Hashtable();
            customProps.Add("gameMode", GorillaComputer.instance.currentQueue + GorillaComputer.instance.currentGameMode);
            RoomOptions roomOptions = new RoomOptions()
            {
                SuppressPlayerInfo = false,
                MaxPlayers = playercount,
                PublishUserId = false,
                CustomRoomProperties = customProps
            };
            PhotonNetwork.CreateRoom("NBJG", roomOptions);
        }

        public static void MatSpamAll()
        {
            SetMaster();
            foreach (Photon.Realtime.Player plr in PhotonNetwork.PlayerListOthers)
            {
                foreach (GorillaTagManager tagman in GameObject.FindObjectsOfType<GorillaTagManager>())
                {
                    if (tagman.currentInfected.Contains(plr))
                    {
                        tagman.currentInfected.Remove(plr);
                        tagman.EndInfectionGame();
                    }
                    else
                    {
                        tagman.AddInfectedPlayer(plr);
                        tagman.UpdateInfectionState();
                    }
                    tagman.UpdateInfectionState();
                }
            }
        }

        public static void MatSpamGun()
        {
            SetMaster();
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.gunLocked && Main.lockTarget != null)
                {
                    foreach (GorillaTagManager tagman in GameObject.FindObjectsOfType<GorillaTagManager>())
                    {
                        if (tagman.currentInfected.Contains(Main.lockTarget.photonView.Owner))
                        {
                            tagman.currentInfected.Remove(Main.lockTarget.photonView.Owner);
                            tagman.EndInfectionGame();
                        }
                        else
                        {
                            tagman.AddInfectedPlayer(Main.lockTarget.photonView.Owner);
                            tagman.UpdateInfectionState();
                        }
                        tagman.UpdateInfectionState();
                    }
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
                Main.lockTarget = null;
                if (Main.gunLocked)
                    Main.gunLocked = false;
            }
        }

        public static void SetMasterGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;


                if (Main.gunLocked && Main.lockTarget != null)
                {
                    if (PhotonNetwork.MasterClient != Main.lockTarget.photonView.Owner)
                        Utility.SetMaster(Main.lockTarget.photonView.Owner);
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
                Main.lockTarget = null;
                if (Main.gunLocked)
                    Main.gunLocked = false;
            }
        }

        public static void KickAll()
        {
            foreach (Photon.Realtime.Player plr in PhotonNetwork.PlayerListOthers)
            {
                GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Add(plr.UserId);
                GameObject.Find("Photon Manager").GetComponent<PhotonNetworkController>().friendIDList.Add(plr.UserId);
                if (Main.lockTarget != null && GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Contains(plr.UserId))
                {
                    for (int i = 0; i < 25; i++)
                    {
                        Utility.myVRRig().photonView.RPC("JoinPubWithFreinds", plr, null);
                    }
                }
            }
        }

        public static void CrashGunV2()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.gunLocked && Main.lockTarget != null)
                {
                    Utility.myVRRig().photonView.RPC("RequestCosmetics", RpcTarget.Others, null); 
                    Utility.myVRRig().photonView.RPC("RequestCosmetics", RpcTarget.Others, null); 
                    Utility.myVRRig().photonView.RPC("RequestCosmetics", RpcTarget.Others, null); 
                    Utility.myVRRig().photonView.RPC("RequestCosmetics", RpcTarget.Others, null); 
                    Utility.myVRRig().photonView.RPC("RequestCosmetics", RpcTarget.Others, null); 
                    Utility.myVRRig().photonView.RPC("UpdateCosmetics", RpcTarget.Others, null); 
                    Utility.myVRRig().photonView.RPC("UpdateCosmetics", RpcTarget.Others, null); 
                    Utility.myVRRig().photonView.RPC("UpdateCosmetic", RpcTarget.Others, null); 
                    Utility.myVRRig().photonView.RPC("UpdateCosmetic", RpcTarget.Others, null); 
                    Utility.myVRRig().photonView.RPC("UpdateCosmetic", RpcTarget.Others, null);
                    Utility.BetaCrashPlayer(Main.lockTarget.photonView.Owner);
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
                Main.lockTarget = null;
                if (Main.gunLocked)
                    Main.gunLocked = false;
            }
        }

        public static void KickGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.gunLocked && Main.lockTarget != null)
                {
                    GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Add(Main.lockTarget.photonView.Owner.UserId);
                    GameObject.Find("Photon Manager").GetComponent<PhotonNetworkController>().friendIDList.Add(Main.lockTarget.photonView.Owner.UserId);
                    PhotonView photonView = RigManager.GetPhotonViewFromVRRig(Main.lockTarget);
                    if (Main.lockTarget != null && GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Contains(Main.lockTarget.photonView.Owner.UserId))
                    {
                        for (int i = 0; i < 25; i++)
                        {
                            photonView.RPC("JoinPubWithFreinds", Main.lockTarget.photonView.Owner, null);
                        }
                    }
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
                Main.lockTarget = null;
                if (Main.gunLocked)
                    Main.gunLocked = false;
            }
        }


        public static void BanGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who && who != GorillaTagger.Instance.myVRRig)
                    {
                        Utility.BetaBanAll(who.photonView.Owner.UserId);
                    }
                }
            }
            else
            {
                Main.lockTarget = null;
                if (Main.gunLocked)
                    Main.gunLocked = false;
            }
        }
        public static void SlowAll()
        {
            Utility.SetMaster(PhotonNetwork.LocalPlayer);
            Utility.myVRRig().photonView.RPC("SetSlowedTime", Main.lockTarget.photonView.Owner, null);
            Utility.myVRRig().photonView.RPC("SetJoinTaggedTime", Main.lockTarget.photonView.Owner, null);
        }

        public static void SlowGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;


                if (Main.gunLocked && Main.lockTarget != null)
                {
                    Utility.SetMaster(PhotonNetwork.LocalPlayer);
                    Utility.myVRRig().photonView.RPC("SetSlowedTime", Main.lockTarget.photonView.Owner, null);
                    Utility.myVRRig().photonView.RPC("SetJoinTaggedTime", Main.lockTarget.photonView.Owner, null);
                }

                if (Main.GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who)
                    {
                        Main.gunLocked = true;
                        Main.lockTarget = who;
                    }
                    PhotonNetwork.SendAllOutgoingCommands();
                }
            }
            else
            {
                Main.lockTarget = null;
                if (Main.gunLocked)
                    Main.gunLocked = false;
            }
        }

        public static void CrashAllV2()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                {
                    if (Utility.RTrigger)
                    {
                        Utility.BetaCrashAllV2(rig);
                        Utility.BetaCrashAllV2(rig);
                        Utility.BetaCrashAllV2(rig);
                        Utility.BetaCrashAllV2(rig);
                        Utility.BetaCrashAllV2(rig);
                        PhotonNetwork.SendAllOutgoingCommands();
                    }
                }
            }
        }

        public static void CrashAllV3()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                {
                    if (Utility.RTrigger)
                    {
                        Hashtable domycumbust = new Hashtable(5);
                        domycumbust.Add(0, new Il2CppSystem.Object() { });
                        domycumbust.Add(1, new Il2CppSystem.Object() { });
                        domycumbust.Add(2, new Il2CppSystem.Object() { });
                        domycumbust.Add(3, new Il2CppSystem.Object() { });
                        domycumbust.Add(4, new Il2CppSystem.Object() { });
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(207, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(207, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(207, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(207, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(207, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(201, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(201, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(201, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(250, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(250, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(250, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(249, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(249, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(249, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(199, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(199, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(199, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(199, domycumbust, null, ExitGames.Client.Photon.SendOptions.SendUnreliable);
                        PhotonNetwork.SendAllOutgoingCommands();
                    }
                }
            }
        }

        public static void CrashAllV4()
        {
            if (Utility.RTrigger)
            {
                for (int i = 0; i < 150; i++)
                {
                    PhotonNetwork.RaiseEvent(2, null, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendUnreliable);
                    PhotonNetwork.RaiseEvent(3, null, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendUnreliable);
                }
                PhotonNetwork.SendAllOutgoingCommands();
            }
        }

        public static void CrashAllV5()
        {
            if (Utility.RTrigger)
            {
                foreach (Photon.Realtime.Player plr in PhotonNetwork.PlayerListOthers)
                {
                    PhotonNetwork.DestroyPlayerObjects(plr);
                    PhotonNetwork.SendDestroyOfPlayer(plr.ActorNumber);
                    PhotonNetwork.OpRemoveCompleteCacheOfPlayer(plr.ActorNumber);
                }
                for (int i = 0; i < 150; i++)
                {
                    PhotonNetwork.RaiseEvent(2, null, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendUnreliable);
                    PhotonNetwork.RaiseEvent(3, null, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendUnreliable);
                    PhotonNetwork.RaiseEvent((byte)UnityEngine.Random.Range(200, 212), null, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendUnreliable);
                }
                PhotonNetwork.SendAllOutgoingCommands();
            }
        }

        public static void CrashGunV5()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;
                Photon.Realtime.Player plr = Ray.collider.GetComponentInParent<PhotonView>().Owner;

                if (Main.GetGunInput(true))
                {
                    for (int i = 0; i < 150; i++)
                    {
                        PhotonNetwork.DestroyPlayerObjects(plr);
                        PhotonNetwork.SendDestroyOfPlayer(plr.ActorNumber);
                        PhotonNetwork.OpRemoveCompleteCacheOfPlayer(plr.ActorNumber);
                        PhotonNetwork.RaiseEvent(2, null, new RaiseEventOptions { TargetActors = new int[] { plr.actorNumber } }, SendOptions.SendUnreliable);
                        PhotonNetwork.RaiseEvent(3, null, new RaiseEventOptions { TargetActors = new int[] { plr.actorNumber } }, SendOptions.SendUnreliable);
                        PhotonNetwork.RaiseEvent((byte)UnityEngine.Random.Range(200, 212), null, new RaiseEventOptions { TargetActors = new int[] { plr.actorNumber } }, SendOptions.SendUnreliable);
                    }
                    PhotonNetwork.SendAllOutgoingCommands();
                }
            }
        }

        public static void CrashGunV3()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;
                Photon.Realtime.Player plr = Ray.collider.GetComponentInParent<PhotonView>().Owner;

                if (Main.GetGunInput(true))
                {
                    for (int i = 0; i < 150; i++)
                    {
                        PhotonNetwork.RaiseEvent(2, null, new RaiseEventOptions { TargetActors = new int[] { plr.actorNumber } }, SendOptions.SendUnreliable);
                        PhotonNetwork.RaiseEvent(3, null, new RaiseEventOptions { TargetActors = new int[] { plr.actorNumber } }, SendOptions.SendUnreliable);
                    }
                    PhotonNetwork.SendAllOutgoingCommands();
                }
            }
        }

        public static void CrashGunV4()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;
                Photon.Realtime.Player plr = Ray.collider.GetComponentInParent<PhotonView>().Owner;

                if (Main.GetGunInput(true))
                {
                    for (int i = 0; i < 150; i++)
                    {
                        PhotonNetwork.RaiseEvent((byte)UnityEngine.Random.Range(200, 214), null, new RaiseEventOptions { TargetActors = new int[] { plr.actorNumber } }, SendOptions.SendUnreliable);
                        PhotonNetwork.RaiseEvent((byte)UnityEngine.Random.Range(200, 214), null, new RaiseEventOptions { TargetActors = new int[] { plr.actorNumber } }, SendOptions.SendUnreliable);
                    }
                    PhotonNetwork.SendAllOutgoingCommands();
                }
            }
        }

        public static void CrashAll()
        {
            SetMaster();
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerListOthers)
            {
                if (Utility.RTrigger)
                {
                    Utility.BetaCrashPlayer(player);
                    PhotonNetwork.SendAllOutgoingCommands();
                }
            }
        }

        public static void FloatGun()
        {
            if (Main.GetGunInput(false))
            {   
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.gunLocked && Main.lockTarget != null)
                {
                    Vector3 basePos = Main.lockTarget.headMesh.transform.position;
                    Vector3 offset = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));
                    Utility.BetaSpawnPrefab("bulletPrefab", basePos + offset, Quaternion.identity);
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
                Main.lockTarget = null;
                if (Main.gunLocked)
                    Main.gunLocked = false;
            }
        }

        public static void CrashGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                Utility.SetMaster(PhotonNetwork.LocalPlayer);

                if (Main.gunLocked && Main.lockTarget != null)
                {
                    Utility.BetaCrashPlayer(Main.lockTarget.photonView.Owner);
                    Utility.BetaDestroyPlayers(Main.lockTarget.photonView.Owner);
                    Utility.BetaDestroyPlayers(Main.lockTarget.photonView.Owner);
                    Utility.BetaDestroyPlayers(Main.lockTarget.photonView.Owner);
                    Utility.BetaDestroyPlayers(Main.lockTarget.photonView.Owner);
                    Utility.BetaCrashPlayer(Main.lockTarget.photonView.Owner);
                }

                if (Main.GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who)
                    {
                        Main.gunLocked = true;
                        Main.lockTarget = who;
                    }
                    PhotonNetwork.SendAllOutgoingCommands();
                }
            }
            else
            {
                Main.lockTarget = null;
                if (Main.gunLocked)
                    Main.gunLocked = false;
            }
        }
    }
}
