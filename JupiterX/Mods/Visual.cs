// ============================================================
//  JupiterX | Mods/Visual.cs
//  Copyright (c) 2026 Jupiterx (@NAuth). All rights reserved.
//
//  This software and its source code are the property of the
//  author. Unauthorized copying, redistribution, modification,
//  or reuse of any part of this project, in whole or in part,
//  without express written permission is strictly prohibited.
//
//  Version: 2.0.0 | By Silent/Ashley/Nova (@s1lnt)
// ============================================================

using Console;
using GorillaNetworking;
using JupiterX.Menu;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            if (PhotonNetwork.InRoom)
            {
                foreach (var vrrig in GorillaParent.instance.vrrigs.ToArray().Where(vrrig => vrrig != Utility.myVRRig()))
                {
                    vrrig.lerpValueBody = 2f;
                    vrrig.lerpValueFingers = 1f;
                }
            }
        }

        public static void ReSmoothRigs()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (var vrrig in GorillaParent.instance.vrrigs.ToArray().Where(vrrig => vrrig != Utility.myVRRig()))
                {
                    vrrig.lerpValueBody = Utility.myVRRig().lerpValueBody;
                    vrrig.lerpValueFingers = Utility.myVRRig().lerpValueFingers;
                }
            }
        }

        private static readonly Dictionary<VRRig, GameObject> boxEspPool = new Dictionary<VRRig, GameObject>();
        public static void BoxESP()
        {
            if (!PhotonNetwork.InRoom)
            {
                DisableBoxESP();
                return;
            }
            List<VRRig> remove = null;
            foreach (var pair in boxEspPool)
            {
                if (pair.Key == null || !GorillaParent.instance.vrrigs.Contains(pair.Key))
                {
                    remove ??= new List<VRRig>();
                    remove.Add(pair.Key);
                    if (pair.Value != null)
                        Object.Destroy(pair.Value);
                }
            }
            if (remove != null)
            {
                foreach (var rig in remove)
                    boxEspPool.Remove(rig);
            }
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && rig != Utility.myVRRig())
                {
                    if (!boxEspPool.TryGetValue(rig, out GameObject box))
                    {
                        box = new GameObject("box");
                        box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        box.transform.position = rig.headConstraint.transform.position;
                        box.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        GameObject.Destroy(box.GetComponent<BoxCollider>());
                        boxEspPool[rig] = box;
                    }
                    bool isTagged = rig.mainSkin.material.name.Contains("fected");
                    box.transform.rotation = rig.transform.rotation;
                    box.GetComponent<Renderer>().material.shader = Utility.GUIShader();
                    box.GetComponent<Renderer>().material.color = isTagged ? Color.red : Color.grey;
                }
            }
        }
        public static void DisableBoxESP()
        {
            foreach (var obj in boxEspPool.Values)
            {
                if (obj != null)
                    Object.Destroy(obj);
            }
            boxEspPool.Clear();
        }

        private static readonly Dictionary<VRRig, GameObject> capsuleEspPool = new Dictionary<VRRig, GameObject>();
        public static void CapsuleESP()
        {
            if (!PhotonNetwork.InRoom)
            {
                DisableCapsuleESP();
                return;
            }
            List<VRRig> remove = null;
            foreach (var pair in capsuleEspPool)
            {
                if (pair.Key == null || !GorillaParent.instance.vrrigs.Contains(pair.Key))
                {
                    remove ??= new List<VRRig>();
                    remove.Add(pair.Key);
                    if (pair.Value != null)
                        Object.Destroy(pair.Value);
                }
            }
            if (remove != null)
            {
                foreach (var rig in remove)
                    capsuleEspPool.Remove(rig);
            }
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && rig != Utility.myVRRig())
                {
                    if (!capsuleEspPool.TryGetValue(rig, out GameObject box))
                    {
                        box = new GameObject("box");
                        box = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                        box.transform.position = rig.headConstraint.transform.position;
                        box.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        GameObject.Destroy(box.GetComponent<BoxCollider>());
                        capsuleEspPool[rig] = box;
                    }
                    bool isTagged = rig.mainSkin.material.name.Contains("fected");
                    box.transform.rotation = rig.transform.rotation;
                    box.GetComponent<Renderer>().material.shader = Utility.GUIShader();
                    box.GetComponent<Renderer>().material.color = isTagged ? Color.red : Color.grey;
                }
            }
        }
        public static void DisableCapsuleESP()
        {
            foreach (var obj in capsuleEspPool.Values)
            {
                if (obj != null)
                    Object.Destroy(obj);
            }
            capsuleEspPool.Clear();
        }


        private static readonly Dictionary<VRRig, GameObject> sphereEspPool = new Dictionary<VRRig, GameObject>();
        public static void SphereESP()
        {
            if (!PhotonNetwork.InRoom)
            {
                DisableSphereESP();
                return;
            }
            List<VRRig> remove = null;
            foreach (var pair in sphereEspPool)
            {
                if (pair.Key == null || !GorillaParent.instance.vrrigs.Contains(pair.Key))
                {
                    remove ??= new List<VRRig>();
                    remove.Add(pair.Key);
                    if (pair.Value != null)
                        Object.Destroy(pair.Value);
                }
            }
            if (remove != null)
            {
                foreach (var rig in remove)
                    sphereEspPool.Remove(rig);
            }
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && rig != Utility.myVRRig())
                {
                    if (!sphereEspPool.TryGetValue(rig, out GameObject box))
                    {
                        box = new GameObject("box");
                        box = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        box.transform.position = rig.headConstraint.transform.position;
                        box.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        GameObject.Destroy(box.GetComponent<BoxCollider>());
                        sphereEspPool[rig] = box;
                    }
                    bool isTagged = rig.mainSkin.material.name.Contains("fected");
                    box.transform.rotation = rig.transform.rotation;
                    box.GetComponent<Renderer>().material.shader = Utility.GUIShader();
                    box.GetComponent<Renderer>().material.color = isTagged ? Color.red : Color.grey;
                }
            }
        }
        public static void DisableSphereESP()
        {
            foreach (var obj in sphereEspPool.Values)
            {
                if (obj != null)
                    Object.Destroy(obj);
            }
            sphereEspPool.Clear();
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
            nametag.characterSize = 0.03f;
            nametag.anchor = TextAnchor.MiddleCenter;
            nametag.alignment = TextAlignment.Center;
            textHolder.transform.position = rig.headConstraint.transform.position + new Vector3(0f, 1.15f + (index * -0.15f), 0f);
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
                    if (rig != null && rig != Utility.myVRRig())
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
                    if (rig != null && rig != Utility.myVRRig())
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
                    if (rig != null && rig != Utility.myVRRig())
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
                    if (rig != null && rig != Utility.myVRRig())
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
                    if (rig != null && rig != Utility.myVRRig())
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

        private static readonly Dictionary<VRRig, LineRenderer> tracersPool = new Dictionary<VRRig, LineRenderer>();
        private static readonly HashSet<VRRig> liveRigs = new HashSet<VRRig>();
        private static readonly List<VRRig> staleRigs = new List<VRRig>();
        private static Material tracerMaterial;
        public static void Tracers()
        {
            if (!PhotonNetwork.InRoom)
            {
                CleanUpTracers();
                return;
            }
            VRRig localRig = Utility.myVRRig();
            Vector3 handPos = Utility.RightHandTransform().position;
            liveRigs.Clear();
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig == null || rig == localRig || !rig.gameObject.activeInHierarchy)
                    continue;

                liveRigs.Add(rig);
                if (!tracersPool.TryGetValue(rig, out LineRenderer line) || line == null)
                {
                    line = CreateTracer();
                    tracersPool[rig] = line;
                }
                Color color = rig.IsTagged() ? Color.red : Color.grey;
                line.startColor = color;
                line.endColor = color;
                line.SetPosition(0, rig.headMesh.transform.position);
                line.SetPosition(1, handPos);
            }
            staleRigs.Clear();
            foreach (var pair in tracersPool)
            {
                if (pair.Key == null || pair.Value == null || !liveRigs.Contains(pair.Key))
                    staleRigs.Add(pair.Key);
            }
            foreach (VRRig rig in staleRigs)
            {
                LineRenderer line = tracersPool[rig];
                if (line != null)
                    Object.Destroy(line.gameObject);
                tracersPool.Remove(rig);
            }
        }
        public static void CleanUpTracers()
        {
            foreach (LineRenderer line in tracersPool.Values)
            {
                if (line != null)
                    Object.Destroy(line.gameObject);
            }
            tracersPool.Clear();
        }
        private static LineRenderer CreateTracer()
        {
            if (tracerMaterial == null)
                tracerMaterial = new Material(Utility.GUIShader());
            GameObject holder = new GameObject("Tracer");
            LineRenderer line = holder.AddComponent<LineRenderer>();
            line.sharedMaterial = tracerMaterial;
            line.positionCount = 2;
            line.useWorldSpace = true;
            line.startWidth = 0.01f;
            line.endWidth = 0.01f;
            line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            line.receiveShadows = false;
            return line;
        }

        public static void fullBright()
        {
            RenderSettings.fog = false;
            RenderSettings.ambientLight = Color.white;
        }

        public static void fulldrak()
        {
            RenderSettings.fog = true;
            RenderSettings.ambientLight = Color.black;
        }

        public static void Chams(bool chams)
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && rig != Utility.myVRRig())
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
            DrawLabel(Utility.RightHandTransform(), "Velocity", $"{rb.velocity.magnitude:F1}m/s", rb.velocity.magnitude >= GorillaLocomotion.Player.Instance.maxJumpSpeed ? Color.green : Color.white);
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

            bool playerIsTagged = Utility.myVRRig().IsTagged();
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
            DrawLabel(Utility.RightHandTransform(), "Time", FormatTimer(Mathf.FloorToInt(playerIsTagged ? endTime : Time.time - startTime)), playerIsTagged ? Color.green : Color.white);
        }

        public static void NearbyTaggerLabel()
        {
            if (GorillaTagger.Instance == null || GorillaParent.instance == null)
                return;
            if (Utility.myVRRig().IsTagged())
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
            DrawLabel(Utility.LeftHandTransform(), "NearbyTagger", $"{closest:F1}m", colorn);
        }
        public static void LastLabel()
        {
            if (!PhotonNetwork.InRoom)
                return;
            if (InfectedList().Count == 0)
                return;
            int left = PhotonNetwork.PlayerList.Length - InfectedList().Count;
            DrawLabel(Utility.LeftHandTransform(), "LastLabel", left + " left", left <= 1 && !Utility.myVRRig().IsTagged() ? Color.green : Color.white);
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
                case "HUNT":
                    GorillaHuntManager huntManager = (GorillaHuntManager)GorillaGameManager.instance;
                    infected.AddRange(huntManager.currentHunted.ToArray());
                    break;
                default:
					break;
			}
			return infected;
		}
	}
}
