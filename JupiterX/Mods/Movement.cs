using ExitGames.Client.Photon;
using JupiterX.Extensions;
using JupiterX.Menu;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static JupiterX.Menu.Main;

namespace JupiterX.Mods
{
    public class Movement
    {
        static Vector3 normal2;
        static Vector3 vel1;
        static Vector3 vel2;
        static float dist2;
        static int layers;
        static bool LeftClose2;
        static bool DoOnce2;
        static float maxD2;
        static float ammount;
        public static void WallWalk()
        {
            if (Utility.RightGrip || Utility.LeftGrip)
            {
                if (!DoOnce2)
                {
                    maxD2 = 1f;
                    layers = int.MaxValue;
                    DoOnce2 = true;
                }
                RaycastHit raycastHit;
                Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position, -GorillaTagger.Instance.rightHandTransform.right, out raycastHit, 1f, layers);
                RaycastHit raycastHit2;
                Physics.Raycast(GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.leftHandTransform.right, out raycastHit2, 1f, layers);
                if (raycastHit2.distance > raycastHit.distance)
                {
                    normal2 = raycastHit.normal;
                    dist2 = raycastHit.distance;
                }
                else
                {
                    normal2 = raycastHit2.normal;
                    dist2 = raycastHit2.distance;
                    LeftClose2 = true;
                }
                if (dist2 < maxD2)
                {
                    vel2 = normal2 * (ammount * Time.deltaTime);
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity -= vel2;
                }
                else
                {
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.useGravity = true;
                }
            }
            else
            {
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.useGravity = true;
            }
        }

        public static Vector3? longJumpPower;
        public static float? keepVelocityUntil;
        public static Vector3? velocity;

        public static float playspaceAbusePower = 0.004f;
        public static void PlayspaceAbuse()
        {
            if (Utility.RightPrimary)
            {
                keepVelocityUntil ??= Time.time + 0.5f;
                velocity ??= GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity;

                if (Time.time < keepVelocityUntil)
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity = velocity.Value;

                longJumpPower ??= (GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity * playspaceAbusePower).X_Z();
                GorillaTagger.Instance.transform.position += longJumpPower.Value;
            }
            else
            {
                longJumpPower = null;
                velocity = null;
            }
        }

        public static string[] FlySpeeds = { "Very Slow", "Slow", "Normal", "Fast", "Very Fast", "Way Too Fast" };
        public static int FlySpeedAmount = 0;
        public static float FlySpeed = 1f;

        public static void ChangeFlySpeed(bool increment = true)
        {
            if (increment)
            {
                FlySpeedAmount = (FlySpeedAmount + 1) % FlySpeeds.Length;
            }
            else
            {
                FlySpeedAmount = (FlySpeedAmount - 1 + FlySpeeds.Length) % FlySpeeds.Length;
            }

            switch (FlySpeedAmount)
            {
                case 0: FlySpeed = 1f; break;
                case 1: FlySpeed = 3f; break;
                case 2: FlySpeed = 7f; break;
                case 3: FlySpeed = 14f; break;
                case 4: FlySpeed = 18f; break;
                case 5: FlySpeed = 30f; break;
            }
            Buttons.GetIndex("Change Fly Speed").overlapText =
                $"Change Fly Speed <color=grey>[<color=cyan>{FlySpeeds[FlySpeedAmount]}</color>]</color>";
        }

