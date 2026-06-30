using HarmonyLib;
using System;
using UnityEngine;

namespace JupiterX.Patches
{
    [HarmonyPatch]
    internal class AntiQuit
    {
        [HarmonyPatch(typeof(Application), nameof(Application.Quit), new Type[] { })]
        [HarmonyPrefix]
        private static bool BlockQuit()
        {
            return false;
        }

        [HarmonyPatch(typeof(Application), nameof(Application.Quit), new Type[] { typeof(int) })]
        [HarmonyPrefix]
        private static bool BlockQuitWithCode(int exitCode)
        {
            return false;
        }

        [HarmonyPatch(typeof(Environment), nameof(Environment.Exit), new Type[] { typeof(int) })]
        [HarmonyPrefix]
        private static bool BlockExit(int exitCode)
        {
            return false;
        }

        [HarmonyPatch(typeof(Environment), nameof(Environment.FailFast), new Type[] { typeof(string) })]
        [HarmonyPrefix]
        private static bool BlockFailFast(string message)
        {
            return false;
        }

        [HarmonyPatch(typeof(Environment), nameof(Environment.FailFast), new Type[] { typeof(string), typeof(Exception) })]
        [HarmonyPrefix]
        private static bool BlockFailFastException(string message, Exception exception)
        {
            return false;
        }
    }
}