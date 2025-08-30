using HarmonyLib;
using System;
using UnityEngine;

[HarmonyPatch]
public class AntiQuit
{
    // 1. Block Application.Quit()
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Application), nameof(Application.Quit))]
    public static bool BlockApplicationQuit()
    {
        MelonLoader.MelonLogger.Msg("[AntiQuit] Blocked Application.Quit()");
        return false; // skip original quit
    }

    // 2. Block Environment.Exit(int)
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Environment), nameof(Environment.Exit))]
    public static bool BlockEnvironmentExit(int exitCode)
    {
        MelonLoader.MelonLogger.Msg($"[AntiQuit] Blocked Environment.Exit({exitCode})");
        return false; // skip original exit
    }

    // Optional: block Environment.FailFast as well (crash quit)
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Environment), nameof(Environment.FailFast), typeof(string))]
    public static bool BlockEnvironmentFailFast(string message)
    {
        MelonLoader.MelonLogger.Msg($"[AntiQuit] Blocked Environment.FailFast: {message}");
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(Environment), nameof(Environment.FailFast), typeof(string), typeof(Exception))]
    public static bool BlockEnvironmentFailFastException(string message, Exception exception)
    {
        MelonLoader.MelonLogger.Msg($"[AntiQuit] Blocked Environment.FailFast with exception: {message}");
        return false;
    }
}
