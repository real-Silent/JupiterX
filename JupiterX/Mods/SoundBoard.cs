using Il2CppSystem.Net;
using JupiterX.Menu;
using MelonLoader;
using Photon.Voice.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using JupiterX.Classes;
using Photon.Pun;
using JupiterX.Notifications;

namespace JupiterX.Mods
{
    public class SoundBoard // this file was made by (@domok.)
    {
        private static bool SoundLoaded = false;
        private static AudioClip downloadedSound = null;
        public static bool AudioIsPlaying = false;
        public static float RecoverTime = -1f;
        public static bool LoopAudio = false;
        public static string Subdirectory = "";
        public static void LoadSoundboard()
        {
            string basePath = Path.Combine("JupiterX", "Sounds", Subdirectory.TrimStart('/').Replace("\\", "/"));
            if (!Directory.Exists("JupiterX")) 
                Directory.CreateDirectory("JupiterX"); 
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);
            List<string> enabledSounds = new List<string>();
            foreach (ButtonInfo binfo in Buttons.buttons[14])
            {
                if (binfo.enabled)
                    enabledSounds.Add(binfo.overlapText);
            }
            List<ButtonInfo> soundbuttons = new List<ButtonInfo>
            {
                new ButtonInfo { buttonText = "Exit Soundboard", method = () => Buttons.CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns you to main menu." }
            };
            int index = 0;
            string[] folders = Directory.GetDirectories(basePath);
            foreach (string folder in folders)
            {
                index++;
                string folderName = Path.GetFileName(folder);
                string relativePath = Path.Combine(Subdirectory.TrimStart('/'), folderName).Replace("\\", "/");
                soundbuttons.Add(new ButtonInfo { buttonText = $"SoundboardFolder{index}", overlapText = $"▶ {folderName}", method = () => OpenFolder(relativePath), isTogglable = false, toolTip = $"Opens the {folderName} folder." });
            }
            index = 0;
            string[] files = Directory.GetFiles(basePath);
            foreach (string file in files)
            {
                index++;
                string fileName = Path.GetFileName(file);
                string cleanName = RemoveFileExtension(fileName).Replace("_", " ");
                bool isEnabled = enabledSounds.Contains(cleanName);
                string relativePath = Path.Combine("JupiterX", "Sounds", Subdirectory.TrimStart('/'), fileName).Replace("\\", "/");
                if (LoopAudio)
                    soundbuttons.Add(new ButtonInfo { buttonText = $"SoundboardSound{index}", overlapText = cleanName, enableMethod = () => PlaySoundFile(relativePath), disableMethod = () => StopAllSounds(), enabled = isEnabled, toolTip = $"Plays \"{cleanName}\" through your microphone." });
                else
                    soundbuttons.Add(new ButtonInfo { buttonText = $"SoundboardSound{index}", overlapText = cleanName, method = () => PlaySoundFile(relativePath), isTogglable = false, toolTip = $"Plays \"{cleanName}\" through your microphone." });
            }
            soundbuttons.Add(new ButtonInfo { buttonText = "Stop All Sounds", method = () => StopAllSounds(), isTogglable = false, toolTip = "Stops all currently playing sounds." });
            soundbuttons.Add(new ButtonInfo { buttonText = "Reload Sounds", method = () => LoadSoundboard(), isTogglable = false, toolTip = "Reloads all of your sounds." });
            //soundbuttons.Add(new ButtonInfo { buttonText = "Get More Sounds", method = LoadSoundLibrary, isTogglable = false, toolTip = "Opens a public audio library, where you can download your own sounds." });
            Buttons.CurrentCategoryName = "Soundboard";
            Buttons.buttons[Buttons.GetCategory("Soundboard")] = soundbuttons.ToArray();
        }
        public static void LoadSoundLibrary() => 
            MelonCoroutines.Start(LoadSoundLibraryCoroutine());
        private static IEnumerator LoadSoundLibraryCoroutine() // this iiDks btw
        {
            string url = "https://github.com/iiDk-the-actual/ModInfo/raw/main/SoundLibrary.txt";
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();
#if UNITY_2020_1_OR_NEWER
    if (request.result != UnityWebRequest.Result.Success)
#else
            if (request.isNetworkError || request.isHttpError)
#endif
            {
                yield break;
            }
            string libraryText = request.downloadHandler.text;
            string[] audios = AlphabetizeNoSkip(libraryText.Split('\n'));
            List<ButtonInfo> soundbuttons = new List<ButtonInfo>
            {
                new ButtonInfo
                {
                    buttonText = "Exit Sound Library",
                    method = () => LoadSoundboard(),
                    isTogglable = false,
                    toolTip = "Returns you back to the soundboard."
                }
            };
            int index = 0;
            foreach (string audio in audios)
            {
                if (audio.Length > 2)
                {
                    index++;
                    string[] data = audio.Split(';');
                    if (data.Length >= 2)
                    {
                        soundbuttons.Add(new ButtonInfo
                        {
                            buttonText = "SoundboardDownload" + index,
                            overlapText = data[0],
                            method = () => DownloadSound(data[0], data[1]),
                            isTogglable = false,
                            toolTip = $"Downloads {data[0]} to your sound library."
                        });
                    }
                }
            }
            Buttons.CurrentCategoryName = "Temporary Category";
            Buttons.buttons[Buttons.GetCategory("Temporary Category")] = soundbuttons.ToArray();
        }
        public static string LoadSoundFromURL(string resourcePath, string fileName)
        {
            string folderName = "JupiterX";
            string fullPath = Path.Combine(folderName, fileName);
            try
            {
                if (!Directory.Exists(folderName))
                    Directory.CreateDirectory(folderName);
                if (!File.Exists(fullPath))
                {
                    UnityEngine.Debug.Log($"Downloading {fileName} from {resourcePath}");
                    WebClient client = new WebClient();
                    client.DownloadFile(resourcePath, fullPath);
                }
                LoadAndPlaySound(fileName);
                return fullPath;
            }
            catch { return null; }
        }

