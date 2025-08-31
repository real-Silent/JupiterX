using ExitGames.Client.Photon;
using GorillaNetworking;
using JupiterX.Classes;
using JupiterX.Menu;
using Photon.Pun;
using PlayFab;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX.Mods
{
    internal class Overpowered
    {
        public static void BetaChangeShinyRock(int ammount)
        {
            CosmeticsController.instance.gotMyDaily = true;
            CosmeticsController.instance.currencyBalance += ammount;
            CosmeticsController.instance.UpdateCurrencyBoard();
        }
        public static void RPCLag()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Utility.SetMaster(PhotonNetwork.LocalPlayer);
            }
            if (Utility.RTrigger)
            {
                Utility.myVRRig().photonView.RPC("SetTaggedTime", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("SetTaggedTime", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("SetTaggedTime", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("SetTaggedTime", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("SetTaggedTime", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("SetTaggedTime", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("SetTaggedTime", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdatePlayerCosmetic", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdatePlayerCosmetic", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdatePlayerCosmetic", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdatePlayerCosmetic", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdatePlayerCosmetic", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdatePlayerCosmetic", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdatePlayerCosmetic", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("SetSlowedTime", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("SetSlowedTime", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("SetSlowedTime", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("SetSlowedTime", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("SetSlowedTime", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("ReportTagRPC", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("ReportTagRPC", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("ReportTagRPC", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("ReportTagRPC", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("ReportTagRPC", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("ReportTagRPC", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("ReportTagRPC", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdateCosmetics", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdateCosmetics", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdateCosmetics", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdateCosmetics", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdateCosmetics", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdateCosmetics", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdateCosmetics", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdateCosmetics", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdateCosmetics", RpcTarget.Others, null);
                Utility.myVRRig().photonView.RPC("UpdateCosmetics", RpcTarget.Others, null);
            }
        }
        public static void SetMaster()
        {
            Utility.SetMaster(PhotonNetwork.LocalPlayer);
        }

        public static void DoMatStuffIdk()
        {
            Photon.Realtime.Player[] players = PhotonNetwork.PlayerListOthers.ToArray();
            Utility.BetaDoSmthWithTag(0);
            Utility.BetaDoSmthWithTag(1);
            Utility.BetaDoSmthWithTag(2);
            Utility.BetaDoSmthWithTag(3);
            for (int i = 0; i < players.Length; i++)
            {
                Utility.BetaDoSmthWithTag(4, players[i]);
            }
        }

        public static void SetMasterGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
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
                if (Main.gunLocked)
                    Main.gunLocked = false;
                JupiterX.Menu.Main.DestroyGun();
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
                    for (int i = 0; i < 1; i++)
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
                GameObject NewPointer = GunData.NewPointer;
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
                    Utility.myVRRig().photonView.RPC("UpdateCosmetics", RpcTarget.Others, null); 
                    Utility.myVRRig().photonView.RPC("UpdateCosmetics", RpcTarget.Others, null); 
                    Utility.myVRRig().photonView.RPC("UpdateCosmetics", RpcTarget.Others, null);
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
                if (Main.gunLocked)
                    Main.gunLocked = false;
                JupiterX.Menu.Main.DestroyGun();
            }
        }

        public static void KickGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
                RaycastHit Ray = GunData.Ray;


                if (Main.gunLocked && Main.lockTarget != null)
                {
                    GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Add(Main.lockTarget.photonView.Owner.UserId);
                    GameObject.Find("Photon Manager").GetComponent<PhotonNetworkController>().friendIDList.Add(Main.lockTarget.photonView.Owner.UserId);
                    PhotonView photonView = RigManager.GetPhotonViewFromVRRig(Main.lockTarget);
                    if (Main.lockTarget != null && GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Contains(Main.lockTarget.photonView.Owner.UserId))
                    {
                        for (int i = 0; i < 1; i++)
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
                if (Main.gunLocked)
                    Main.gunLocked = false;
                JupiterX.Menu.Main.DestroyGun();
            }
        }


        public static void BanGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
                RaycastHit Ray = GunData.Ray;


                if (Main.GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who)
                    {
                        Utility.BetaBanAll(who.photonView.Owner.UserId);
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


        public static void BanGunJXModding()
        {
            
        }

        public static void SlowAll()
        {
            Utility.SetMaster(PhotonNetwork.LocalPlayer);
            Utility.myVRRig().photonView.RPC("SetSlowedTime", Main.lockTarget.photonView.Owner, null);
        }

        public static void SlowGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
                RaycastHit Ray = GunData.Ray;


                if (Main.gunLocked && Main.lockTarget != null)
                {
                    Utility.SetMaster(PhotonNetwork.LocalPlayer);
                    Utility.myVRRig().photonView.RPC("SetSlowedTime", Main.lockTarget.photonView.Owner, null);
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
                    }
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
                }
            }
        }

        public static void CrashGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
                RaycastHit Ray = GunData.Ray;

                if (Utility.IsMaster() == false)
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
                }
            }
            else
            {
                if (Main.gunLocked)
                    Main.gunLocked = false;
                JupiterX.Menu.Main.DestroyGun();
            }
        }
    }
}
