using GorillaNetworking;
using Photon.Pun;
using UnityEngine;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX.Mods
{
    internal class Important
    {
        public static void QuitGame()
        {
            Application.Quit();
        }
        public static void AntiAFK()
        {
            PhotonNetworkController phc = GameObject.Find("Photon Manager").GetComponent<PhotonNetworkController>();
            phc.disableAFKKick = true;
        }

        public static void Reconnect()
        {
            PhotonNetwork.Disconnect();
            PhotonNetworkController phc = GameObject.Find("Photon Manager").GetComponent<PhotonNetworkController>();
            phc.AttemptToJoinSpecificRoom(Menu.Main.lastRoom);
        }
    }
}
