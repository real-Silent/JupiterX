using Photon.Pun;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX.Mods
{
    internal class Name
    {
        public static void MenuNameTag()
        {
            PhotonNetwork.LocalPlayer.NickName = "<color=#0333A4>JupiterX Menu | By Silent</color>\ndiscord.gg/zmbGV74y8W";
        }
        public static void ChangeName(string name)
        {
            PhotonNetwork.LocalPlayer.NickName = name;
        }
    }
}
