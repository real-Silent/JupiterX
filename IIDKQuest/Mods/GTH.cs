using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations.Rigging;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX.Mods
{
    internal class GTH
    {
        public static void FullBright(bool enabled)
        {
            if (enabled)
            {
                RenderSettings.fog = false;
                RenderSettings.ambientLight = Color.white;
            }
            else
            {
                RenderSettings.fog = true;
                RenderSettings.ambientLight = Color.black;
            }
        }
        public static void TimmySpam()
        {
            if (Utility.RTrigger)
                Utility.BetaSpawnPrefab("horror/timmy", Utility.RightHandTransform().position, Utility.RightHandTransform().rotation);
            if (Utility.LTrigger)
                Utility.BetaSpawnPrefab("horror/timmy", Utility.LeftHandTransform().position, Utility.LeftHandTransform().rotation);
        }
        public static void StalkerSpam()
        {
            if (Utility.RTrigger)
                Utility.BetaSpawnPrefab("horror/stalker", Utility.RightHandTransform().position, Utility.RightHandTransform().rotation);
            if (Utility.LTrigger)
                Utility.BetaSpawnPrefab("horror/stalker", Utility.LeftHandTransform().position, Utility.LeftHandTransform().rotation);
        }

        public static void TimmyAll()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                {
                    if (Utility.RTrigger)
                        Utility.BetaSpawnPrefab("horror/timmy", rig.headConstraint.transform.position, rig.headConstraint.transform.rotation);
                }
            }
        }
        public static void StalkerAll()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                {
                    if (Utility.RTrigger)
                        Utility.BetaSpawnPrefab("horror/stalker", rig.headConstraint.transform.position, rig.headConstraint.transform.rotation);
                }
            }
        }

        public static void TimmyGun()
        {
            if (Utility.RGrip)
            {
                RaycastHit raycastHit;
                Physics.Raycast(Utility.RightHandTransform().position -
                Utility.RightHandTransform().up, -
                Utility.RightHandTransform().up +
                Utility.RightHandTransform().forward, out raycastHit);
                {
                    GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                    gameObject.GetComponent<Renderer>().material.color = Color.white;
                    gameObject.transform.position = raycastHit.point;
                    UnityEngine.Object.Destroy(gameObject.GetComponent<BoxCollider>());
                    UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(gameObject.GetComponent<Collider>());
                    UnityEngine.Object.Destroy(gameObject, Time.deltaTime);
                    gameObject.GetComponent<Collider>().enabled = false;

                    if (Utility.RTrigger)
                    {
                        gameObject.GetComponent<Renderer>().material.color = Color.red;
                        Utility.BetaSpawnPrefab("horror/timmy", gameObject.transform.position, gameObject.transform.rotation);
                    }
                }
            }
        }

        public static void MoveTimmy()
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            if (Utility.RGrip)
            {
                RaycastHit raycastHit;
                Physics.Raycast(Utility.RightHandTransform().position -
                Utility.RightHandTransform().up, -
                Utility.RightHandTransform().up +
                Utility.RightHandTransform().forward, out raycastHit);
                {
                    GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                    gameObject.GetComponent<Renderer>().material.color = Color.white;
                    gameObject.transform.position = raycastHit.point;
                    UnityEngine.Object.Destroy(gameObject.GetComponent<BoxCollider>());
                    UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(gameObject.GetComponent<Collider>());
                    UnityEngine.Object.Destroy(gameObject, Time.deltaTime);
                    gameObject.GetComponent<Collider>().enabled = false;

                    if (Utility.RTrigger)
                    {
                        gameObject.GetComponent<Renderer>().material.color = Color.red;
                        Utility.SetMaster(PhotonNetwork.LocalPlayer);
                        // thx saturn for this part
                        foreach (GameObject timmy in GameObject.FindObjectsOfType<GameObject>())
                        {
                            if (timmy.name.ToLower().Contains("timmy"))
                            {
                                timmy.transform.position = gameObject.transform.position;
                            }
                        }
                    }
                }
            }
        }

        public static void TimmyAllRigs()
        {
            if (Utility.RTrigger)
            {
                Utility.SetMaster(PhotonNetwork.LocalPlayer);
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                    {
                        if (Utility.RTrigger)
                        {
                            foreach (GameObject timmy in GameObject.FindObjectsOfType<GameObject>())
                            {
                                if (timmy.name.ToLower().Contains("timmy"))
                                {
                                    if (Utility.RTrigger)
                                    {
                                        timmy.transform.position = rig.headConstraint.transform.position;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void SpazTimmy()
        {
            if (Utility.RTrigger)
            {
                Utility.SetMaster(PhotonNetwork.LocalPlayer);
                foreach (GameObject timmy in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (timmy.name.ToLower().Contains("timmy"))
                    {
                        if (Utility.RTrigger)
                        {
                            timmy.transform.localRotation = Quaternion.Euler(UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100));
                        }
                    }
                }
            }
        }

        public static void FlingTimmy()
        {
            if (Utility.RTrigger)
            {
                Utility.SetMaster(PhotonNetwork.LocalPlayer);
                foreach (GameObject timmy in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (timmy.name.ToLower().Contains("timmy"))
                    {
                        if (Utility.RTrigger)
                        {
                            timmy.transform.position = new Vector3(0, 150f, 0);
                        }
                    }
                }
            }
        }

        public static void KillGun()
        {
            if (Utility.RGrip)
            {
                RaycastHit raycastHit;
                Physics.Raycast(Utility.RightHandTransform().position -
                Utility.RightHandTransform().up, -
                Utility.RightHandTransform().up +
                Utility.RightHandTransform().forward, out raycastHit);
                {
                    GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                    gameObject.GetComponent<Renderer>().material.color = Color.white;
                    gameObject.transform.position = raycastHit.point;
                    UnityEngine.Object.Destroy(gameObject.GetComponent<BoxCollider>());
                    UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(gameObject.GetComponent<Collider>());
                    UnityEngine.Object.Destroy(gameObject, Time.deltaTime);
                    gameObject.GetComponent<Collider>().enabled = false;

                    if (Utility.RTrigger)
                    {
                        gameObject.GetComponent<Renderer>().material.color = Color.red;
                        VRRig rig = raycastHit.collider.GetComponentInParent<VRRig>();
                        if (rig != null)
                        {
                            GameObject stalker = PhotonNetwork.Instantiate("horror/stalker", rig.headConstraint.transform.position, rig.headConstraint.transform.rotation);
                            if (stalker.GetComponent<Renderer>().enabled)
                                stalker.GetComponent<Renderer>().enabled = false;
                        }
                    }
                }
            }
        }

        public static void KillAll()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                {
                    if (Utility.RTrigger)
                    {
                        Utility.BetaSpawnPrefab("horror/stalker", rig.headConstraint.transform.position, rig.headConstraint.transform.rotation);
                        Utility.BetaSpawnPrefab("horror/stalker", rig.transform.position, rig.headConstraint.transform.rotation);
                        Utility.BetaSpawnPrefab("horror/stalker", rig.rightHandTransform.transform.position, rig.headConstraint.transform.rotation);
                    }
                }
            }
        }

        public static void StalkerGun()
        {
            if (Utility.RGrip)
            {
                RaycastHit raycastHit;
                Physics.Raycast(Utility.RightHandTransform().position -
                Utility.RightHandTransform().up, -
                Utility.RightHandTransform().up +
                Utility.RightHandTransform().forward, out raycastHit);
                {
                    GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                    gameObject.GetComponent<Renderer>().material.color = Color.white;
                    gameObject.transform.position = raycastHit.point;
                    UnityEngine.Object.Destroy(gameObject.GetComponent<BoxCollider>());
                    UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(gameObject.GetComponent<Collider>());
                    UnityEngine.Object.Destroy(gameObject, Time.deltaTime);
                    gameObject.GetComponent<Collider>().enabled = false;

                    if (Utility.RTrigger)
                    {
                        gameObject.GetComponent<Renderer>().material.color = Color.red;
                        Utility.BetaSpawnPrefab("horror/stalker", gameObject.transform.position, gameObject.transform.rotation);
                    }
                }
            }
        }
    }
}
