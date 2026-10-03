// ============================================================
//  JupiterX | Notifications/LeavePatch.cs
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
using JupiterX.Menu;
using JupiterX.Notifications;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace JupiterX.Patches
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerLeftRoom")]
    public class LeavePatch
    {
        public static void Prefix(Player otherPlayer)
        {
            if (otherPlayer == null || string.IsNullOrEmpty(otherPlayer.UserId)) return;

            float currentTime = Time.realtimeSinceStartup;
            if (recentlyLeft.TryGetValue(otherPlayer.UserId, out float lastTime))
            {
                if (currentTime - lastTime < cooldownTime)
                    return;
            }
            recentlyLeft[otherPlayer.UserId] = currentTime;
            JoinPatch.ClearNotifiedUser(otherPlayer.UserId);

            if (otherPlayer != PhotonNetwork.LocalPlayer && !Main.disablePlayerNotifications)
                NotificationManager.SendNotification($"<color=grey>[</color><color=red>LEAVE</color><color=grey>]</color> Name: {Utility.CleanPlayerName(otherPlayer.NickName)}");
        }

        private static Dictionary<string, float> recentlyLeft = new Dictionary<string, float>();
        private const float cooldownTime = 5f;
    }
}