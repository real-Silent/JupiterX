// ============================================================
//  JupiterX | Classes/Mods/RigManager.cs
//  Copyright (c) 2026 Jupiterx (@NAuth). All rights reserved.
//
//  This software and its source code are the property of the
//  author. Unauthorized copying, redistribution, modification,
//  or reuse of any part of this project, in whole or in part,
//  without express written permission is strictly prohibited.
//
//  Version: 2.0.0 | By Silent/Ashley/Nova (@s1lnt)
// ============================================================

using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using System;

namespace JupiterX.Classes
{

    [MelonLoader.RegisterTypeInIl2Cpp]
    public class RigManager : MonoBehaviour
    {
        public RigManager(IntPtr ptr ) : base(ptr) { }

        public static VRRig GetVRRigFromPlayer(Player p)
        {
            VRRig rig = null;
            foreach (var rg in GorillaParent.instance.vrrigs)
            {
                if (rg.photonView.Owner == p)
                {
                    rig = rg; break;
                }
            }
            return rig;
        }

        public static VRRig GetRandomVRRig(bool includeSelf)
        {
            VRRig random = GorillaParent.instance.vrrigs.ToArray()[UnityEngine.Random.Range(0, GorillaParent.instance.vrrigs.Count - 1)];
            if (includeSelf)
            {
                return random;
            }
            else
            {
                if (random != GorillaTagger.Instance.offlineVRRig)
                {
                    return random;
                }
                else
                {
                    return GetRandomVRRig(includeSelf);
                }
            }
        }

        public static VRRig GetClosestVRRig()
        {
            float num = float.MaxValue;
            VRRig outRig = null;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (Vector3.Distance(GorillaTagger.Instance.bodyCollider.transform.position, vrrig.transform.position) < num)
                {
                    num = Vector3.Distance(GorillaTagger.Instance.bodyCollider.transform.position, vrrig.transform.position);
                    outRig = vrrig;
                }
            }
            return outRig;
        }

        public static PhotonView GetPhotonViewFromVRRig(VRRig p)
        {
            return p.photonView;
        }

        public static Photon.Realtime.Player GetRandomPlayer(bool includeSelf)
        {
            if (includeSelf)
            {
                return PhotonNetwork.PlayerList[UnityEngine.Random.Range(0, PhotonNetwork.PlayerList.Length - 1)];
            } else
            {
                return PhotonNetwork.PlayerListOthers[UnityEngine.Random.Range(0, PhotonNetwork.PlayerListOthers.Length - 1)];
            }
        }

        public static Photon.Realtime.Player GetPlayerFromVRRig(VRRig p)
        {
            return GetPhotonViewFromVRRig(p).Owner;
        }

        public static string GetPlayerInfoAsString(Photon.Realtime.Player who)
        {
            VRRig[] rigs = GorillaParent.instance.vrrigs.ToArray();
            int i = rigs.Length;
            VRRig wat = rigs[i];
            wat.photonView.Owner = who;
            PhotonView whoo = wat.photonView;
            string playerinfo = $"Name: {whoo.Owner.NickName} , UID: {whoo.Owner.UserId} . Actr Numb: {whoo.Owner.ActorNumber}";
            return playerinfo;
        }

        public static Photon.Realtime.Player GetPlayerFromID(string id)
        {
            Photon.Realtime.Player found = null;
            foreach (Photon.Realtime.Player target in PhotonNetwork.PlayerList)
            {
                if (target.UserId == id)
                {
                    found = target;
                    break;
                }
            }
            return found;
        }
    }
}