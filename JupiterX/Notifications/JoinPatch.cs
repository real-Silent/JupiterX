using HarmonyLib;
using JupiterX.Menu;
using JupiterX.Notifications;
using Photon.Pun;
using Photon.Realtime;

namespace JupiterX
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerEnteredRoom")]
    public class JoinPatch
    {
        private static void Prefix(Player player)
        {
            if (player != PhotonNetwork.LocalPlayer && !Main.disablePlayerNotifications)
                NotificationManager.SendNotification($"<color=grey>[</color><color=green>JOIN</color><color=grey>]</color> Name: {Utility.CleanPlayerName(player.NickName)}");
        }
    }
}