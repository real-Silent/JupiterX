using HarmonyLib;
using JupiterX.Notifications;
using Photon.Pun;

namespace JupiterX.Patches
{
    [HarmonyPatch(typeof(GorillaNot), "SendReport")]
    public class AntiCheatPatches
    {
        public static bool AntiCheatSelf;
        public static bool AntiCheatAll;

        public static void Prefix(string susReason, string susId, string susNick)
        {
            if (AntiCheatSelf)
            {
                if (susId == PhotonNetwork.LocalPlayer.UserId)
                    NotificationManager.SendNotification($"<color=cyan>[ANTICHEAT]</color> AntiCheat Reported you for {susReason}.");
            }
            if (AntiCheatAll)
            {
                NotificationManager.SendNotification($"<color=cyan>[ANTICHEAT]</color> AntiCheat Reported {susNick} for {susReason}.");
            }
        }
    }
}