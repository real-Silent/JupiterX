using JupiterX.Classes;
using JupiterX.Menu;
using JupiterX.Notifications;
using Photon.Pun;
using Photon.Voice.Unity;
using Photon.Voice.Unity.UtilityScripts;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JupiterX.Mods
{
    public class Fun
    {
        // Head mods from seralyth im lazy
        private static VRMap Head =>
            PhotonNetwork.InRoom ? GorillaTagger.Instance.myVRRig.head : GorillaTagger.Instance.offlineVRRig.head;

        private static float headSpinSpeed = 10f;
        public static void SpinHead(string axis)
        {
            if (Utility.myVRRig().enabled)
            {
                Vector3 rot = Head.trackingRotationOffset;
                switch (axis.ToLower())
                {
                    case "x":
                        rot.x += headSpinSpeed;
                        break;
                    case "y":
                        rot.y += headSpinSpeed;
                        break;
                    case "z":
                        rot.z += headSpinSpeed;
                        break;
                    default:
                        return;
                }
                Head.trackingRotationOffset = rot;
            }
            else
            {
                switch (axis.ToLower())
                {
                    case "x":
                        Head.rigTarget.transform.rotation = Quaternion.Euler(Head.rigTarget.transform.rotation.eulerAngles + new Vector3(headSpinSpeed, 0f, 0f));
                        break;
                    case "y":
                        Head.rigTarget.transform.rotation = Quaternion.Euler(Head.rigTarget.transform.rotation.eulerAngles + new Vector3(0f, headSpinSpeed, 0f));
                        break;
                    case "z":
                        Head.rigTarget.transform.rotation = Quaternion.Euler(Head.rigTarget.transform.rotation.eulerAngles + new Vector3(0f, 0f, headSpinSpeed));
                        break;
                    default:
                        return;
                }
            }
        }


        public static void FixHead()
        {
            Vector3 rot = Head.trackingRotationOffset;
            rot = Vector3.zero;
            Head.trackingRotationOffset = rot;
        }

        public static void UpsideDownHead()
        {
            Vector3 rot = Head.trackingRotationOffset;
            rot.z = 180f;
            Head.trackingRotationOffset = rot;
        }

        public static void BrokenNeck()
        {
            Vector3 rot = Head.trackingRotationOffset;
            rot.z = 90f;
            Head.trackingRotationOffset = rot;
        }

        public static void BackwardsHead()
        {
            Vector3 rot = Head.trackingRotationOffset;
            rot.y = 180f;
            Head.trackingRotationOffset = rot;
        }

        public static void SidewaysHead()
        {
            Vector3 rot = Head.trackingRotationOffset;
            rot.y = 90f;
            Head.trackingRotationOffset = rot;
        }

        public static float lastBangTime;
        public static readonly float BPM = 159f;

        public static void HeadBang()
        {
            Vector3 rot = Head.trackingRotationOffset;

            if (Time.time > lastBangTime)
            {
                rot.x = 50f;
                lastBangTime = Time.time + 60f / BPM;
            }
            else
                rot.x = Mathf.Lerp(rot.x, 0f, 0.1f);
            Head.trackingRotationOffset = rot;
        }


        private static Recorder _recorder;
        public static Recorder recorder
        {
            get
            {
                if (_recorder != null)
                    return _recorder;
                GameObject networkVoice = GameObject.Find("Network Voice");
                if (networkVoice != null)
                {
                    _recorder = networkVoice.GetComponent<Recorder>();
                    if (_recorder == null)
                        _recorder = networkVoice.AddComponent<Recorder>();
                    return _recorder;
                }
                GameObject photonManager = GameObject.Find("Photon Manager");
                if (photonManager != null)
                {
                    _recorder = photonManager.GetComponent<Recorder>();
                    if (_recorder == null)
                        _recorder = photonManager.AddComponent<Recorder>();
                }
                return _recorder;
            }
        }

        private static MicAmplifier _micAmplifier;
        public static MicAmplifier MicAmplifier
        {
            get
            {
                if (_micAmplifier != null)
                    return _micAmplifier;
                GameObject networkVoice = GameObject.Find("Network Voice");
                if (networkVoice != null)
                {
                    _micAmplifier = networkVoice.GetComponent<MicAmplifier>();
                    if (_micAmplifier == null)
                        _micAmplifier = networkVoice.AddComponent<MicAmplifier>();
                    return _micAmplifier;
                }
                GameObject photonManager = GameObject.Find("Photon Manager");
                if (photonManager != null)
                {
                    _micAmplifier = photonManager.GetComponent<MicAmplifier>();
                    if (_micAmplifier == null)
                        _micAmplifier = photonManager.AddComponent<MicAmplifier>();
                }
                return _micAmplifier;
            }
        }

        public static void CubeAll()
        {
            if (Utility.RightTrigger)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                    {
                        PhotonNetwork.Instantiate("bulletPrefab", rig.transform.position + new Vector3(0, 0, 0.6f), rig.headConstraint.transform.rotation);
                    }
                }
            }
        }

        public static void FixMic()
        {
            recorder.Bitrate = 20000;
            recorder.SourceType = Recorder.InputSourceType.Microphone;
            recorder.SourceType = Recorder.InputSourceType.Microphone;
            MicAmplifier.AmplificationFactor = 1;
            MicAmplifier.BoostValue = 0;
        }
        public static void LowQualityMic()
        {
            recorder.SourceType = Recorder.InputSourceType.Microphone;
            recorder.Bitrate = 12000;
            recorder.VoiceDetection = true;
            recorder.VoiceDetectionThreshold = 0.02f;
            MicAmplifier.AmplificationFactor = 1f;
            MicAmplifier.BoostValue = 0f;
        }

        public static void HighQualityMic()
        {
            recorder.SourceType = Recorder.InputSourceType.Microphone;
            recorder.Bitrate = 32000;
            recorder.VoiceDetection = false;
            MicAmplifier.AmplificationFactor = 2f;
            MicAmplifier.BoostValue = 10f;
        }

        public static void BassBoostMic()
        {
            recorder.SourceType = Recorder.InputSourceType.Microphone;
            recorder.Bitrate = 18000;
            recorder.VoiceDetection = false;
            MicAmplifier.AmplificationFactor = 2.5f;
            MicAmplifier.BoostValue = 15f;
        }
        public static void BassBoostMicExtreme()
        {
            recorder.SourceType = Recorder.InputSourceType.Microphone;
            recorder.Bitrate = 16000;
            recorder.VoiceDetection = false;
            MicAmplifier.AmplificationFactor = 3f;
            MicAmplifier.BoostValue = 25f;
        }

        public static void GetIdSelf()
        {
            NotificationManager.SendNotification($"<color=cyan>[INFO]</color> Your userid is {Utility.MyPlayer().UserId}");
            if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "JupiterX/Ids")))
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "JupiterX/Ids"));
            else if (!File.Exists(Path.Combine(Application.persistentDataPath, "JupiterX/Ids/SelfId.txt")))
                File.WriteAllText(Path.Combine(Application.persistentDataPath, "JupiterX/Ids/SelfId.txt"), Utility.MyPlayer().UserId);
            else
                File.WriteAllText(Path.Combine(Application.persistentDataPath, "JupiterX/Ids/SelfId.txt"), Utility.MyPlayer().UserId);
        }
        public static void GetIdGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who)
                    {
                        NotificationManager.SendNotification($"<color=cyan>[INFO]</color> There userid is {who.photonView.Owner.UserId}");
                        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "JupiterX/Ids")))
                            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "JupiterX/Ids"));
                        else if (!File.Exists(Path.Combine(Application.persistentDataPath, $"JupiterX/Ids/{who.photonView.Owner.NickName}_UserId.txt")))
                            File.WriteAllText(Path.Combine(Application.persistentDataPath, $"JupiterX/Ids/{who.photonView.Owner.NickName}_UserId.txt"), who.photonView.Owner.UserId);
                        else
                            File.WriteAllText(Path.Combine(Application.persistentDataPath, $"JupiterX/Ids/{who.photonView.Owner.NickName}_UserId.txt"), who.photonView.Owner.UserId);
                    }
                }
            }
        }

        public static void GetIdAll()
        {
            if (!PhotonNetwork.InRoom)
            {
                NotificationManager.SendNotification("<color=red>[ERROR]</color> Are you in a room ?");
                return;
            }
            foreach (Photon.Realtime.Player plr in PhotonNetwork.PlayerListOthers)
            {
                NotificationManager.SendNotification($"<color=cyan>[INFO]</color> There userid is {plr.UserId}");
                if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "JupiterX/Ids")))
                    Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "JupiterX/Ids"));
                else if (!File.Exists(Path.Combine(Application.persistentDataPath, $"JupiterX/Ids/{plr.NickName}_UserId.txt")))
                    File.WriteAllText(Path.Combine(Application.persistentDataPath, $"JupiterX/Ids/{plr.NickName}_UserId.txt"), plr.UserId);
                else
                    File.WriteAllText(Path.Combine(Application.persistentDataPath, $"JupiterX/Ids/{plr.NickName}_UserId.txt"), plr.UserId);
            }
        }

        public static void GrabRoomInfo()
        {
            if (!PhotonNetwork.InRoom)
            {
                NotificationManager.SendNotification("<color=red>[ERROR]</color> Are you in a room ?");
                return;
            }
            foreach (Photon.Realtime.Player plr in PhotonNetwork.PlayerList)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"---------------{PhotonNetwork.CurrentRoom.Name}---------------");
                stringBuilder.AppendLine($"NickName: {plr.NickName}, UserId: {plr.UserId}, Cosmetics: {RigManager.GetVRRigFromPlayer(plr).concatStringOfCosmeticsAllowed}");
                stringBuilder.Append("\nRoom Info Pulled By JupiterX");
                string payload = stringBuilder.ToString();
                NotificationManager.SendNotification($"<color=cyan>[INFO]</color> Grabbed player info for room {PhotonNetwork.CurrentRoom.Name}");
                if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "JupiterX/RoomInfo")))
                    Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "JupiterX/RoomInfo"));
                else if (!File.Exists(Path.Combine(Application.persistentDataPath, $"JupiterX/RoomInfo/{PhotonNetwork.CurrentRoom.Name}_Info.txt")))
                    File.WriteAllText(Path.Combine(Application.persistentDataPath, $"JupiterX/RoomInfo/{PhotonNetwork.CurrentRoom.Name}_Info.txt"), payload);
                else
                    File.WriteAllText(Path.Combine(Application.persistentDataPath, $"JupiterX/RoomInfo/{PhotonNetwork.CurrentRoom.Name}_Info.txt"), payload);
            }
        }

        public static void FixHandTaps()
        {
            GorillaTagger.Instance.handTapVolume = 0.1f;
        }
        public static void LoadHandTaps()
        {
            GorillaTagger.Instance.handTapVolume = float.MaxValue;
        }
        public static void SilentHandTaps()
        {
            GorillaTagger.Instance.handTapVolume = 0f;
        }
        public static void NoTapCooldown()
        {
            GorillaTagger.Instance.tapCoolDown = 0f;
        }
        public static void ResetTapcooldown()
        {
            GorillaTagger.Instance.tapCoolDown = 0.33f;
        }

        private static bool autoclickstate;
        public static void AutoClicker()
        {
            autoclickstate = !autoclickstate;
            if (Utility.LeftTriggerFloat > 0.5f)
            {
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.leftHand.calcT = autoclickstate ? 1f : 0f;
                    GorillaTagger.Instance.myVRRig.leftHand.MapMyFinger(1f);
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.leftHand.calcT = autoclickstate ? 1f : 0f;
                    GorillaTagger.Instance.offlineVRRig.leftHand.MapMyFinger(1f);
                }
            }
            if (Utility.RightTriggerFloat > 0.5f)
            {
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.rightHand.calcT = autoclickstate ? 1f : 0f;
                    GorillaTagger.Instance.myVRRig.rightHand.MapMyFinger(1f);
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.rightHand.calcT = autoclickstate ? 1f : 0f;
                    GorillaTagger.Instance.offlineVRRig.rightHand.MapMyFinger(1f);
                }
            }
        }

        public static void MuteGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    VRRig rig = Ray.collider.GetComponentInParent<VRRig>();
                    if (rig)
                    {
                        rig.muted = true;
                        GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().FirstOrDefault<GorillaPlayerScoreboardLine>(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).PressButton(true, GorillaPlayerLineButton.ButtonType.Mute);
                    }
                }
            }
        }

        public static void UnMuteGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    VRRig rig = Ray.collider.GetComponentInParent<VRRig>();
                    if (rig)
                    {
                        rig.muted = false;
                        GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().FirstOrDefault<GorillaPlayerScoreboardLine>(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).PressButton(false, GorillaPlayerLineButton.ButtonType.Mute);
                    }
                }
            }
        }

        public static void MuteAll()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                {
                    rig.muted = true;
                    GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().FirstOrDefault<GorillaPlayerScoreboardLine>(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).PressButton(true, GorillaPlayerLineButton.ButtonType.Mute);
                }
            }
        }

        public static void UnMuteAll()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                {
                    rig.muted = false;
                    GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().FirstOrDefault<GorillaPlayerScoreboardLine>(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).PressButton(false, GorillaPlayerLineButton.ButtonType.Mute);
                }
            }
        }

        public static void ReportGun()
        {
            if (Main.GetGunInput(false))
            {
                var GunData = Main.RenderGun();
                GameObject NewPointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Main.GetGunInput(true))
                {
                    VRRig rig = Ray.collider.GetComponentInParent<VRRig>();
                    if (rig)
                    {
                        GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().FirstOrDefault<GorillaPlayerScoreboardLine>(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).PressButton(true, GorillaPlayerLineButton.ButtonType.Report);
                        GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().FirstOrDefault<GorillaPlayerScoreboardLine>(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).PressButton(true, GorillaPlayerLineButton.ButtonType.Cheating);
                        GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().FirstOrDefault<GorillaPlayerScoreboardLine>(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).ReportPlayer(rig.photonView.Owner.UserId, GorillaPlayerLineButton.ButtonType.Cheating, rig.photonView.Owner.NickName);
                    }
                }
            }
        }

        public static void ReportAll()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && !rig.photonView.IsMine && !rig.isMyPlayer)
                {
                    GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().FirstOrDefault<GorillaPlayerScoreboardLine>(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).PressButton(true, GorillaPlayerLineButton.ButtonType.Report);
                    GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().FirstOrDefault<GorillaPlayerScoreboardLine>(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).PressButton(true, GorillaPlayerLineButton.ButtonType.Cheating);
                    GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().FirstOrDefault<GorillaPlayerScoreboardLine>(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).ReportPlayer(rig.photonView.Owner.UserId, GorillaPlayerLineButton.ButtonType.Cheating, rig.photonView.Owner.NickName);
                }
            }
        }
    }
}