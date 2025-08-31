using HarmonyLib;
using System;
using UnityEngine;


// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding

[HarmonyPatch]
public class AntiQuit
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Application), nameof(Application.Quit))]
    public static bool BlocQuit()
    {
        return false; 
    }
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Environment), nameof(Environment.Exit))]
    public static bool BlockExit(int exitCode)
    {
        return false; 
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(Environment), nameof(Environment.FailFast), typeof(string))]
    public static bool BlockEnt(string message)
    {
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(Environment), nameof(Environment.FailFast), typeof(string), typeof(Exception))]
    public static bool BlockFastException(string message, Exception exception)
    {
        return false;
    }
}