        public static Dictionary<string, AudioClip> audioFilePool = new Dictionary<string, AudioClip> { };
        private static GameObject audiomgr = null;
        public static void Play2DAudio(AudioClip sound, float volume)
        {
            if (audiomgr == null)
            {
                audiomgr = new GameObject("2DAudioMgr");
                AudioSource temp = audiomgr.AddComponent<AudioSource>();
                temp.spatialBlend = 0f;
            }
            AudioSource ausrc = audiomgr.GetComponent<AudioSource>();
            ausrc.volume = volume;
            ausrc.PlayOneShot(sound);
        }
        public static void DownloadSound(string name, string url)
        {
            if (name.Contains(".."))
                name = name.Replace("..", "");
            if (name.Contains(":"))
                return;
            string filename = "Sounds" + Subdirectory + "/" + name + "." + GetFileExtension(url);
            if (File.Exists("JupiterX/" + filename))
                File.Delete("JupiterX/" + filename);
            if (audioFilePool.ContainsKey(name))
                audioFilePool.Remove(name);
            AudioClip soundDownloaded = (AudioClip)LoadSoundFromURL(url, filename);
            if (soundDownloaded.length < 20f)
                Play2DAudio(soundDownloaded, 1f);
        }
        public static string GetFileExtension(string fileName) =>
            fileName.ToLower().Split('.')[fileName.Split('.').Length - 1];
        public static string RemoveLastDirectory(string directory) => 
            directory == "" || directory.LastIndexOf('/') <= 0 ? "" : directory.Substring(0, directory.LastIndexOf('/'));
        public static string[] AlphabetizeNoSkip(string[] array)
        {
            if (array.Length <= 1)
                return array;
            string first = array[0];
            string[] others = array.OrderBy(s => s).ToArray();
            return others.ToArray();
        }
        public static void OpenFolder(string folder)
        {
            Subdirectory = "/" + folder.Trim('/');
            LoadSoundboard();
        }
        public static string RemoveFileExtension(string file)
        {
            int index = 0;
            string output = "";
            string[] split = file.Split('.');
            foreach (string part in split)
            {
                index++;
                if (index != split.Length)
                {
                    if (index > 1)
                        output += ".";
                    output += part;
                }
            }
            return output;
        }
        public static void LoadAndPlaySound(string soundpath)
        {
            if (!File.Exists(soundpath))
                return;
            string extension = Path.GetExtension(soundpath).ToLowerInvariant();
            if (extension != ".wav")
                return;
            byte[] soundData = File.ReadAllBytes(soundpath);
            AudioClip clip = CreateAudioClipFromWav(soundData, Path.GetFileNameWithoutExtension(soundpath));
            if (clip != null)
                PlayAudioThroughMicrophone(clip);
            else
                NotifiLib.SendNotification($"Audio clip with path of {soundpath} is null somehow please make sure everything is correct", 25f);
        }

