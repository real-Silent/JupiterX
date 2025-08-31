using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX.Patches
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerLeftRoom")]
    public class LeavePatch
    {
        private static void Prefix(Player otherPlayer)
        {
            if (otherPlayer != PhotonNetwork.LocalPlayer && otherPlayer != a && !Settings.disableNotis)
            {
                NotificationManager.SendNotification("red", "LEAVE", "Name: " + otherPlayer.NickName);
                a = otherPlayer;
            }
        }

        private static Player a;
    }
}