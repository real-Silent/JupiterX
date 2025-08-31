using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerEnteredRoom")]
    public class JoinPatch
    {
        private static void Prefix(Player newPlayer)
        {
            if (newPlayer != oldnewplayer && !Settings.disableNotis)
            {
                NotificationManager.SendNotification("green", "JOIN",  "Name: " + newPlayer.NickName);
                oldnewplayer = newPlayer;
            }
        }

        private static Player oldnewplayer;
    }
}