using GorillaNetworking;
using JupiterX.Menu;
using Photon.Pun;
using UnityEngine;

namespace JupiterX.Mods
{
    public class vRRig
    {
        public static void InvisMonke()
        {
            if (Utility.RightSecondary)
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
            if (Utility.RightPrimary)
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
            if (!PhotonNetwork.InRoom) return;

            VRRig myRig = GorillaTagger.Instance.myVRRig;
            myRig.enabled = false;
            Utility.GhostView(true);
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig == null || rig == myRig) continue;
                if (Time.time > delaytimebeesbleh)
                {
                    Vector3 targetPos = rig.headMesh.transform.position + new Vector3(0f, 0.6f, 0f);
                    myRig.transform.position = targetPos;
                    myRig.rightHandTransform.position -= myRig.transform.position * 0.3f;
                    myRig.leftHandTransform.position -= myRig.transform.position * 0.3f;
                    delaytimebeesbleh = Time.time + 0.3f;
                }
            }
        }

        public static void SpazRig()
        {
			(PhotonNetwork.InRoom ? GorillaTagger.Instance.myVRRig.head : GorillaTagger.Instance.offlineVRRig.head).rigTarget.eulerAngles = new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
			(PhotonNetwork.InRoom ? GorillaTagger.Instance.myVRRig.leftHand : GorillaTagger.Instance.offlineVRRig.leftHand).rigTarget.eulerAngles = new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
			(PhotonNetwork.InRoom ? GorillaTagger.Instance.myVRRig.rightHand : GorillaTagger.Instance.offlineVRRig.rightHand).rigTarget.eulerAngles = new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
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
            if (Utility.RightGrip)
            {
                Utility.myVRRig().enabled = false;
                Utility.myVRRig().transform.position = Utility.RightHandTransform().position;
                Utility.GhostView(true);
            }
            if (Utility.LeftGrip)
            {
                Utility.myVRRig().enabled = false;
                Utility.myVRRig().transform.position = Utility.LeftHandTransform().position;
                Utility.GhostView(true);
            }
            if (!Utility.LeftGrip || !Utility.RightGrip)
            {
                Utility.myVRRig().enabled = true;
                Utility.GhostView(false);
            }
        }
    }
}
