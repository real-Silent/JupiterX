using JupiterX.Menu;
using JupiterX.Notifications;
using Photon.Pun;
using UnityEngine;
using static JupiterX.Menu.Main;

namespace JupiterX.Mods
{
    public class Advantage
    {
        public static void TagAll()
        {
            if (Utility.IsMaster())
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
                        if (Utility.ActualRig().mainSkin.material.name.Contains("fected") && !rig.mainSkin.material.name.Contains("fected"))
                        {
                            Utility.ActualRig().enabled = false;
                            Utility.ActualRig().transform.position = rig.headConstraint.transform.position;
                            Utility.ActualRig().rightHandTransform.transform.position = rig.headConstraint.transform.position;
                            Utility.RightHandTransform().position = rig.headConstraint.transform.position;
                            NotificationManager.SendNotification("<color=yellow>[INFO]</color> Tagged all!", 7f);
                        }
                        else
                        {
                            Utility.ActualRig().enabled = true;
                            NotificationManager.SendNotification("<color=red>[ERROR]</color> You are not tagged.", 10f);
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
                    if (Utility.ActualRig().mainSkin.material.name.Contains("fected") && !rig.mainSkin.material.name.Contains("fected"))
                    {
                        float dis = Vector3.Distance(Utility.MainTransform().position, rig.headConstraint.transform.position);
                        if (dis < 0.75f)
                        {
                            Utility.ActualRig().rightHandTransform.transform.position = rig.headConstraint.transform.position;
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
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who)
                    {
                        GorillaGameManager.instance.GetComponent<PhotonView>().RPC(
                                "ReportTagRPC",
                                RpcTarget.MasterClient,
                                new Il2CppSystem.Object[] { who.photonView.Owner }
                            );
                    }
                }
            }
        }

        public static void TagGunRPC()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.gunLocked && Main.lockTarget != null)
                {
                    Utility.MakeMeMaster();
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
                Main.lockTarget = null;
                if (Main.gunLocked)
                    Main.gunLocked = false;
            }
        }

        public static void FlickTagGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject NewPointer = GunData.Pointer;
                if (GetGunInput(true))
                {
                    Utility.RightHandTransform().position = NewPointer.transform.position;
                }
            }
        }
    }
}
