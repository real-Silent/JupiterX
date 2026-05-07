using Photon.Pun;
using Photon.Voice.Unity;
using Photon.Voice.Unity.UtilityScripts;
using UnityEngine;

namespace JupiterX.Mods
{
    public class Fun
    {
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
            if (Utility.RTrigger)
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

        public static void FixHandTaps()
        {
            GorillaTagger.Instance.handTapVolume = 1f;
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
            GorillaTagger.Instance.tapCoolDown = 1f;
        }

        private static bool autoclickstate;
        public static void AutoClicker()
        {
            autoclickstate = !autoclickstate;
            if (Utility.LTriggerFloat > 0.5f)
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
            if (Utility.RTriggerFloat > 0.5f)
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
    }
}