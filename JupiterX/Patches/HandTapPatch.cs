using HarmonyLib;
using System.Reflection;

namespace JupiterX.Patches
{
    [HarmonyPatch]
    internal class HandTapPatch
    {
        public static bool enabled;

        static MethodBase TargetMethod()
        {
            var type = AccessTools.TypeByName("VRRig");
            if (type == null)
                return null;
            return AccessTools.Method(type, "PlayHandTapLocal", new[] { typeof(int), typeof(bool), typeof(float), typeof(bool) }) ?? AccessTools.Method(type, "PlayHandTap", new[] { typeof(int), typeof(bool), typeof(float) });
        }

        static bool Prefix()
        {
            return !enabled;
        }
    }
}