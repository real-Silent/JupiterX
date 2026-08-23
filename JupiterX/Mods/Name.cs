using Photon.Pun;
using System.IO;

namespace JupiterX.Mods
{
    public class Name
    {
        public static void MenuNameTag()
        {
            PhotonNetwork.LocalPlayer.NickName = "<color=cyan>JupiterX V2</color> <color=grey>By</color> <color=magenta>Nova</color>\nhttps://discord.gg/dtQdz59FJG";
        }

        public static void ChangeNameSpaz(string name, string[] colors)
        {
            int random = UnityEngine.Random.Range(0, colors.Length);
            PhotonNetwork.LocalPlayer.NickName = $"<color={colors[random]}>{name}</color>";
        }

        public static void ChangeName(string name, string color)
        {
            PhotonNetwork.LocalPlayer.NickName = "<color=" + color + ">" + name + "</color>";
        }

        public static void CustomName()
        {
            string filePath = Path.Combine(UnityEngine.Application.persistentDataPath, "JupiterX/CustomLocalName.txt");
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "your name here");
            }
            else
            {
                PhotonNetwork.LocalPlayer.NickName = File.ReadAllText(filePath);
            }
        }
    }
}