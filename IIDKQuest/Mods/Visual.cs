using PlayFab;
using PlayFab.ClientModels;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking.Match;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX.Mods
{
    internal class Visual
    {
        static string taggedText;
        static bool isTagged = false;
        static int Tagged = 0;
        public static void LeftTaggedLabel() // creds to iiDk
        {
            GameObject textHolder = new GameObject("TaggedLabel");

            textHolder.transform.position = Utility.LeftHandTransform().position + new Vector3(0, 0.5f, 0);
            textHolder.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

            TextMesh label = textHolder.AddComponent<TextMesh>();
            label.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            label.fontSize = 24;
            label.color = Color.white;
            label.characterSize = 0.1f;
            label.anchor = TextAnchor.MiddleCenter;
            label.alignment = TextAlignment.Center;
            label.fontStyle = FontStyle.Italic;

            VRRig[] rigs = GorillaParent.instance.vrrigs.ToArray();

            for (int i = 0; i < rigs.Length; i++)
            {
                if (rigs[i] != null && !rigs[i].photonView.IsMine && !rigs[i].isMyPlayer)
                {
                    if (rigs[i].mainSkin.material.name.Contains("fected") || rigs[i].mainSkin.material.name.ToLower().Contains("It"))
                    {
                        isTagged = true;
                        Tagged += 1;
                    }
                    else
                    {
                        isTagged = false;
                    }
                    if (isTagged)
                    {
                        taggedText = $"{Tagged} left";
                    }
                    else
                    {
                        taggedText = $"<color=lime>0</color> left";
                    }
                }
            }
            label.text = taggedText;
            textHolder.transform.position = GorillaTagger.Instance.leftHandTransform.position + new Vector3(0f, 0.1f, 0f);
            textHolder.transform.LookAt(Camera.main.transform.position);
            textHolder.transform.Rotate(0f, 180f, 0f);

            GameObject.Destroy(textHolder, Time.deltaTime);
        }

        public static void VelocityLabel() // creds to iiDk
        {
            GameObject textHolder = new GameObject("VelocityLabel");

            textHolder.transform.position = Utility.RightHandTransform().position + new Vector3(0, 0.5f, 0);
            textHolder.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

            TextMesh label = textHolder.AddComponent<TextMesh>();
            label.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            label.fontSize = 24;
            label.color = Color.white;
            label.characterSize = 0.1f;
            label.anchor = TextAnchor.MiddleCenter;
            label.alignment = TextAlignment.Center;
            label.fontStyle = FontStyle.Italic;

            label.text = string.Format("{0:F1}m/s", Utility.RigidbodyTransform().velocity.magnitude);
            textHolder.transform.position = GorillaTagger.Instance.rightHandTransform.position + new Vector3(0f, 0.1f, 0f);
            textHolder.transform.LookAt(Camera.main.transform.position);
            textHolder.transform.Rotate(0f, 180f, 0f);

            GameObject.Destroy(textHolder, Time.deltaTime);
        }


        public static void BoxESP()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                {
                    bool isTagged = rig.mainSkin.material.name.Contains("fected");
                    GameObject box = new GameObject("box");
                    box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    box.transform.position = rig.headConstraint.transform.position;
                    box.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    box.transform.rotation = rig.transform.rotation;
                    box.GetComponent<Renderer>().material.shader = Utility.GUIShader();
                    box.GetComponent<Renderer>().material.color = isTagged ? Color.red : Color.grey;
                    GameObject.Destroy(box, Time.deltaTime);
                }
            }
        }
        public static void SphereESP()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                {
                    bool isTagged = rig.mainSkin.material.name.Contains("fected");
                    GameObject sphere = new GameObject("sphere");
                    sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.position = rig.headConstraint.transform.position;
                    sphere.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    sphere.transform.rotation = rig.transform.rotation;
                    sphere.GetComponent<Renderer>().material.shader = Utility.GUIShader();
                    sphere.GetComponent<Renderer>().material.color = isTagged ? Color.red : Color.grey;
                    GameObject.Destroy(sphere, Time.deltaTime);
                }
            }
        }

        public static void NameTagESP() // creds to Saturn
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                {
                    GameObject textHolder = new GameObject("NameTagESP");

                    textHolder.transform.position = rig.headConstraint.transform.position + new Vector3(0, 1.2f, 0);
                    textHolder.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

                    TextMesh label = textHolder.AddComponent<TextMesh>();
                    label.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                    label.fontSize = 24;
                    label.color = Color.white;
                    label.characterSize = 0.1f;
                    label.anchor = TextAnchor.MiddleCenter;
                    label.alignment = TextAlignment.Center;

                    string platform = rig.concatStringOfCosmeticsAllowed.Contains("FIRST LOGIN") ? "QUEST" : "PCVR"; // "PCVR" : "QUEST";

                    label.text = $"Name: {rig.photonView.Owner.NickName}\nUserId: {rig.photonView.Owner.UserId}\nMaster: {rig.photonView.Owner.IsMasterClient}\nActorNumber: {rig.photonView.Owner.ActorNumber}\nPlatform: {platform}";
                    textHolder.transform.position = rig.headConstraint.transform.position + new Vector3(0, 1f, 0);
                    textHolder.transform.LookAt(Camera.main.transform.position);
                    textHolder.transform.Rotate(0f, 180f, 0f);

                    GameObject.Destroy(textHolder, Time.deltaTime);
                }
            }
        }

        public static void Tracers()
        {
            VRRig[] theRigs = GorillaParent.instance.vrrigs.ToArray(); // made tracers do this shit lol
            foreach (VRRig rig in theRigs)
            {
                for (int i = 0; i < theRigs.Length; i++)
                {
                    if (theRigs[i] != null && !theRigs[i].isMyPlayer && !theRigs[i].photonView.IsMine)
                    {
                        theRigs[i] = Utility.GetAllVRRigsWithoutMe(rig);
                        GameObject holder;
                        LineRenderer tracer;
                        bool isTagged = rig.mainSkin.material.name.Contains("fected");
                        Color lineColor = isTagged ? Color.red : Color.grey;
                        (holder, tracer) = Utility.CreateLine(Utility.RightHandTransform(), rig.headMesh.transform, lineColor);
                    }
                }
            }
        }

        public static void fullBright()
        {
            RenderSettings.fog = false; RenderSettings.ambientLight = Color.white;
        }

        public static void fulldrak()
        {
            RenderSettings.fog = true; RenderSettings.ambientLight = Color.black;
        }

        public static void Chams(bool chams)
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                {
                    bool isTagged = rig.mainSkin.material.name.Contains("fected");
                    if (chams)
                    {
                        rig.mainSkin.material.shader = Utility.GUIShader();
                        rig.currentMatIndex = isTagged ? 1 : 0;
                    }
                    else
                    {
                        rig.mainSkin.material.shader = Utility.StandardShader();
                        rig.currentMatIndex = 0;
                    }
                }
            }
        }
    }
}
