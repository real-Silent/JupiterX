using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX.Patches
{
    [HarmonyPatch(typeof(GorillaQuitBox), "OnBoxTriggered")]
    internal class NoQuitBox
    {
        [HarmonyPrefix]
        private static bool Prefix()
        {
            return false;
        }
    }
}
