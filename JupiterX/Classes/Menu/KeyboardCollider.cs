using System;
using System.Collections.Generic;
using UnityEngine;
using static JupiterX.Menu.Main;

namespace JupiterX.Classes
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class KeyboardKey : MonoBehaviour
    {
        public KeyboardKey(IntPtr e) : base(e) { }
        public static readonly Dictionary<string, KeyboardKey> keyLookupDictionary = new Dictionary<string, KeyboardKey>();
        public string key;
        public static float delay;

        public virtual void Start() =>
            keyLookupDictionary[gameObject.name] = this;

        public virtual void OnTriggerEnter(Collider collider)
        {
            if ((collider != lKeyCollider && collider != rKeyCollider) || menu == null || !(Time.time > delay)) return;
                delay = Time.time + 0.1f;
            if (!Settings.DisableButtonSounds)
                Utility.PlayEmbeddedSoundOnHand("JupiterX.Resources.steal.wav");
            PressKeyboardKey(key);
        }
    }
}