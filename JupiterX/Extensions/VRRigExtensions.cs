using System.Linq;
using UnityEngine;

namespace JupiterX.Extensions
{
    public static class VRRigExtensions
    {
        public static bool IsTagged(this VRRig rig)
        {
            GorillaTagManager tagman = GameObject.FindObjectsOfType<GorillaTagManager>().FirstOrDefault();
            if (rig.mainSkin.material.name.Contains("fected") || (tagman != null && tagman.currentInfected.Contains(rig.photonView.Owner)))
            {
                return true;
            }
            return false;
        }

        public static string Platforms(this VRRig rig)
        {
            if (rig.IsPlayerSteam())
                return "Steam";
            return "Quest";
        }

        public static Color playerColor(this VRRig rig) 
        {
            Color rigC = rig.mainSkin.material.color;
            return new Color(rigC.r, rigC.g, rigC.b);
        }

        public static bool IsPlayerSteam(this VRRig rig)
        {
            if (rig.concatStringOfCosmeticsAllowed.Contains("S. FIRST LOGIN"))
            {
                return true;
            }
            return false;
        }
    }
}