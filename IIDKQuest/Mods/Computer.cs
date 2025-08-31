using GorillaNetworking;
using Photon.Pun;
using UnityEngine;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX.Mods
{
    internal class Computer
    {
        public static void Reconnect()
        {
            string roomname = PhotonNetwork.CurrentRoom.Name;
            if (string.IsNullOrEmpty(roomname))
            {
                return;
            }
            PhotonNetworkController phc = GameObject.Find("Photon Manager").GetComponent<PhotonNetworkController>();
            phc.AttemptToJoinSpecificRoom(roomname);
        }
        public static void Leave()
        {
            PhotonNetworkController phc = GameObject.Find("Photon Manager").GetComponent<PhotonNetworkController>();
            phc.AttemptDisconnect();
        }
        public static void Jrr()
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }
        public static void JoinCode(string code)
        {
            PhotonNetworkController phc = GameObject.Find("Photon Manager").GetComponent<PhotonNetworkController>();
            phc.AttemptToJoinSpecificRoom(code);
        }
    }
}
