using JupiterX.Managers;
using Photon.Pun;
using UnityEngine;
using static JupiterX.Menu.Main;

namespace JupiterX.Mods
{
    public class Projectiles
    {
        private static int[] types = new int[] // Snowball, Slingshot, Cloud, Cupid, Ice, Deadshot, Elf
        {
            -675036877, -820530352, 1511318966, 825718363, -1671677000, 693334698, 1705139863
        };

        public static void SnowballSpam()
        {
            if (Utility.RightGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.RightHandTransform().position, Vector3.zero, types[0], -1, true, 1 });
            if (Utility.LeftGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.LeftHandTransform().position, Vector3.zero, types[0], -1, true, 1 });
        }
        public static void SlingshotSpam()
        {
            if (Utility.RightGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.RightHandTransform().position, Vector3.zero, types[1], -1, true, 1 });
            if (Utility.LeftGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.LeftHandTransform().position, Vector3.zero, types[1], -1, true, 1 });
        }
        public static void CloudSpam()
        {
            if (Utility.RightGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.RightHandTransform().position, Vector3.zero, types[2], -1, true, 1 });
            if (Utility.LeftGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.LeftHandTransform().position, Vector3.zero, types[2], -1, true, 1 });
        }
        public static void CupidSpam()
        {
            if (Utility.RightGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.RightHandTransform().position, Vector3.zero, types[3], -1, true, 1 });
            if (Utility.LeftGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.LeftHandTransform().position, Vector3.zero, types[3], -1, true, 1 });
        }
        public static void IceSpam()
        {
            if (Utility.RightGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.RightHandTransform().position, Vector3.zero, types[4], -1, true, 1 });
            if (Utility.LeftGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.LeftHandTransform().position, Vector3.zero, types[4], -1, true, 1 });
        }
        public static void DeadshotSpam()
        {
            if (Utility.RightGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.RightHandTransform().position, Vector3.zero, types[5], -1, true, 1 });
            if (Utility.LeftGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.LeftHandTransform().position, Vector3.zero, types[5], -1, true, 1 });
        }
        public static void ElfSpam()
        {
            if (Utility.RightGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.RightHandTransform().position, Vector3.zero, types[6], -1, true, 1 });
            if (Utility.LeftGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.LeftHandTransform().position, Vector3.zero, types[6], -1, true, 1 });
        }

        public static void SnowballLauncher()
        {
            if (Utility.RightGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.RightHandTransform().position, Quaternion.AngleAxis(-45f, Utility.RightHandTransform().right) * Utility.RightHandTransform().forward * 15f, types[0], -1, true, 1 });
            if (Utility.LeftGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.LeftHandTransform().position, Quaternion.AngleAxis(-45f, Utility.LeftHandTransform().right) * Utility.LeftHandTransform().forward * 15f, types[0], -1, true, 1 });
        }
        public static void SlingshotLauncher()
        {
            if (Utility.RightGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.RightHandTransform().position, Quaternion.AngleAxis(-45f, Utility.RightHandTransform().right) * Utility.RightHandTransform().forward * 15f, types[1], -1, true, 1 });
            if (Utility.LeftGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.LeftHandTransform().position, Quaternion.AngleAxis(-45f, Utility.LeftHandTransform().right) * Utility.LeftHandTransform().forward * 15f, types[1], -1, true, 1 });
        }
        public static void CloudLauncher()
        {
            if (Utility.RightGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.RightHandTransform().position, Quaternion.AngleAxis(-45f, Utility.RightHandTransform().right) * Utility.RightHandTransform().forward * 15f, types[2], -1, true, 1 });
            if (Utility.LeftGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.LeftHandTransform().position, Quaternion.AngleAxis(-45f, Utility.LeftHandTransform().right) * Utility.LeftHandTransform().forward * 15f, types[2], -1, true, 1 });
        }
        public static void CupidLauncher()
        {
            if (Utility.RightGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.RightHandTransform().position, Quaternion.AngleAxis(-45f, Utility.RightHandTransform().right) * Utility.RightHandTransform().forward * 15f, types[3], -1, true, 1 });
            if (Utility.LeftGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.LeftHandTransform().position, Quaternion.AngleAxis(-45f, Utility.LeftHandTransform().right) * Utility.LeftHandTransform().forward * 15f, types[3], -1, true, 1 });
        }
        public static void IceLauncher()
        {
            if (Utility.RightGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.RightHandTransform().position, Quaternion.AngleAxis(-45f, Utility.RightHandTransform().right) * Utility.RightHandTransform().forward * 15f, types[4], -1, true, 1 });
            if (Utility.LeftGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.LeftHandTransform().position, Quaternion.AngleAxis(-45f, Utility.LeftHandTransform().right) * Utility.LeftHandTransform().forward * 15f, types[4], -1, true, 1 });
        }
        public static void DeadshotLauncher()
        {
            if (Utility.RightGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.RightHandTransform().position, Quaternion.AngleAxis(-45f, Utility.RightHandTransform().right) * Utility.RightHandTransform().forward * 15f, types[5], -1, true, 1 });
            if (Utility.LeftGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.LeftHandTransform().position, Quaternion.AngleAxis(-45f, Utility.LeftHandTransform().right) * Utility.LeftHandTransform().forward * 15f, types[5], -1, true, 1 });
        }
        public static void ElfLauncher()
        {
            if (Utility.RightGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.RightHandTransform().position, Quaternion.AngleAxis(-45f, Utility.RightHandTransform().right) * Utility.RightHandTransform().forward * 15f, types[6], -1, true, 1 });
            if (Utility.LeftGrip)
                RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Utility.LeftHandTransform().position, Quaternion.AngleAxis(-45f, Utility.LeftHandTransform().right) * Utility.LeftHandTransform().forward * 15f, types[6], -1, true, 1 });
        }

        public static void SnowballGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject Pointer = GunData.Pointer;
                if (GetGunInput(true))
                {
                    RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Pointer.transform.position, Vector3.zero, types[0], -1, true, 1 });
                }
            }
        }

        public static void SlingshotGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject Pointer = GunData.Pointer;
                if (GetGunInput(true))
                {
                    RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Pointer.transform.position, Vector3.zero, types[1], -1, true, 1 });
                }
            }
        }
        public static void CloudGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject Pointer = GunData.Pointer;
                if (GetGunInput(true))
                {
                    RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Pointer.transform.position, Vector3.zero, types[2], -1, true, 1 });
                }
            }
        }
        public static void CupidGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject Pointer = GunData.Pointer;
                if (GetGunInput(true))
                {
                    RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Pointer.transform.position, Vector3.zero, types[3], -1, true, 1 });
                }
            }
        }
        public static void IceGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject Pointer = GunData.Pointer;
                if (GetGunInput(true))
                {
                    RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Pointer.transform.position, Vector3.zero, types[4], -1, true, 1 });
                }
            }
        }
        public static void DeadshotGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject Pointer = GunData.Pointer;
                if (GetGunInput(true))
                {
                    RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Pointer.transform.position, Vector3.zero, types[5], -1, true, 1 });
                }
            }
        }
        public static void ElfGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject Pointer = GunData.Pointer;
                if (GetGunInput(true))
                {
                    RPCManager.GameRPC("LaunchSlingshotProjectile", RpcTarget.All, new object[] { Pointer.transform.position, Vector3.zero, types[6], -1, true, 1 });
                }
            }
        }
    }
}