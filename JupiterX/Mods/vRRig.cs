using ExitGames.Client.Photon;
using GorillaNetworking;
using JupiterX.Menu;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace JupiterX.Mods
{
    public class vRRig
    {
        public static void InvisMonke()
        {
            if (Utility.RSec)
            {
                Utility.myVRRig().enabled = false;
                Utility.myVRRig().transform.position = new Vector3(20397230f, 32423, 3432);
                Utility.GhostView(true);
            }
            else
            {
                Utility.myVRRig().enabled = true;
                Utility.GhostView(false);
            }

        }
        public static void GhostMonke()
        {
            if (Utility.RPrim)
            {
                Utility.myVRRig().enabled = false;
                Utility.GhostView(true);
            }
            else
            {
                Utility.myVRRig().enabled = true;
                Utility.GhostView(false);
            }
        }

        private static float delaytimebeesbleh;
        public static void Bees()
        {
            if (PhotonNetwork.InRoom)
            {
                Utility.myVRRig().enabled = false;
                Utility.GhostView(true);
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        if (Time.time > delaytimebeesbleh)
                        {
                            Utility.myVRRig().rightHandTransform.position -= Utility.myVRRig().transform.position * 0.3f;
                            Utility.myVRRig().leftHandTransform.position -= Utility.myVRRig().transform.position * 0.3f;
                            Utility.myVRRig().transform.position = rig.headMesh.transform.position + new Vector3(0f, 0.6f, 0f);
                            delaytimebeesbleh = Time.time + 0.3f;
                        }
                    }
                }
            }
        }

        public static void FixSpazRig()
        {
            Utility.myVRRig().head.trackingRotationOffset = Vector3.zero;
            Utility.myVRRig().rightHand.trackingRotationOffset = Vector3.zero;
            Utility.myVRRig().leftHand.trackingRotationOffset = Vector3.zero;
            Utility.GhostView(false);
        }

        public static void SpazRig()
        {
            Utility.GhostView(true);
            Utility.myVRRig().head.trackingRotationOffset += Utility.myVRRig().head.trackingRotationOffset = new Vector3(UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100));
            Utility.myVRRig().rightHand.trackingRotationOffset += Utility.myVRRig().rightHand.trackingRotationOffset = new Vector3(UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100));
            Utility.myVRRig().leftHand.trackingRotationOffset += Utility.myVRRig().leftHand.trackingRotationOffset = new Vector3(UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100));
        }
        public static void Strobe()
        {
            GorillaKeyboardButton buttanpress = new GorillaKeyboardButton();
            GorillaComputer.instance.colorCursorLine = Random.Range(0, 3);
            buttanpress.characterString = Random.Range(0, 10).ToString();
            GorillaComputer.instance.ProcessColorState(buttanpress);
        }
        public static void MoveRigGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    Utility.myVRRig().enabled = false;
                    Utility.myVRRig().transform.position = Ray.point;
                    Utility.GhostView(true);
                }
                else
                {
                    Utility.myVRRig().enabled = true;
                    Utility.GhostView(false);
                }
            }
        }

        public static void GrabRig()
        {
            if (Utility.RGrip)
            {
                Utility.myVRRig().enabled = false;
                Utility.myVRRig().transform.position = Utility.RightHandTransform().position;
                Utility.GhostView(true);
            }
            if (Utility.LGrip)
            {
                Utility.myVRRig().enabled = false;
                Utility.myVRRig().transform.position = Utility.LeftHandTransform().position;
                Utility.GhostView(true);
            }
            if (!Utility.LGrip || !Utility.RGrip)
            {
                Utility.myVRRig().enabled = true;
                Utility.GhostView(false);
            }
        }
    }
}
