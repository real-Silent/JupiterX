using JupiterX.Mods;
using HarmonyLib;

namespace JupiterX.Patches
{
    [HarmonyPatch(typeof(GorillaQuitBox), "OnBoxTriggered")]
    public class QuitBoxPatch
    {
        public static bool enabled = true;
        public static bool teleportToStump;

        public static bool Prefix()
        {
            if (teleportToStump)
            {
                GorillaLocomotion.Player.Instance.transform.position = new UnityEngine.Vector3(-67.0116f, 12.5f, 82.4668f);
                return false;
            }
            return enabled;
        }
    }
}