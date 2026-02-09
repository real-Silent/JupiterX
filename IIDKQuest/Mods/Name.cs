using Photon.Pun;

namespace JupiterX.Mods
{
    internal class Name
    {
        public static void MenuNameTag()
        {
            PhotonNetwork.LocalPlayer.NickName = "<color=cyan>JupiterX V2</color> <color=grey>By</color> <color=red>Silent</color>\nhttps://discord.gg/ueFrRsKvVT";
        }
        public static void ChangeName(string name)
        {
            PhotonNetwork.LocalPlayer.NickName = name;
        }
    }
}