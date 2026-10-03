// ============================================================
//  JupiterX | Mods/Prefabs.cs
//  Copyright (c) 2026 Jupiterx (@NAuth). All rights reserved.
//
//  This software and its source code are the property of the
//  author. Unauthorized copying, redistribution, modification,
//  or reuse of any part of this project, in whole or in part,
//  without express written permission is strictly prohibited.
//
//  Version: 2.0.0 | By Silent/Ashley/Nova (@s1lnt)
// ============================================================

using JupiterX.Menu;
using Photon.Pun;
using UnityEngine;

namespace JupiterX.Mods
{
    public class Prefabs
    {
        public static void NetworkPlayerSpam()
        {
            PlayerPrefs.SetString("username", $"<color=#ff00ff>JupiterX V{Utility.version} By Nova</color> \n https://novax.lol/d");

            if (PhotonNetwork.InRoom)
            {
                if (Utility.RightGrip)
                    Utility.BetaSpawnPrefab("Network Player", Utility.RightHandTransform().position, Utility.RightHandTransform().rotation);
                if (Utility.LeftGrip)
                    Utility.BetaSpawnPrefab("Network Player", Utility.LeftHandTransform().position, Utility.LeftHandTransform().rotation);
            }
        }
        public static void EnemySpam()
        {
            if (PhotonNetwork.InRoom)
            {
                if (Utility.RightGrip)
                    Utility.BetaSpawnPrefab("gorillaprefabs/gorillaenemy", Utility.RightHandTransform().position, Utility.RightHandTransform().rotation);
                if (Utility.LeftGrip)
                    Utility.BetaSpawnPrefab("gorillaprefabs/gorillaenemy", Utility.LeftHandTransform().position, Utility.LeftHandTransform().rotation);
            }
        }

        public static void ClearPrefabs()
        {
            if (PhotonNetwork.InRoom)
            {
                Utility.SetMaster(Photon.Pun.PhotonNetwork.LocalPlayer);
                Photon.Pun.PhotonNetwork.DestroyAll();
            }
        }
        public static void TargetSpam()
        {
            if (PhotonNetwork.InRoom)
            {
                if (Utility.RightGrip)
                    Utility.BetaSpawnPrefab("STICKABLE TARGET", Utility.RightHandTransform().position, Utility.RightHandTransform().rotation);
                if (Utility.LeftGrip)
                    Utility.BetaSpawnPrefab("STICKABLE TARGET", Utility.LeftHandTransform().position, Utility.LeftHandTransform().rotation);
            }
        }

        public static void GiveSpamGun(int type)
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.gunLocked && Main.lockTarget != null)
                {
                    if (Main.lockTarget.rightMiddle.calcT > 0.5f) 
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
                Main.lockTarget = null;
                if (Main.gunLocked)
                    Main.gunLocked = false;
            }
        }

        public static void CubeSpam()
        {
            if (PhotonNetwork.InRoom)
            {
                if (Utility.RightGrip)
                    Utility.BetaSpawnPrefab("bulletPrefab", Utility.RightHandTransform().position, Utility.RightHandTransform().rotation);
                if (Utility.LeftGrip)
                    Utility.BetaSpawnPrefab("bulletPrefab", Utility.LeftHandTransform().position, Utility.LeftHandTransform().rotation);
            }
        }
        public static void CubeGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    if (PhotonNetwork.InRoom)
                    {
                        Utility.BetaSpawnPrefab("bulletPrefab", NewPointer.transform.position, NewPointer.transform.rotation);
                    }
                }
            }
        }

        public static void TargetGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    if (PhotonNetwork.InRoom)
                    {
                        Utility.BetaSpawnPrefab("STICKABLE TARGET", NewPointer.transform.position, NewPointer.transform.rotation);
                    }
                }
            }
        }

        public static void ScoreboardGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    if (PhotonNetwork.InRoom)
                    {
                        Utility.BetaSpawnPrefab("gorillaprefabs/gorillascoreboard", NewPointer.transform.position, NewPointer.transform.rotation);
                    }
                }
            }
        }
        public static void SpamScoreboard()
        {
            if (PhotonNetwork.InRoom)
            {
                if (Utility.RightGrip)
                    Utility.BetaSpawnPrefab("gorillaprefabs/gorillascoreboard", Utility.RightHandTransform().position, Utility.RightHandTransform().rotation);
                if (Utility.LeftGrip)
                    Utility.BetaSpawnPrefab("gorillaprefabs/gorillascoreboard", Utility.LeftHandTransform().position, Utility.LeftHandTransform().rotation);
            }
        }

        public static void NetworkPlayerGun() 
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    PlayerPrefs.SetString("username", $"<color=#ff00ff>JupiterX V{Utility.version} By Nova</color> \n https://novax.lol/d");
                    if (PhotonNetwork.InRoom)
                    {
                        Utility.BetaSpawnPrefab("Network Player", NewPointer.transform.position, NewPointer.transform.rotation);
                    }
                }
            }
        }

        public static void EnemyGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    if (PhotonNetwork.InRoom)
                    {
                        Utility.BetaSpawnPrefab("gorillaprefabs/gorillaenemy", NewPointer.transform.position, NewPointer.transform.rotation);
                    }
                }
            }
        }
        public static void PrefabLuancher(string prefab)
        {
            if (PhotonNetwork.InRoom)
            {
                if (Utility.RightGrip)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Vector3 s = Utility.RightHandTransform().position + Utility.RightHandTransform().forward * (i * 3f);
                        Utility.BetaSpawnPrefab(prefab, s, Utility.RightHandTransform().rotation);
                    }
                }
                if (Utility.LeftGrip)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Vector3 j = Utility.LeftHandTransform().position + Utility.LeftHandTransform().forward * (i * 3f);
                        Utility.BetaSpawnPrefab(prefab, j, Utility.LeftHandTransform().rotation);
                    }
                }
            }
        }
    }
}