using Photon.Pun;

namespace JupiterX.Mods
{
    public class Name
    {
        public static void MenuNameTag()
        {
            PhotonNetwork.LocalPlayer.NickName = "<color=cyan>JupiterX V2</color> <color=grey>By</color> <color=magenta>Nova</color>\nhttps://discord.gg/dtQdz59FJG";
        }
        public static void ChangeName(string name, string color)
        {
            PhotonNetwork.LocalPlayer.NickName = "<color=" + color + ">" + name + "</color>";
        }
    }
}