// ============================================================
//  JupiterX | Patches/QuitBoxPatch.cs
//  Copyright (c) 2026 Jupiterx (@NAuth). All rights reserved.
//
//  This software and its source code are the property of the
//  author. Unauthorized copying, redistribution, modification,
//  or reuse of any part of this project, in whole or in part,
//  without express written permission is strictly prohibited.
//
//  Version: 2.0.0 | By Silent/Ashley/Nova (@s1lnt)
// ============================================================

using HarmonyLib;

namespace JupiterX.Patches
{
    [HarmonyPatch(typeof(GorillaQuitBox), "OnBoxTriggered")]
    public class QuitBoxPatch
    {
        public static bool enabled = true;
        public static bool teleportToStump;

        public static bool Prefix()
        {
            if (teleportToStump)
            {
                GorillaLocomotion.Player.Instance.transform.position = new UnityEngine.Vector3(-67.0116f, 12.5f, 82.4668f);
                return false;
            }
            return enabled;
        }
    }
}