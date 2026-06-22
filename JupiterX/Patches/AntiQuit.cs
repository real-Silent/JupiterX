using HarmonyLib;
using System;
using UnityEngine;

[HarmonyPatch]
internal class AntiQuit
{
    [HarmonyPatch(typeof(Application), nameof(Application.Quit))]
    [HarmonyPrefix]
    private static bool BlockQuit()
    {
        return false;
    }

    [HarmonyPatch(typeof(Environment), nameof(Environment.Exit))]
    [HarmonyPrefix]
    private static bool BlockExit(int exitCode)
    {
        return false;
    }

    [HarmonyPatch(typeof(Environment), nameof(Environment.FailFast), typeof(string))]
    [HarmonyPrefix]
    private static bool BlockFailFast(string message)
    {
        return false;
    }

    [HarmonyPatch(typeof(Environment), nameof(Environment.FailFast), typeof(string), typeof(Exception))]
    [HarmonyPrefix]
    private static bool BlockFailFastException(string message, Exception exception)
    {
        return false;
    }
}