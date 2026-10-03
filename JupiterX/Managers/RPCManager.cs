// ============================================================
//  JupiterX | Managers/RPCManager.cs
//  Copyright (c) 2026 Jupiterx (@NAuth). All rights reserved.
//
//  This software and its source code are the property of the
//  author. Unauthorized copying, redistribution, modification,
//  or reuse of any part of this project, in whole or in part,
//  without express written permission is strictly prohibited.
//
//  Version: 2.0.0 | By Silent/Ashley/Nova (@s1lnt)
// ============================================================

using Photon.Pun;
using System.Linq;
using UnhollowerBaseLib;
using UnityEngine;

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