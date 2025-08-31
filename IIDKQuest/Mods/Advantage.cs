using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
using static JupiterX.Menu.Main;
using JupiterX.Menu;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX.Mods
{
    internal class Advantage
    {
        public static void TagAll()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                foreach (GorillaTagManager tagman in GameObject.FindObjectsOfType<GorillaTagManager>())
                {
                    foreach (Photon.Realtime.Player plr in PhotonNetwork.PlayerListOthers)
                    {
                        tagman.AddInfectedPlayer(plr);
                    }
                }
            }
            else
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                    {
                        if (Utility.myVRRig().mainSkin.material.name.Contains("fected") && !rig.mainSkin.material.name.Contains("fected"))
                        {
                            Utility.myVRRig().enabled = false;
                            Utility.myVRRig().transform.position = rig.headConstraint.transform.position;
                            Utility.myVRRig().rightHandTransform.transform.position = rig.headConstraint.transform.position;
                            Utility.RightHandTransform().position = rig.headConstraint.transform.position;
                            NotificationManager.SendNotification("yellow", "TAGGED", "Tagged All");
                        }
                        else
                        {
                            Utility.myVRRig().enabled = true;
                            NotificationManager.SendNotification("red", "ERROR", "You are not tagged");
                        }
                    }
                }
            }
        }

        public static void TagAura()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                {
                    if (Utility.myVRRig().mainSkin.material.name.Contains("fected") && !rig.mainSkin.material.name.Contains("fected"))
                    {
                        float dis = Vector3.Distance(Utility.MainTransform().position, rig.headConstraint.transform.position);
                        if (dis < 0.75f)
                        {
                            Utility.myVRRig().rightHandTransform.transform.position = rig.headConstraint.transform.position;
                            Utility.RightHandTransform().position = rig.headConstraint.transform.position;
                        }
                    }
                }
            }
        }

        public static void TagGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
                RaycastHit Ray = GunData.Ray;


                if (Main.gunLocked && Main.lockTarget != null)
                {
                    if (Utility.IsMaster() == false)
                        Utility.SetMaster(PhotonNetwork.LocalPlayer);
                    foreach (GorillaTagManager tagman in GameObject.FindObjectsOfType<GorillaTagManager>())
                    {
                        tagman.AddInfectedPlayer(Main.lockTarget.photonView.Owner);
                    }
                }

                if (Main.GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who)
                    {
                        Main.gunLocked = true;
                        Main.lockTarget = who;
                    }
                }
            }
            else
            {
                if (Main.gunLocked)
                    Main.gunLocked = false;
                JupiterX.Menu.Main.DestroyGun();
            }
        }

        public static void TagGunRPC()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
                RaycastHit Ray = GunData.Ray;


                if (Main.gunLocked && Main.lockTarget != null)
                {
                    if (Utility.IsMaster() == false)
                        Utility.SetMaster(PhotonNetwork.LocalPlayer);
                    foreach (GorillaTagManager tagman in GameObject.FindObjectsOfType<GorillaTagManager>())
                    {
                        tagman.AddInfectedPlayer(Main.lockTarget.photonView.Owner);
                    }
                }

                if (Main.GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who)
                    {
                        Main.gunLocked = true;
                        Main.lockTarget = who;
                    }
                }
            }
            else
            {
                if (Main.gunLocked)
                    Main.gunLocked = false;
                JupiterX.Menu.Main.DestroyGun();
            }
        }

        public static void FlickTagGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject NewPointer = GunData.NewPointer;

                if (GetGunInput(true))
                {
                    GorillaLocomotion.Player.Instance.rightHandTransform.position = NewPointer.transform.position;
                }
            }
            else
            {
                DestroyGun();
            }
        }
    }
}
