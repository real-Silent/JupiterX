// ============================================================
//  JupiterX | Extensions/VRRigExtensions.cs
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
using UnityEngine;

namespace JupiterX.Extensions
{
    public static class VRRigExtensions
    {
        public static bool IsTagged(this VRRig rig)
        {
            GorillaTagManager tagman = GameObject.FindObjectsOfType<GorillaTagManager>().FirstOrDefault();
            if (rig.mainSkin.material.name.Contains("fected") || (tagman != null && tagman.currentInfected.Contains(rig.photonView.Owner)))
            {
                return true;
            }
            return false;
        }

        public static string Platforms(this VRRig rig)
        {
            if (rig.IsPlayerSteam())
                return "Steam";
            return "Quest";
        }

        public static Color playerColor(this VRRig rig) 
        {
            Color rigC = rig.mainSkin.material.color;
            return new Color(rigC.r, rigC.g, rigC.b);
        }

        public static bool IsLocal(this VRRig rig)
        {
            if (rig == null || GorillaTagger.Instance == null)
                return false;
            return PhotonNetwork.InRoom ? rig == GorillaTagger.Instance.myVRRig  : rig == GorillaTagger.Instance.offlineVRRig;
        }

        public static bool IsPlayerSteam(this VRRig rig)
        {
            if (rig.concatStringOfCosmeticsAllowed.Contains("S. FIRST LOGIN"))
            {
                return true;
            }
            return false;
        }
    }
}