using Console;
using GorillaNetworking;
using JupiterX.Menu;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

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

					trailRenderer.material.shader = Shader.Find("GUI/Text Shader");
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


		private static readonly Dictionary<VRRig, List<int>> ntDistanceList = new Dictionary<VRRig, List<int>>();
		public static float GetTagDistance(VRRig rig)
		{
			if (ntDistanceList.ContainsKey(rig))
			{
				if (ntDistanceList[rig][0] == Time.frameCount)
				{
					ntDistanceList[rig].Add(Time.frameCount);
					return (0.25f + ntDistanceList[rig].Count * 0.15f) * 1f;
				}
				ntDistanceList[rig].Clear();
				ntDistanceList[rig].Add(Time.frameCount);
				return (0.25f + ntDistanceList[rig].Count * 0.15f) * 1f;
			}
			ntDistanceList.Add(rig, new List<int> { Time.frameCount });
			return 0.4f * 1f;
		}


		private static readonly Dictionary<VRRig, GameObject> nametags = new Dictionary<VRRig, GameObject>();
		public static Vector3 GetNameTagPosition(VRRig rig)
		{
			Transform anchor = rig.headMesh.transform;
			return anchor.position + anchor.up * GetTagDistance(rig);
		}
		public static Transform GetNameTagTransform(VRRig rig)
		{
			return rig.headMesh.transform;
		}

		public static void NameTags()
		{
			List<KeyValuePair<VRRig, GameObject>> nametagsCopy = nametags.ToList();
			foreach (var nametag in nametagsCopy.Where(nametag => !GorillaParent.instance.vrrigs.Contains(nametag.Key)))
			{
				Object.Destroy(nametag.Value);
				nametags.Remove(nametag.Key);
			}
			foreach (var vrrig in GorillaParent.instance.vrrigs)
			{
				if (vrrig != GorillaTagger.Instance.myVRRig)
				{
					if (!nametags.ContainsKey(vrrig))
					{
						GameObject go = new GameObject("JupiterX_Nametag");
						go.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
						TextMeshPro TextMeshPro = go.AddComponent<TextMeshPro>();
						TextMeshPro.fontSize = 4.8f;
						TextMeshPro.alignment = TextAlignmentOptions.Center;
						nametags.Add(vrrig, go);
					}
					GameObject nameTag = nametags[vrrig];
					TextMeshPro tmp = nameTag.GetComponent<TextMeshPro>() ?? nameTag.AddComponent<TextMeshPro>();
					tmp.text = CleanPlayerName(vrrig.photonView.Owner.NickName);
					tmp.color = vrrig.playerColor();
					tmp.fontStyle = FontStyles.Normal;
					nameTag.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f) * 1f;
					nameTag.transform.position = GetNameTagPosition(vrrig);
					nameTag.transform.LookAt(Camera.main.transform.position);
					nameTag.transform.Rotate(0f, 180f, 0f);
				}
			}
		}

		public static string NoRichtextTags(string input, string replace = "")
		{
			Regex notags = new Regex("<.*?>", RegexOptions.IgnoreCase);
			return notags.Replace(input, replace);
		}
		public static string CleanPlayerName(string input, int length = 12)
		{
			input = NoRichtextTags(input);
			if (input.Length > length)
				input = input[..(length - 1)];
			return input;
		}

		public static void DisableNameTags()
		{
			foreach (KeyValuePair<VRRig, GameObject> nametag in nametags)
				Object.Destroy(nametag.Value);
			nametags.Clear();
		}

		private static readonly Dictionary<VRRig, GameObject> idNameTags = new Dictionary<VRRig, GameObject>();
		public static void IDTags()
		{
			List<KeyValuePair<VRRig, GameObject>> nametagsCopy = idNameTags.ToList();
			foreach (var nametag in nametagsCopy.Where(nametag => !GorillaParent.instance.vrrigs.Contains(nametag.Key)))
			{
				Object.Destroy(nametag.Value);
				idNameTags.Remove(nametag.Key);
			}
			foreach (var vrrig in GorillaParent.instance.vrrigs)
			{
				if (vrrig != GorillaTagger.Instance.myVRRig)
				{
					if (!idNameTags.ContainsKey(vrrig))
					{
						GameObject go = new GameObject("JupiterX_IDNameTag");
						go.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
						TextMeshPro TextMeshPro = go.AddComponent<TextMeshPro>();
						TextMeshPro.fontSize = 4.8f;
						TextMeshPro.alignment = TextAlignmentOptions.Center;
						idNameTags.Add(vrrig, go);
					}
					GameObject nameTag = idNameTags[vrrig];
					TextMeshPro tmp = nameTag.GetComponent<TextMeshPro>() ?? nameTag.AddComponent<TextMeshPro>();
					tmp.text = vrrig.photonView.Owner.UserId;
					tmp.color = vrrig.playerColor();
					tmp.fontStyle = FontStyles.Normal;
					nameTag.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f) * 1f;
					nameTag.transform.position = GetNameTagPosition(vrrig);
					nameTag.transform.LookAt(Camera.main.transform.position);
					nameTag.transform.Rotate(0f, 180f, 0f);
				}
			}
		}

		public static void DisableIDTags()
		{
			foreach (KeyValuePair<VRRig, GameObject> nametag in idNameTags)
				Object.Destroy(nametag.Value);
			idNameTags.Clear();
		}


		private static readonly Dictionary<VRRig, GameObject> platformNameTags = new Dictionary<VRRig, GameObject>();
		public static void PlatformTags()
		{
			List<KeyValuePair<VRRig, GameObject>> nametagsCopy = platformNameTags.ToList();
			foreach (var nametag in nametagsCopy.Where(nametag => !GorillaParent.instance.vrrigs.Contains(nametag.Key)))
			{
				Object.Destroy(nametag.Value);
				platformNameTags.Remove(nametag.Key);
			}
			foreach (var vrrig in GorillaParent.instance.vrrigs)
			{
				if (vrrig != GorillaTagger.Instance.myVRRig)
				{
					if (!platformNameTags.ContainsKey(vrrig))
					{
						GameObject go = new GameObject("JupiterX_IDNameTag");
						go.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
						TextMeshPro TextMeshPro = go.AddComponent<TextMeshPro>();
						TextMeshPro.fontSize = 4.8f;
						TextMeshPro.alignment = TextAlignmentOptions.Center;
						platformNameTags.Add(vrrig, go);
					}
					GameObject nameTag = platformNameTags[vrrig];
					TextMeshPro tmp = nameTag.GetComponent<TextMeshPro>() ?? nameTag.AddComponent<TextMeshPro>();
					tmp.text = vrrig.GetPlatform();
					tmp.color = vrrig.playerColor();
					tmp.fontStyle = FontStyles.Normal;
					nameTag.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f) * 1f;
					nameTag.transform.position = GetNameTagPosition(vrrig);
					nameTag.transform.LookAt(Camera.main.transform.position);
					nameTag.transform.Rotate(0f, 180f, 0f);
				}
			}
		}

		public static void DisablePlatformTags()
		{
			foreach (KeyValuePair<VRRig, GameObject> nametag in platformNameTags)
				Object.Destroy(nametag.Value);
			platformNameTags.Clear();
		}

		private static readonly Dictionary<VRRig, GameObject> taggedNameTags = new Dictionary<VRRig, GameObject>();
		public static void TaggedTags()
		{
			List<KeyValuePair<VRRig, GameObject>> nametagsCopy = taggedNameTags.ToList();
			foreach (var nametag in nametagsCopy.Where(nametag => !GorillaParent.instance.vrrigs.Contains(nametag.Key)))
			{
				Object.Destroy(nametag.Value);
				taggedNameTags.Remove(nametag.Key);
			}
			foreach (var vrrig in GorillaParent.instance.vrrigs)
			{
				if (vrrig != GorillaTagger.Instance.myVRRig)
				{
					if (!taggedNameTags.ContainsKey(vrrig))
					{
						GameObject go = new GameObject("JupiterX_IDNameTag");
						go.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
						TextMeshPro TextMeshPro = go.AddComponent<TextMeshPro>();
						TextMeshPro.fontSize = 4.8f;
						TextMeshPro.alignment = TextAlignmentOptions.Center;
						taggedNameTags.Add(vrrig, go);
					}
					GameObject nameTag = taggedNameTags[vrrig];
					TextMeshPro tmp = nameTag.GetComponent<TextMeshPro>() ?? nameTag.AddComponent<TextMeshPro>();
					tmp.text = vrrig.IsTagged() ? "Tagged" : "";
					tmp.color = vrrig.playerColor();
					tmp.fontStyle = FontStyles.Normal;
					nameTag.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f) * 1f;
					nameTag.transform.position = GetNameTagPosition(vrrig);
					nameTag.transform.LookAt(Camera.main.transform.position);
					nameTag.transform.Rotate(0f, 180f, 0f);
				}
			}
		}

		public static void DisableTaggedTags()
		{
			foreach (KeyValuePair<VRRig, GameObject> nametag in taggedNameTags)
				Object.Destroy(nametag.Value);
			taggedNameTags.Clear();
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


		public static readonly Dictionary<string, GameObject> labelDictionary = new Dictionary<string, GameObject>();
		public static readonly Dictionary<bool, List<int>> labelDistances = new Dictionary<bool, List<int>>();
		public static float GetLabelDistance(bool leftHand)
		{
			if (!labelDistances.TryGetValue(leftHand, out List<int> frames))
			{
				frames = new List<int> { Time.frameCount };
				labelDistances[leftHand] = frames;
				return 0.2f;
			}
			if (frames[0] == Time.frameCount)
			{
				frames.Add(Time.frameCount);
				return 0.1f + Time.frameCount * 0.1f;
			}
			frames.Clear();
			frames.Add(Time.frameCount);
			return 0.1f + frames.Count * 0.1f;
		}

		public static void GetLabel(string codeName, bool leftHand, string text, Color color) // No this isnt skidding i fixed this for iiDk last year -nova
		{
			if (!labelDictionary.TryGetValue(codeName, out GameObject go))
			{
				go = new GameObject(codeName);
				go.transform.localScale = Vector3.one * (0.25f * 1f);
				labelDictionary.Add(codeName, go);
			}
			go.SetActive(true);
			TextMeshPro TextMeshPro = go.AddComponent<TextMeshPro>();
			TextMeshPro.color = color;
			TextMeshPro.fontSize = 2.4f;
			TextMeshPro.fontStyle = FontStyles.Italic;
			TextMeshPro.alignment = TextAlignmentOptions.Center;
			TextMeshPro.text = text;
			go.transform.position = (leftHand ? GorillaTagger.Instance.leftHandTransform : GorillaTagger.Instance.rightHandTransform).position + Vector3.up * (GetLabelDistance(leftHand) * 1f);
			go.transform.LookAt(Camera.main.transform.position);
			go.transform.Rotate(0f, 180f, 0f);
		}

		public static void VelocityLabel()
		{
			GetLabel("Velocity", false, $"{GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity.magnitude:F1}m/s", GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity.magnitude >= GorillaLocomotion.Player.Instance.maxJumpSpeed ? Color.green : Color.white);
		}

		private static string FormatTimer(int seconds)
		{
			int minutes = seconds / 60;
			int remainingSeconds = seconds % 60;
			string timeString = $"{minutes:D2}:{remainingSeconds:D2}";
			return timeString;
		}

		private static float startTime;
		private static float endTime;
		private static bool lastWasTagged;
		public static void TimeLabel()
		{
			if (PhotonNetwork.InRoom)
			{
				bool isThereTagged = InfectedList().Count > 0;
				if (isThereTagged)
				{
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
					GetLabel("Time", false, FormatTimer(Mathf.FloorToInt(playerIsTagged ? endTime : Time.time - startTime)), playerIsTagged ? Color.green : Color.white);
				}
				else
					startTime = Time.time;
			}
		}

		public static void NearbyTaggerLabel()
		{
			if (!GorillaTagger.Instance.myVRRig.IsTagged())
			{
				float closest = float.MaxValue;
				foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
				{
					if (vrrig.IsTagged() != GorillaTagger.Instance.myVRRig.IsTagged())
					{
						float dist = Vector3.Distance(GorillaTagger.Instance.headCollider.transform.position, vrrig.headMesh.transform.position);
						if (dist < closest)
							closest = dist;
					}
				}
				if (!Mathf.Approximately(closest, float.MaxValue))
				{
					Color colorn = Color.green;
					if (closest < 30f)
						colorn = Color.yellow;
					if (closest < 20f)
						colorn = new Color32(255, 90, 0, 255);
					if (closest < 10f)
						colorn = Color.red;
					GetLabel("NearbyTagger", true, $"{closest:F1}m", colorn);
				}
			}
		}

		public static void LastLabel()
		{
			if (PhotonNetwork.InRoom)
			{
				bool isThereTagged = InfectedList().Count > 0;
				int left = PhotonNetwork.PlayerList.Length - InfectedList().Count;
				if (isThereTagged)
				    GetLabel("LastLabel", true, left + " left", left <= 1 && !GorillaTagger.Instance.myVRRig.IsTagged() ? Color.green : Color.white);
			}
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
