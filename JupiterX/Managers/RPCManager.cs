using Photon.Pun;
using System.Linq;
using UnhollowerBaseLib;
using UnityEngine;

// JupiterX copyright 2026
/*
 - Please do not skid or say you made this file
 - This file is originally made by nova/silent
 - If this file goes into your mod menu you will be counted as a skidder
 - I have had this file private for 2-3 ish months because of this reason
 - If you want to use this give me credits somewhere
 */

namespace JupiterX.Managers
{
    public class RPCManager
    {
        public static void RigRPC(string methodname, RpcTarget target, object[] param)
        {
            var args = new Il2CppReferenceArray<Il2CppSystem.Object>(param.Length);
            for (int i = 0; i < param.Length; i++)
                args[i] = BoxManager.BoxAny(param[i]);
            GorillaTagger.Instance.myVRRig.photonView.RPC(methodname, target, args);
        }
        public static void RigRPC(string methodname, Photon.Realtime.Player target, object[] param)
        {
            var args = new Il2CppReferenceArray<Il2CppSystem.Object>(param.Length);
            for (int i = 0; i < param.Length; i++)
                args[i] = BoxManager.BoxAny(param[i]);
            GorillaTagger.Instance.myVRRig.photonView.RPC(methodname, target, args);
        }
        public static void GameRPC(string methodname, RpcTarget target, object[] param)
        {
            var args = new Il2CppReferenceArray<Il2CppSystem.Object>(param.Length);
            for (int i = 0; i < param.Length; i++)
                args[i] = BoxManager.BoxAny(param[i]);
            GorillaGameManager.instance.photonView.RPC(methodname, target, args);
        }
        public static void GameRPC(string methodname, Photon.Realtime.Player target, object[] param)
        {
            var args = new Il2CppReferenceArray<Il2CppSystem.Object>(param.Length);
            for (int i = 0; i < param.Length; i++)
                args[i] = BoxManager.BoxAny(param[i]);
            GorillaGameManager.instance.photonView.RPC(methodname, target, args);
        }

        public static void GTDoorRPC(string methodname, RpcTarget target, object[] param)
        {
            var args = new Il2CppReferenceArray<Il2CppSystem.Object>(param.Length);
            for (int i = 0; i < param.Length; i++)
                args[i] = BoxManager.BoxAny(param[i]);
            GameObject.FindObjectsOfType<GTDoor>().FirstOrDefault().photonView.RPC(methodname, target, args);
        }
        public static void GTDoorRPC(string methodname, Photon.Realtime.Player target, object[] param)
        {
            var args = new Il2CppReferenceArray<Il2CppSystem.Object>(param.Length);
            for (int i = 0; i < param.Length; i++)
                args[i] = BoxManager.BoxAny(param[i]);
            GameObject.FindObjectsOfType<GTDoor>().FirstOrDefault().photonView.RPC(methodname, target, args);
        }
    }
}