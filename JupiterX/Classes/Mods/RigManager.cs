using Il2CppPhoton.Realtime;
using Il2CppPhoton.Pun;
using UnityEngine;
using System;
using Il2Cpp;

namespace JupiterX.Classes
{

    [MelonLoader.RegisterTypeInIl2Cpp]
    public class RigManager : MonoBehaviour
    {
        public RigManager(IntPtr ptr ) : base(ptr) { }

        public static VRRig GetVRRigFromPlayer(Il2CppPhoton.Realtime.Player p)
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

        public static Il2CppPhoton.Realtime.Player GetRandomPlayer(bool includeSelf)
        {
            if (includeSelf)
            {
                return PhotonNetwork.PlayerList[UnityEngine.Random.Range(0, PhotonNetwork.PlayerList.Length - 1)];
            } else
            {
                return PhotonNetwork.PlayerListOthers[UnityEngine.Random.Range(0, PhotonNetwork.PlayerListOthers.Length - 1)];
            }
        }

        public static Il2CppPhoton.Realtime.Player GetPlayerFromVRRig(VRRig p)
        {
            return GetPhotonViewFromVRRig(p).Owner;
        }

        public static string GetPlayerInfoAsString(Il2CppPhoton.Realtime.Player who)
        {
            VRRig[] rigs = GorillaParent.instance.vrrigs.ToArray();
            int i = rigs.Length;
            VRRig wat = rigs[i];
            wat.photonView.Owner = who;
            PhotonView whoo = wat.photonView;
            string playerinfo = $"Name: {whoo.Owner.NickName} , UID: {whoo.Owner.UserId} . Actr Numb: {whoo.Owner.ActorNumber}";
            return playerinfo;
        }

        public static Il2CppPhoton.Realtime.Player GetPlayerFromID(string id)
        {
            Il2CppPhoton.Realtime.Player found = null;
            foreach (Il2CppPhoton.Realtime.Player target in PhotonNetwork.PlayerList)
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