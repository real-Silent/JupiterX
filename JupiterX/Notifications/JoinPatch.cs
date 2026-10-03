// ============================================================
//  JupiterX | Notifications/JoinPatch.cs
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

namespace JupiterX
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerEnteredRoom")]
    public class JoinPatch
    {
        public static void Prefix(Player newPlayer)
        {
            if (newPlayer == null || string.IsNullOrEmpty(newPlayer.UserId)) return;

            if (newPlayer != PhotonNetwork.LocalPlayer && !notifiedPlayerIds.Contains(newPlayer.UserId) && !Main.disablePlayerNotifications)
            {
                notifiedPlayerIds.Add(newPlayer.UserId);
                NotificationManager.SendNotification($"<color=grey>[</color><color=green>JOIN</color><color=grey>]</color> Name: {Utility.CleanPlayerName(newPlayer.NickName)}");
            }
        }
        private static HashSet<string> notifiedPlayerIds = new HashSet<string>();

        public static void ClearNotifiedUser(string userId) =>
            notifiedPlayerIds.Remove(userId);
    }
}