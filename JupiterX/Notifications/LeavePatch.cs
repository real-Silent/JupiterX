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
        private static void Prefix(Player otherPlayer)
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