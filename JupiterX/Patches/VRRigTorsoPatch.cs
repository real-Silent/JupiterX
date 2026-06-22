using HarmonyLib;
using JupiterX.Mods;
using UnityEngine;

namespace JupiterX.Patches
{
    [HarmonyPatch(typeof(VRRig), "LateUpdate")]
    class VRRigTorsoPatch
    {
        public static bool enable;
        [HarmonyPostfix]
        public static void Postfix(VRRig __instance)
        {
            if (enable)
            {
                if (__instance == GorillaTagger.Instance.myVRRig)
                {
                    __instance.transform.rotation = Quaternion.Euler(0f, vRRig.yRotation, 0f);
                    float scaleFactor = __instance.transform.localScale.x;
                    __instance.head.MapMine(scaleFactor, __instance.playerOffsetTransform);
                    __instance.rightHand.MapMine(scaleFactor, __instance.playerOffsetTransform);
                    __instance.leftHand.MapMine(scaleFactor, __instance.playerOffsetTransform);
                }
            }
        }
    }
}