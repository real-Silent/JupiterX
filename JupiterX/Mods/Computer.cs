using Photon.Pun;

namespace JupiterX.Mods
{
    public class Computer
    {
        public static void Reconnect()
        {
            string roomname = PhotonNetwork.CurrentRoom.Name;
            if (string.IsNullOrEmpty(roomname))
                return;
            Utility.photonNetworkController.AttemptToJoinSpecificRoom(roomname);
        }
        public static void Leave()
        {
            Utility.photonNetworkController.AttemptDisconnect();
        }
        public static void Jrr()
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }
        public static void JoinCode(string code)
        {
            Utility.photonNetworkController.AttemptToJoinSpecificRoom(code);
        }
    }
}