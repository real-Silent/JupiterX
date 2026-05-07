using HarmonyLib;

namespace JupiterX.Patches
{
    [HarmonyPatch(typeof(GorillaQuitBox), "OnBoxTriggered")]
    public class NoQuitBox
    {
        [HarmonyPrefix]
        private static bool Prefix()
        {
            return false;
        }
    }
}