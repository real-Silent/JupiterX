using System;
using UnityEngine;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX.Classes
{

    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ColorChanger : TimedBehaviour
    {
        public ColorChanger(IntPtr ptr) : base(ptr) { }

        public override void Start()
        {
            base.Start();
            renderer = base.GetComponent<Renderer>();
            Update();
        }

        public override void Update()
        {
            base.Update();
            if (colorInfo != null)
            {
                if (!colorInfo.copyRigColors)
                {
                    Color color = new Gradient { colorKeys = colorInfo.colors }.Evaluate((Time.time / 2f) % 1);
                    if (colorInfo.isRainbow)
                    {
                        float h = (Time.frameCount / 180f) % 1f;
                        color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                    }
                    renderer.material.color = color;
                }
                else
                {
                    renderer.material = GorillaTagger.Instance.offlineVRRig.mainSkin.material;
                }
            }
        }

        public Renderer renderer;
        public Gradient colors = null;
        public ExtGradient colorInfo;
        public bool isRainbow = false;
        public bool isPastelRainbow = false;
        public bool isSlowFade = false;
        public bool isEpileptic = false;
        public bool isMonkeColors = false;
    }
}