        private static AudioClip CreateAudioClipFromWav(byte[] wavData, string clipName)
        {
            try
            {
                if (wavData.Length < 44) return null;
                int channels = BitConverter.ToInt16(wavData, 22);
                int sampleRate = BitConverter.ToInt32(wavData, 24);
                int bitsPerSample = BitConverter.ToInt16(wavData, 34);
                int dataSize = wavData.Length - 44;
                int sampleCount = dataSize / (channels * (bitsPerSample / 8));
                AudioClip audioClip = AudioClip.Create(clipName, sampleCount, channels, sampleRate, false);
                float[] samples = new float[sampleCount * channels];
                if (bitsPerSample == 16)
                {
                    for (int i = 0; i < sampleCount * channels; i++)
                    {
                        short sample = BitConverter.ToInt16(wavData, 44 + i * 2);
                        samples[i] = sample / 32768f;
                    }
                }
                else if (bitsPerSample == 8)
                {
                    for (int i = 0; i < sampleCount * channels; i++)
                    {
                        byte sample = wavData[44 + i];
                        samples[i] = (sample - 128) / 128f;
                    }
                }
                ExitGames.Client.Photon.Hashtable neededForSoundBoard = new ExitGames.Client.Photon.Hashtable();
                neededForSoundBoard.Add("imusingthesoundboard", "imusingthesoundboard");
                PhotonNetwork.LocalPlayer.SetCustomProperties(neededForSoundBoard);
                audioClip.SetData(samples, 0);
                return audioClip;
            }
            catch { return null; }
        }
        private static void PlayAudioThroughMicrophone(AudioClip clip)
        {
            if (clip == null)
                return;
            try
            {
                Recorder component = GameObject.Find("NetworkVoice")?.GetComponent<Recorder>() ?? GameObject.Find("Photon Manager")?.GetComponent<Recorder>();
                if (component == null)
                    return;
                component.SourceType = Recorder.InputSourceType.AudioClip;
                component.AudioClip = clip;
                typeof(Recorder).GetMethod("RestartRecording")?.Invoke(component, new object[] { LoopAudio });
                typeof(Recorder).GetProperty("DebugEchoMode")?.SetValue(component, true);
                AudioIsPlaying = true;
                RecoverTime = Time.time + clip.length + 0.4f;
                ExitGames.Client.Photon.Hashtable neededForSoundBoard = new ExitGames.Client.Photon.Hashtable();
                neededForSoundBoard.Add("imusingthesoundboard", "imusingthesoundboard");
                PhotonNetwork.LocalPlayer.SetCustomProperties(neededForSoundBoard);
            }
            catch { }
        }
        public static void RestoreMicrophone()
        {
            try
            {
                Recorder component = GameObject.Find("NetworkVoice")?.GetComponent<Recorder>() ?? GameObject.Find("Photon Manager")?.GetComponent<Recorder>();
                if (component != null)
                {
                    component.SourceType = Recorder.InputSourceType.Microphone;
                    component.AudioClip = null;
                    typeof(Recorder).GetMethod("RestartRecording")?.Invoke(component, new object[] { true });
                    typeof(Recorder).GetProperty("DebugEchoMode")?.SetValue(component, false);
                    AudioIsPlaying = false;
                    RecoverTime = -1f;
                }
            }
            catch { }
        }
        public static void StopAllSounds() => 
            RestoreMicrophone();
        public static void PlaySoundFile(string soundpath) => 
            LoadAndPlaySound(soundpath);
        public static void Update()
        {
            if (AudioIsPlaying && RecoverTime > 0 && Time.time >= RecoverTime)
                RestoreMicrophone();
        }
        public static byte[] LoadSoundFromResource(string soundFileName)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                string resourcePath = $"Light.Sounds.{soundFileName}";
                using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                {
                    if (stream != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        stream.CopyTo(ms);
                        return ms.ToArray();
                    }
                }
            }
            catch { }
            return null;
        }
        public static void PlayResourceSound(string soundFileName)
        {
            try
            {
                byte[] soundData = LoadSoundFromResource(soundFileName);
                if (soundData != null && soundData.Length > 0)
                {
                    AudioClip clip = CreateAudioClipFromWav(soundData, Path.GetFileNameWithoutExtension(soundFileName));
                    if (clip != null)
                        PlayAudioThroughMicrophone(clip);
                }
            }
            catch { }
        }
        public static void PlayLoadedSound()
        {
            if (downloadedSound != null && SoundLoaded)
                PlayAudioThroughMicrophone(downloadedSound);
        }
        public static void ResetLoadedSound()
        {
            SoundLoaded = false;
            downloadedSound = null;
        }
    }
}