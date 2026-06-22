using Console;
using GorillaNetworking;
using JupiterX.Menu;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace JupiterX.Mods
{
    public class Visual
    {
		public static TrailRenderer trailRenderer;
		public static void DrawGun()
		{
			if (Main.GetGunInput(false))
			{
				var GunData = Main.RenderGun();
				GameObject NewPointer = GunData.Pointer;

				if (trailRenderer == null)
				{
					GameObject trailHolder = new GameObject("JupiterX_DrawGunTrail");

					trailRenderer = trailHolder.AddComponent<TrailRenderer>();
					trailRenderer.startWidth = 0.1f;
					trailRenderer.endWidth = 0.1f;

					trailRenderer.minVertexDistance = 0.05f;

					trailRenderer.material.shader = Utility.GUIShader();
					trailRenderer.time = float.PositiveInfinity;

					trailRenderer.startColor = Color.black;
					trailRenderer.endColor = Color.black;
				}
				trailRenderer.emitting = Main.GetGunInput(true);
				trailRenderer.gameObject.transform.position = NewPointer.transform.position;
			}
		}

		public static void DisableDrawGun()
		{
			if (trailRenderer != null)
				Object.Destroy(trailRenderer.gameObject);

			trailRenderer = null;
		}

		public static readonly List<Renderer> disabledRenderers = new List<Renderer>();
        public static void Xray()
        {
            if (Utility.RightTrigger)
            {
                if (disabledRenderers.Count <= 0)
                {
                    foreach (Renderer renderer in GameObject.FindObjectsOfType<Renderer>().Where(rend => rend != null && rend.gameObject != null && !(rend is SkinnedMeshRenderer) && rend.enabled && rend.gameObject.activeSelf))
                    {
                        renderer.enabled = false;
                        disabledRenderers.Add(renderer);
                    }
                }
            }
            else
            {
                if (disabledRenderers.Count > 0)
                {
                    foreach (Renderer renderer in disabledRenderers.Where(rend => rend != null && rend.gameObject != null))
                        renderer.enabled = true;
                    disabledRenderers.Clear();
                }
            }
        }

        public static void NoSmoothRigs()
        {
            foreach (var vrrig in GorillaParent.instance.vrrigs.ToArray().Where(vrrig => vrrig != GorillaTagger.Instance.myVRRig))
            {
                vrrig.lerpValueBody = 2f;
                vrrig.lerpValueFingers = 1f;
            }
        }

        public static void ReSmoothRigs()
        {
            foreach (var vrrig in GorillaParent.instance.vrrigs.ToArray().Where(vrrig => vrrig != GorillaTagger.Instance.myVRRig))
            {
                vrrig.lerpValueBody = GorillaTagger.Instance.myVRRig.lerpValueBody;
                vrrig.lerpValueFingers = GorillaTagger.Instance.myVRRig.lerpValueFingers;
            }
        }

		public static void BoxESP()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        bool isTagged = rig.mainSkin.material.name.Contains("fected");
                        GameObject box = new GameObject("box");
                        box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        box.transform.position = rig.headConstraint.transform.position;
                        box.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        GameObject.Destroy(box.GetComponent<BoxCollider>());
                        box.transform.rotation = rig.transform.rotation;
                        box.GetComponent<Renderer>().material.shader = Utility.GUIShader();
                        box.GetComponent<Renderer>().material.color = isTagged ? Color.red : Color.grey;
                        GameObject.Destroy(box, Time.deltaTime);
                    }
                }
            }
        }

        public static void CapsuleESP()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        bool isTagged = rig.mainSkin.material.name.Contains("fected");
                        GameObject box = new GameObject("box");
                        box = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                        GameObject.Destroy(box.GetComponent<CapsuleCollider>());
                        box.transform.position = rig.headConstraint.transform.position;
                        box.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        box.transform.rotation = rig.transform.rotation;
                        box.GetComponent<Renderer>().material.shader = Utility.GUIShader();
                        box.GetComponent<Renderer>().material.color = isTagged ? Color.red : Color.grey;
                        GameObject.Destroy(box, Time.deltaTime);
                    }
                }
            }
        }

        public static void SphereESP()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        bool isTagged = rig.mainSkin.material.name.Contains("fected");
                        GameObject sphere = new GameObject("sphere");
                        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        GameObject.Destroy(sphere.GetComponent<SphereCollider>());
                        sphere.transform.position = rig.headConstraint.transform.position;
                        sphere.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        sphere.transform.rotation = rig.transform.rotation;
                        sphere.GetComponent<Renderer>().material.shader = Utility.GUIShader();
                        sphere.GetComponent<Renderer>().material.color = isTagged ? Color.red : Color.grey;
                        GameObject.Destroy(sphere, Time.deltaTime);
                    }
                }
            }
        }

        private static void DrawTag(VRRig rig, string text, Color color, int index)
        {
            GameObject textHolder = new GameObject("Tag");
            TextMesh nametag = textHolder.AddComponent<TextMesh>();
            Font arial = Resources.GetBuiltinResource<Font>("Arial.ttf");
            nametag.font = arial;
            textHolder.GetComponent<MeshRenderer>().material = arial.material;
            nametag.text = text;
            nametag.color = color;
            nametag.fontSize = 38;
            nametag.characterSize = 0.02f;
            nametag.anchor = TextAnchor.MiddleCenter;
            nametag.alignment = TextAlignment.Center;
            textHolder.transform.position = rig.headConstraint.transform.position + new Vector3(0f, 1.25f + (index * -0.15f), 0f);
            textHolder.transform.LookAt(Camera.main.transform);
            textHolder.transform.Rotate(0f, 180f, 0f);
            Object.Destroy(textHolder, Time.deltaTime);
        }

        public static void NameTags()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        DrawTag(rig, CleanPlayerName(rig.photonView.Owner.NickName), rig.playerColor(), 0);
                    }
                }
            }
        }
        public static void IDNameTags()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        DrawTag(rig, rig.photonView.Owner.UserId, rig.playerColor(), 1);
                    }
                }
            }
        }

        public static void PlatformTags()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        DrawTag(rig, rig.GetPlatform(), rig.playerColor(), 2);
                    }
                }
            }
        }

        public static void MasterTags()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        DrawTag(rig, rig.photonView.Owner.IsMasterClient ? "Master" : "Not Master", rig.playerColor(), 3);
                    }
                }
            }
        }

        public static void TaggedTags()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        DrawTag(rig, rig.IsTagged() ? "Tagged" : "", rig.playerColor(), 4);
                    }
                }
            }
        }

        public static string NoRichtextTags(string input, string replace = "")
        {
            input ??= "";
            return Regex.Replace(input, "<.*?>", replace, RegexOptions.IgnoreCase);
        }

        public static string CleanPlayerName(string input, int length = 12)
        {
            input = NoRichtextTags(input);
            if (input.Length > length)
                input = input[..length];
            return input;
        }

        static GameObject holder;
        static LineRenderer tracer;
        public static void Tracers()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
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
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        bool isTagged = rig.mainSkin.material.name.Contains("fected");
                        if (chams)
                        {
                            rig.mainSkin.material.shader = Utility.GUIShader();
                            rig.currentMatIndex = isTagged ? 1 : 0;
                        }
                        else
                        {
                            rig.ChangeMaterialLocal(rig.currentMatIndex);
                        }
                    }
                }
            }
        }

        private static void DrawLabel(Transform target, string labelObjName, string text, Color color, int index = 0)
        {
            GameObject textHolder = new GameObject("Label_" + labelObjName);
            TextMesh label = textHolder.AddComponent<TextMesh>();
            label.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            label.fontSize = 22;
            label.characterSize = 0.1f;
            label.anchor = TextAnchor.MiddleCenter;
            label.alignment = TextAlignment.Center;
            label.fontStyle = FontStyle.Italic;
            label.color = color;
            label.text = text;
            textHolder.transform.position = target.position + new Vector3(0f, 0.1f + (index * 0.15f), 0f);
            textHolder.transform.localScale = Vector3.one * 0.25f;
            textHolder.transform.LookAt(Camera.main.transform);
            textHolder.transform.Rotate(0f, 180f, 0f);
            Object.Destroy(textHolder, Time.deltaTime);
        }

        public static void VelocityLabel()
        {
            Rigidbody rb = GorillaTagger.Instance.bodyCollider.attachedRigidbody;
            DrawLabel(GorillaTagger.Instance.rightHandTransform, "Velocity", $"{rb.velocity.magnitude:F1}m/s", rb.velocity.magnitude >= GorillaLocomotion.Player.Instance.maxJumpSpeed ? Color.green : Color.white);
        }

        private static string FormatTimer(int seconds)
        {
            int minutes = seconds / 60;
            int remainingSeconds = seconds % 60;
            return $"{minutes:D2}:{remainingSeconds:D2}";
        }

        private static float startTime;
        private static float endTime;
        private static bool lastWasTagged;
        public static void TimeLabel()
        {
            if (!PhotonNetwork.InRoom)
                return;

            if (InfectedList().Count == 0)
            {
                startTime = Time.time;
                return;
            }

            bool playerIsTagged = GorillaTagger.Instance.myVRRig.IsTagged();
            switch (playerIsTagged)
            {
                case true when !lastWasTagged:
                    endTime = Time.time - startTime;
                    break;
                case false when lastWasTagged:
                    startTime = Time.time;
                    break;
            }
            lastWasTagged = playerIsTagged;
            DrawLabel(GorillaTagger.Instance.rightHandTransform, "Time", FormatTimer(Mathf.FloorToInt(playerIsTagged ? endTime : Time.time - startTime)), playerIsTagged ? Color.green : Color.white);
        }

        public static void NearbyTaggerLabel()
        {
            if (GorillaTagger.Instance == null || GorillaParent.instance == null)
                return;
            if (GorillaTagger.Instance.myVRRig.IsTagged())
                return;

            float closest = float.MaxValue;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == null || vrrig.headMesh == null || !vrrig.IsTagged())
                    continue;
                float dist = Vector3.Distance(GorillaTagger.Instance.headCollider.transform.position, vrrig.headMesh.transform.position);
                if (dist < closest)
                    closest = dist;
            }
            if (closest == float.MaxValue)
                return;

            Color colorn = Color.green;
            if (closest < 30f) colorn = Color.yellow;
            if (closest < 20f) colorn = new Color32(255, 90, 0, 255);
            if (closest < 10f) colorn = Color.red;
            DrawLabel(GorillaTagger.Instance.leftHandTransform, "NearbyTagger", $"{closest:F1}m", colorn);
        }
        public static void LastLabel()
        {
            if (!PhotonNetwork.InRoom)
                return;
            if (InfectedList().Count == 0)
                return;
            int left = PhotonNetwork.PlayerList.Length - InfectedList().Count;
            DrawLabel(GorillaTagger.Instance.leftHandTransform, "LastLabel", left + " left", left <= 1 && !GorillaTagger.Instance.myVRRig.IsTagged() ? Color.green : Color.white);
        }

        public static List<Photon.Realtime.Player> InfectedList()
		{
			List<Photon.Realtime.Player> infected = new List<Photon.Realtime.Player>();
			if (!PhotonNetwork.InRoom || GorillaGameManager.instance == null)
				return infected;
			switch (GorillaComputer.instance.currentGameMode)
			{
				case "INFECTION":
					GorillaTagManager tagManager = (GorillaTagManager)GorillaGameManager.instance;
					if (tagManager.isCurrentlyTag)
						infected.Add(tagManager.currentIt);
					else
						infected.AddRange(tagManager.currentInfected.ToArray());
					break;
				default:
					break;
			}
			return infected;
		}
	}
}
