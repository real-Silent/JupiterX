using HarmonyLib;
using JupiterX.Menu;
using JupiterX.Notifications;
using Il2CppPhoton.Pun;
using Il2CppPhoton.Realtime;
using System.Collections.Generic;

namespace JupiterX
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerEnteredRoom")]
    public class JoinPatch
    {
        private static void Prefix(Player newPlayer)
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