        public static void LowGravity() =>
            GorillaTagger.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * 6.66f, ForceMode.Acceleration);

        public static void ZeroGravity() =>
            GorillaTagger.Instance.bodyCollider.attachedRigidbody.AddForce(-Physics.gravity, ForceMode.Acceleration);

        public static void HighGravity() =>
            GorillaTagger.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.down * 7.77f, ForceMode.Acceleration);

        public static void ReverseGravity()
        {
            GorillaTagger.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * 19.62f, ForceMode.Acceleration);
            GorillaTagger.Instance.rightHandTransform.parent.rotation = Quaternion.Euler(180f, 0f, 0f);
        }

        public static void UnflipCharacter() =>
            GorillaTagger.Instance.rightHandTransform.parent.rotation = Quaternion.identity;

        public static void Fly()
        {
            if (Utility.RightPrimary)
            {
                Utility.RigidbodyTransform().transform.position += Utility.Head().transform.forward * Time.deltaTime * FlySpeed;
                Utility.RigidbodyTransform().velocity = Vector3.zero;
            }
        }

        public static void BarkFly()
        {
            Vector3 inputDirection = new Vector3(Utility.LeftJoystickAxis.x, Utility.RightJoystickAxis.y, Utility.LeftJoystickAxis.y);

            Vector3 playerForward = GorillaTagger.Instance.bodyCollider.transform.forward.X_Z();
            Vector3 playerRight = GorillaTagger.Instance.bodyCollider.transform.right.X_Z();

            Vector3 velocity = inputDirection.x * playerRight + inputDirection.y * Vector3.up + inputDirection.z * playerForward;
            velocity *= FlySpeed;
            GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.Lerp(GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity, velocity, 0.12875f);

            ZeroGravity();
        }
        public static void NoClipFly()
        {
            if (Utility.RightPrimary)
            {
                Utility.RigidbodyTransform().transform.position += Utility.Head().transform.forward * Time.deltaTime * FlySpeed;
                Utility.RigidbodyTransform().velocity = Vector3.zero;
            }
            NoClip(Utility.RightPrimary);
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

        private static GameObject point = null;
        public static void Checkpoint()
        {
            if (Utility.RightGrip)
            {
                if (point == null)
                {
                    point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    point.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    point.transform.position = Utility.RightHandTransform().position;
                    point.transform.rotation = Utility.RightHandTransform().rotation;
                    point.GetComponent<Renderer>().material.color = Color.white;
                    GameObject.Destroy(point.GetComponent<Collider>());
                }
                point.transform.position = Utility.RightHandTransform().position;
                point.transform.rotation = Utility.RightHandTransform().rotation;
            }
            if (Utility.RightPrimary)
            {
                if (point != null)
                {
                    point.GetComponent<Renderer>().material.color = Color.red;
                    GorillaTagger.Instance.transform.position = point.transform.position;
                    GameObject.Destroy(point);
                    point = null;
                }
            }
        }

        private static GameObject c4 = null;
        public static void C4()
        {
            if (Utility.RightGrip)
            {
                if (c4 == null)
                {
                    c4 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    c4.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    c4.transform.position = Utility.RightHandTransform().position;
                    c4.transform.rotation = Utility.RightHandTransform().rotation;
                    c4.GetComponent<Renderer>().material.color = Color.white;
                    GameObject.Destroy(c4.GetComponent<Collider>());
                }
                c4.transform.position = Utility.RightHandTransform().position;
                c4.transform.rotation = Utility.RightHandTransform().rotation;
            }
            if (Utility.RightPrimary)
            {
                if (c4 != null)
                {
                    c4.GetComponent<Renderer>().material.color = Color.red;
                    Vector3 dir = GorillaTagger.Instance.bodyCollider.transform.position - c4.transform.position;
                    dir.Normalize();
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity += 25f * dir;
                    GameObject.Destroy(c4);
                    c4 = null;
                }
            }
        }

        public static void FollowPlayerGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;


                if (Main.gunLocked && Main.lockTarget != null)
                {
                    Utility.ActualRig().enabled = false;
                    Utility.ActualRig().transform.position = Main.lockTarget.transform.position;
                    Utility.ActualRig().rightHandTransform.position = Main.lockTarget.rightHandTransform.position;
                    Utility.ActualRig().rightHandTransform.rotation = Main.lockTarget.rightHandTransform.rotation;
                    Utility.ActualRig().leftHandTransform.position = Main.lockTarget.leftHandTransform.position;
                    Utility.ActualRig().leftHandTransform.rotation = Main.lockTarget.leftHandTransform.rotation;
                    Utility.ActualRig().headConstraint.transform.position = Main.lockTarget.headConstraint.transform.position;
                    Utility.ActualRig().headConstraint.transform.rotation = Main.lockTarget.headConstraint.transform.rotation;
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
                Main.lockTarget = null;
                if (Main.gunLocked)
                    Main.gunLocked = false;

                Utility.ActualRig().enabled = true;
                Utility.GhostView(false);
            }
        }

        private static List<Collider> colliders = new List<Collider>();
        private static bool collidersCached = false;
        private static bool lastState = false;

        public static void NoClip(bool enabled)
        {
            if (!collidersCached)
            {
                colliders.AddRange(Object.FindObjectsOfType<Collider>());
                collidersCached = true;
            }
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
            if (Utility.RightTrigger)
                Utility.RigidbodyTransform().velocity += Utility.Head().forward / 2f;
            if (Utility.LeftTrigger)
                Utility.RigidbodyTransform().velocity += -Utility.Head().forward / 2f;
        }

        public static void SlingShotFly()
        {
            if (Utility.RightPrimary)
            {
                Utility.RigidbodyTransform().velocity += Utility.Head().transform.forward / 2f;
            }
        }

        public static void TriggerFly()
        {
            if (Utility.RightTrigger)
            {
                Utility.RigidbodyTransform().transform.position += Utility.Head().transform.forward * Time.deltaTime * FlySpeed;
                Utility.RigidbodyTransform().velocity = Vector3.zero;
            }
        }

        public static void ExcelFly()
        {
            if (Utility.RightPrimary) Utility.RigidbodyTransform().velocity += Utility.RightHandTransform().right / 2f;
            if (Utility.LeftPrimary) Utility.RigidbodyTransform().velocity += -Utility.LeftHandTransform().right / 2f;
        }

        public static void FlyTowardsGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                RaycastHit Ray = GunData.Ray;

                if (gunLocked && lockTarget != null)
                {
                    GorillaTagger.Instance.transform.position += (lockTarget.transform.position - GorillaTagger.Instance.bodyCollider.transform.position) * (Time.deltaTime * FlySpeed);
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
                }

                if (GetGunInput(true))
                {
                    VRRig gunTarget = Ray.collider.GetComponentInParent<VRRig>();
                    if (gunTarget && !gunTarget.IsLocal())
                    {
                        gunLocked = true;
                        lockTarget = gunTarget;
                    }
                }
            }
            else
            {
                if (gunLocked)
                    gunLocked = false;
            }
        }


        public static Vector2 driveLerpDirection = Vector2.zero;
        public static void Drive()
        {
            Vector2 joy = Utility.LeftJoystickAxis;
            driveLerpDirection = Vector2.Lerp(driveLerpDirection, joy, 0.05f);

            Vector3 addition = GorillaTagger.Instance.bodyCollider.transform.forward * driveLerpDirection.y + GorillaTagger.Instance.bodyCollider.transform.right * driveLerpDirection.x;
            Physics.Raycast(GorillaTagger.Instance.bodyCollider.transform.position - new Vector3(0f, 0.2f, 0f), Vector3.down, out var Ray, 512f, 131585);
            Vector3 targetVelocity = addition * 10f;

            if (Ray.distance < 0.2f && (Mathf.Abs(driveLerpDirection.x) > 0.05f || Mathf.Abs(driveLerpDirection.y) > 0.05f))
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity = new Vector3(targetVelocity.x, GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity.y, targetVelocity.z);
        }

        public static void HardDrive()
        {
            if ((Mathf.Abs(Utility.LeftJoystickAxis.x) > 0.05f || Mathf.Abs(Utility.LeftJoystickAxis.y) > 0.05f))
            {
                Vector3 direction = GorillaTagger.Instance.bodyCollider.transform.forward * Utility.LeftJoystickAxis.y
                                  + GorillaTagger.Instance.bodyCollider.transform.right * Utility.LeftJoystickAxis.x;

                Vector3 raycastPosition = GorillaTagger.Instance.bodyCollider.transform.position
                    + Vector3.up * 5f
                    + direction * (Time.deltaTime * 10f);
                Physics.Raycast(raycastPosition, Vector3.down, out var Ray, 50f, 131585);

                Vector3 targetPosition = Ray.point == Vector3.zero ? raycastPosition : Ray.point;

                GorillaTagger.Instance.transform.position = targetPosition + Vector3.up * 0.2f;
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
            }
        }


        private static bool previousDash;
        public static void Dash()
        {
            if (Utility.RightPrimary && !previousDash)
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity += GorillaLocomotion.Player.Instance.headCollider.transform.forward * FlySpeed;

            previousDash = Utility.RightPrimary;
        }

        private static readonly float revCooldown = 0.5f;
        private static float nextrevTime = 0f;
        public static void ReverseVelocity()
        {
            if (Time.time < nextrevTime)
                return;

            if (Utility.RightPrimary)
            {
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity = -GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity;

                nextrevTime = Time.time + revCooldown;
            }
        }

        private static float flapTime;
        public static void BirdFly()
        {
            if (Vector3.Distance(GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.headCollider.transform.position) < 0.63f || Vector3.Distance(GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.headCollider.transform.position) < 0.63f)
                return;

            if (Vector3.Distance(GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.rightHandTransform.position) < 1f)
                return;
            if (Physics.Raycast(GorillaTagger.Instance.bodyCollider.attachedRigidbody.position, Vector3.down, hitInfo: out _))
                return;

            UnityEngine.XR.InputDevice LeftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            UnityEngine.XR.InputDevice RightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

            if (LeftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 leftVel) && RightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 rightVel))
            {
                if (Time.time - flapTime < 0.4f) return;

                if (leftVel.y < -1.2f && rightVel.y < -1.2f)
                {
                    float force = Mathf.Min(6f * ((Mathf.Abs(leftVel.y) + Mathf.Abs(rightVel.y)) / 2f) / 1.2f, 9f);
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * force, ForceMode.VelocityChange);

                    flapTime = Time.time;
                }
            }
        }

        public static string[] ArmSizes = { "Steam", "Long", "Very Long", "Ghost", "Short" };
        public static int ArmSizeAmount = 0;
        public static Vector3 ArmSize = new Vector3(1.15f, 1.15f, 1.15f);
        public static void ChangeArmLength(bool increment = true)
        {
            if (increment)
            {
                ArmSizeAmount = (ArmSizeAmount + 1) % ArmSizes.Length;
            }
            else
            {
                ArmSizeAmount = (ArmSizeAmount - 1 + ArmSizes.Length) % ArmSizes.Length;
            }

            switch (ArmSizeAmount)
            {
                case 0: ArmSize = new Vector3(1.15f, 1.15f, 1.15f); break;
                case 1: ArmSize = new Vector3(1.25f, 1.25f, 1.25f); break;
                case 2: ArmSize = new Vector3(1.5f, 1.5f, 1.5f); break;
                case 3: ArmSize = new Vector3(1.3f, 1.3f, 1.3f); break;
                case 4: ArmSize = new Vector3(0.8f, 0.8f, 0.8f); break;
            }

            Buttons.GetIndex("Change Arm Length").overlapText = $"Change Arm Length <color=grey>[<color=cyan>{ArmSizes[ArmSizeAmount]}</color>]</color>";
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

        public static void UpAndDown()
        {
            if (Utility.RightTriggerFloat > 0.5f || Utility.RightGrip)
                ZeroGravity();

            if (Utility.RightTriggerFloat > 0.5f)
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity += Vector3.up * (Time.deltaTime * FlySpeed * 3f);

            if (Utility.RightGrip)
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity += Vector3.up * (Time.deltaTime * FlySpeed * -3f);
        }

        public static void LeftAndRight()
        {
            if (Utility.RightTriggerFloat > 0.5f || Utility.RightGrip)
                ZeroGravity();

            if (Utility.RightTriggerFloat > 0.5f)
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity += GorillaTagger.Instance.bodyCollider.transform.right * (Time.deltaTime * FlySpeed * -3f);

            if (Utility.RightGrip)
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity += GorillaTagger.Instance.bodyCollider.transform.right * (Time.deltaTime * FlySpeed * 3f);
        }

        public static void ForwardsAndBackwards()
        {
            if (Utility.RightTriggerFloat > 0.5f || Utility.RightGrip)
                ZeroGravity();

            if (Utility.RightTriggerFloat > 0.5f)
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity += GorillaTagger.Instance.bodyCollider.transform.forward * (Time.deltaTime * FlySpeed * 3f);

            if (Utility.RightGrip)
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity += GorillaTagger.Instance.bodyCollider.transform.forward * (Time.deltaTime * FlySpeed * -3f);
        }

        public static void Platforms()
        {
            Utility.CreatePlatform(false, Utility.RightHandTransform(), Utility.LeftHandTransform(), Utility.RightHandTransform().rotation, Utility.LeftHandTransform().rotation, new Vector3(0.0125f, 0.28f, 0.3825f), Color.grey);
        }

        public static void TriggerPlatforms()
        {
            Utility.CreatePlatform(true, Utility.RightHandTransform(), Utility.LeftHandTransform(), Utility.RightHandTransform().rotation, Utility.LeftHandTransform().rotation, new Vector3(0.0125f, 0.28f, 0.3825f), Color.grey);
        }

        public static void InvisablePlatforms()
        {
            Utility.CreatePlatform(false, Utility.RightHandTransform(), Utility.LeftHandTransform(), Utility.RightHandTransform().rotation, Utility.LeftHandTransform().rotation, new Vector3(0.0125f, 0.28f, 0.3825f), Color.grey, true);
        }

        private static readonly Dictionary<bool, List<GameObject>> frozonicPlatforms = new Dictionary<bool, List<GameObject>>();
        private static readonly Dictionary<bool, int> platformIndex = new Dictionary<bool, int>();
        public static void HandleFrozone(bool left)
        {
            bool grip = left ? Utility.LeftGrip : Utility.RightGrip;

            frozonicPlatforms.TryGetValue(left, out List<GameObject> frozonicPlatformList);
            if (frozonicPlatformList == null)
            {
                frozonicPlatformList = new List<GameObject>();
                frozonicPlatforms.Add(left, frozonicPlatformList);
            }

            platformIndex.TryGetValue(left, out int index);

            if (grip)
            {
                GameObject platform;
                if (frozonicPlatformList.Count >= 72)
                    platform = frozonicPlatformList[index];
                else
                {
                    platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    platform.GetComponent<Renderer>().material.color = Color.grey;
                    platform.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f) * 1f;
                    platform.AddComponent<GorillaSurfaceOverride>().overrideIndex = 61;
                    frozonicPlatformList.Add(platform);
                }

                Transform hand = left ? Utility.LeftHandTransform() : Utility.RightHandTransform();

                platform.transform.position = hand.position + (hand.right * ((left ? 1f : -1f) * ((0.025f + platform.transform.localScale.x / 2f) * 1f)));
                platform.transform.rotation = hand.rotation;

                index = (index + 1) % 72;
            }

            platformIndex[left] = index;

            if (!grip && frozonicPlatformList.Count > 0)
            {
                int platformIndex = frozonicPlatformList.Count - 1;

                Object.Destroy(frozonicPlatformList[platformIndex]);
                frozonicPlatformList.RemoveAt(platformIndex);
            }
        }

        public static void PlatformSpam()
        {
            if (Utility.RightGrip)
            {
                GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Object.Destroy(platform.GetComponent<BoxCollider>());
                platform.GetComponent<Renderer>().material.color = Settings.backgroundColor.GetCurrentColor();
                platform.GetComponent<Renderer>().material.shader = Utility.StandardShader();
                platform.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                platform.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                platform.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                Object.Destroy(platform, 1f);
                //PhotonNetwork.RaiseEvent(69, new object[] { platform.transform.position, platform.transform.rotation }, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
            }
        }

        public static void PlatformGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject NewPointer = GunData.Pointer;

                if (GetGunInput(true))
                {
                    GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Object.Destroy(platform.GetComponent<BoxCollider>());
                    platform.GetComponent<Renderer>().material.color = Settings.backgroundColor.GetCurrentColor();
                    platform.GetComponent<Renderer>().material.shader = Utility.StandardShader();
                    platform.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    platform.transform.position = NewPointer.transform.position;
                    platform.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                    Object.Destroy(platform, 1f);
                    //PhotonNetwork.RaiseEvent(69, new object[] { platform.transform.position, platform.transform.rotation }, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
                }
            }
        }

        public static void Frozone()
        {
            HandleFrozone(true);
            HandleFrozone(false);

            GorillaTagger.Instance.bodyCollider.enabled = !(Utility.LeftGrip || Utility.RightGrip);
        }

        static bool hasTped = false;
        public static void TPGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject NewPointer = GunData.Pointer;

                if (GetGunInput(true))
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
        }

        public static float laggyRigDelay;
        public static void LaggyRig()
        {
            Utility.ActualRig().enabled = false;
            if (Time.time > laggyRigDelay)
            {
                Utility.ActualRig().enabled = true;
                Utility.ActualRig().LateUpdate();
                Utility.ActualRig().enabled = false;

                laggyRigDelay = Time.time + 0.5f;
            }
        }

        public static bool wasRightPrimaryPressed;
        public static void UpdateRig()
        {
            Utility.ActualRig().enabled = false;
            if (Utility.RightPrimary && !wasRightPrimaryPressed)
            {
                Utility.ActualRig().enabled = true;
                Utility.ActualRig().LateUpdate();
                Utility.ActualRig().enabled = false;
            }

            wasRightPrimaryPressed = Utility.RightPrimary;
        }
    }
}