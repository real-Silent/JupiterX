using JupiterX.Menu;
using Photon.Pun;
using UnityEngine;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX.Mods
{
    internal class Prefabs
    {
        public static void NetworkPlayerSpam()
        {
            PlayerPrefs.SetString("username", "JupiterX By Silent\nBest Mod Menu");

            if (Utility.RGrip)
                Utility.BetaSpawnPrefab("Network Player", Utility.RightHandTransform().position, Utility.RightHandTransform().rotation);
            if (Utility.LGrip)
                Utility.BetaSpawnPrefab("Network Player", Utility.LeftHandTransform().position, Utility.LeftHandTransform().rotation);
        }
        public static void EnemySpam()
        {
            if (Utility.RGrip)
                Utility.BetaSpawnPrefab("gorillaprefabs/gorillaenemy", Utility.RightHandTransform().position, Utility.RightHandTransform().rotation);
            if (Utility.LGrip)
                Utility.BetaSpawnPrefab("gorillaprefabs/gorillaenemy", Utility.LeftHandTransform().position, Utility.LeftHandTransform().rotation);
        }

        public static void ClearPrefabs()
        {
            Utility.SetMaster(Photon.Pun.PhotonNetwork.LocalPlayer);
            Photon.Pun.PhotonNetwork.DestroyAll();
        }
        public static void TargetSpam()
        {
            if (Utility.RGrip)
                Utility.BetaSpawnPrefab("STICKABLE TARGET", Utility.RightHandTransform().position, Utility.RightHandTransform().rotation);
            if (Utility.LGrip)
                Utility.BetaSpawnPrefab("STICKABLE TARGET", Utility.LeftHandTransform().position, Utility.LeftHandTransform().rotation);
        }

        public static void GiveSpamGun(int type)
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
                RaycastHit Ray = GunData.Ray;


                if (Main.gunLocked && Main.lockTarget != null)
                {
                    if (Main.lockTarget.rightMiddle.calcT > 0.5f) // fix for this ??
                    {
                        switch (type)
                        {
                            case 0: Utility.BetaSpawnPrefab("bulletPrefab", Main.lockTarget.rightHandTransform.position, Main.lockTarget.rightHandTransform.rotation); break;
                            case 1: Utility.BetaSpawnPrefab("STICKABLE TARGET", Main.lockTarget.rightHandTransform.position, Main.lockTarget.rightHandTransform.rotation); break;
                            case 2: Utility.BetaSpawnPrefab("Network Player", Main.lockTarget.rightHandTransform.position, Main.lockTarget.rightHandTransform.rotation); break;
                            case 3: Utility.BetaSpawnPrefab("gorillaprefabs/gorillaenemy", Main.lockTarget.rightHandTransform.position, Main.lockTarget.rightHandTransform.rotation); break;
                        }
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

        public static void CubeSpam()
        {
            if (Utility.RGrip)
                Utility.BetaSpawnPrefab("bulletPrefab", Utility.RightHandTransform().position, Utility.RightHandTransform().rotation);
            if (Utility.LGrip)
                Utility.BetaSpawnPrefab("bulletPrefab", Utility.LeftHandTransform().position, Utility.LeftHandTransform().rotation);
        }
        public static void CubeGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    Utility.BetaSpawnPrefab("bulletPrefab", NewPointer.transform.position, NewPointer.transform.rotation);
                }
            }
            else
            {
                JupiterX.Menu.Main.DestroyGun();
            }
        }

        public static void TargetGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    Utility.BetaSpawnPrefab("STICKABLE TARGET", NewPointer.transform.position, NewPointer.transform.rotation);
                }
            }
            else
            {
                JupiterX.Menu.Main.DestroyGun();
            }
        }

        public static void NetworkPlayerGun() 
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    PlayerPrefs.SetString("username", "JupiterX By Silent\nBest Mod Menu");
                    Utility.BetaSpawnPrefab("Network Player", NewPointer.transform.position, NewPointer.transform.rotation);
                }
            }
            else
            {
                JupiterX.Menu.Main.DestroyGun();
            }
        }

        public static void EnemyGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    Utility.BetaSpawnPrefab("gorillaprefabs/gorillaenemy", NewPointer.transform.position, NewPointer.transform.rotation);
                }
            }
            else
            {
                JupiterX.Menu.Main.DestroyGun();
            }
        }
    }
}
