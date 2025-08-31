using easyInputs;
using JupiterX.Menu;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX.Mods
{
    internal class Movement
    {
        public static string[] FlySpeeds = { "Very Slow", "Slow", "Normal", "Fast", "Very Fast", "Way Too Fast" };
        public static int FlySpeedAmount = 0;
        public static float FlySpeed = 1f;

        public static void ChangeFlySpeed()
        {
            FlySpeedAmount = (FlySpeedAmount + 1) % FlySpeeds.Length;

            switch (FlySpeedAmount)
            {
                case 0: FlySpeed = 1f; break;   
                case 1: FlySpeed = 3f; break;   
                case 2: FlySpeed = 7f; break;   
                case 3: FlySpeed = 14f; break;  
                case 4: FlySpeed = 18f; break;  
                case 5: FlySpeed = 30f; break;  
            }
            Main.GetIndex("Change Fly Speed").overlapText =
                $"Change Fly Speed <color=cyan>[{FlySpeeds[FlySpeedAmount]}]</color>";
        }

        public static void Fly()
        {
            if (Utility.RPrim)
            {
                Utility.RigidbodyTransform().transform.position += Utility.Head().transform.forward * Time.deltaTime * FlySpeed;
                Utility.RigidbodyTransform().velocity = Vector3.zero;
            }
        }

        public static void Mosaboost()
        {
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 9.5f;
        }

        public static void Speedboost()
        {
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 9.7f;
            GorillaLocomotion.Player.Instance.jumpMultiplier = 11.2f;
        }

        public static void FollowPlayerGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;
                RaycastHit Ray = GunData.Ray;


                if (Main.gunLocked && Main.lockTarget != null)
                {
                    Utility.myVRRig().enabled = false;
                    Utility.myVRRig().transform.position = Main.lockTarget.transform.position;
                    Utility.myVRRig().rightHandTransform.position = Main.lockTarget.rightHandTransform.position;
                    Utility.myVRRig().rightHandTransform.rotation = Main.lockTarget.rightHandTransform.rotation;
                    Utility.myVRRig().leftHandTransform.position = Main.lockTarget.leftHandTransform.position;
                    Utility.myVRRig().leftHandTransform.rotation = Main.lockTarget.leftHandTransform.rotation;
                    Utility.myVRRig().headConstraint.transform.position = Main.lockTarget.headConstraint.transform.position;
                    Utility.myVRRig().headConstraint.transform.rotation = Main.lockTarget.headConstraint.transform.rotation;
                    Utility.GhostView(true);
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

                Utility.myVRRig().enabled = true;
                Utility.GhostView(false);
            }
        }

        private static List<Collider> colliders = new List<Collider>();
        private static bool collidersCached = false;
        private static bool lastState = false;

        public static void NoClip(bool enabled)
        {
            // Cache colliders only once
            if (!collidersCached)
            {
                colliders.AddRange(Object.FindObjectsOfType<Collider>()); // include inactive too
                collidersCached = true;
            }

            // Only update if the state actually changes
            if (lastState != enabled)
            {
                foreach (var col in colliders)
                {
                    if (col != null)
                        col.enabled = !enabled;
                }

                lastState = enabled;
            }
        }

        public static void CarMonke()
        {
            if (Utility.RTrigger)
                Utility.RigidbodyTransform().velocity += Utility.Head().forward / 2f;
            if (Utility.LTrigger)
                Utility.RigidbodyTransform().velocity += -Utility.Head().forward / 2f;
        }

        public static void SlingShotFly()
        {
            if (Utility.RPrim)
            {
                Utility.RigidbodyTransform().velocity += Utility.Head().transform.forward / 2f;
            }
        }

        public static void TFly()
        {
            if (Utility.RTrigger)
            {
                Utility.RigidbodyTransform().transform.position += Utility.Head().transform.forward * Time.deltaTime * FlySpeed;
                Utility.RigidbodyTransform().velocity = Vector3.zero;
            }
        }

        public static void ExcelFly()
        {
            if (Utility.RPrim) Utility.RigidbodyTransform().velocity += Utility.RightHandTransform().right / 2f;
            if (Utility.LPrim) Utility.RigidbodyTransform().velocity += -Utility.LeftHandTransform().right / 2f;
        }


        public static string[] ArmSizes = { "Steam", "Long", "Very Long", "Ghost", "Small" };
        public static int ArmSizeAmount = 0;
        public static Vector3 ArmSize = new Vector3(1.15f, 1.15f, 1.15f);
        public static void ChangeArmLength()
        {
            ArmSizeAmount = (ArmSizeAmount + 1) % ArmSizes.Length;

            switch (ArmSizeAmount)
            {
                case 0: ArmSize = new Vector3(1.15f, 1.15f, 1.15f); break;
                case 1: ArmSize = new Vector3(1.25f, 1.25f, 1.25f); break;
                case 2: ArmSize = new Vector3(1.5f, 1.5f, 1.5f); break;
                case 3: ArmSize = new Vector3(1.3f, 1.3f, 1.3f); break;
                case 4: ArmSize = new Vector3(0.8f, 0.8f, 0.8f); break;
            }

            Main.GetIndex("Change Arm Length").overlapText = $"Change Arm Length <color=cyan>[{ArmSizes[ArmSizeAmount]}]</color>";
        }
        public static void LongArms(bool off)
        {
            if (off)
                Utility.MainTransform().localScale = new Vector3(1f, 1f, 1f);
            else
                Utility.MainTransform().localScale = ArmSize;
            
        }
        public static void NoTagFreeze(int type)
        {
            switch (type) { case 0: Utility.GetTagFreeze(true); break; case 1: Utility.GetTagFreeze(false); break; }
        }
        public static void SpeedBoost()
        {
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 9f; GorillaLocomotion.Player.Instance.jumpMultiplier = 13f;
        }

        public static void Platforms()
        {
            Utility.CreatePlatform(Utility.RightHandTransform(), Utility.LeftHandTransform(), Utility.RightHandTransform().rotation, Utility.LeftHandTransform().rotation, new Vector3(0.0125f, 0.28f, 0.3825f), Color.grey);
        }

        static bool hasTped = false;
        public static void TPGun()
        {
            if (JupiterX.Menu.Main.GetGunInput(false))
            {
                var GunData = JupiterX.Menu.Main.RenderGun();
                GameObject NewPointer = GunData.NewPointer;


                if (JupiterX.Menu.Main.GetGunInput(true))
                {
                    if (!hasTped)
                    {
                        Utility.MainTransform().position = NewPointer.transform.position;
                        hasTped = true;
                    }
                }
                else
                {
                    hasTped = false;
                }
            }
            else
            {
                JupiterX.Menu.Main.DestroyGun();
            }
        }
    }
}
