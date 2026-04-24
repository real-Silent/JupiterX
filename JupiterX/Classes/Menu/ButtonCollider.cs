using System;
using UnityEngine;
using static JupiterX.Menu.Main;
using static JupiterX.Settings;

namespace JupiterX.Classes
{

    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ButtonCollider : MonoBehaviour
	{
		public ButtonCollider(IntPtr ptr ) : base(ptr) { }
		public string relatedText;
        public bool incremental;
        public bool positive;

        public static float buttonCooldown = 0f;

        public void OnTriggerEnter(Collider collider)
		{
			if (Time.time > buttonCooldown && collider == buttonCollider && menu != null)
			{
                buttonCooldown = Time.time + 0.2f;
                GorillaTagger.Instance.StartVibration(RightHanded, GorillaTagger.Instance.tagHapticStrength / 2f, GorillaTagger.Instance.tagHapticDuration / 2f);
                if (!DisableButtonSounds)
				    Utility.PlayEmbeddedSoundOnHand("JupiterX.Resources.steal.wav");
                if (incremental)
                    ToggleIncremental(relatedText, positive);
                else
                    Toggle(relatedText, true);
            }
		}
	}
}
