using System;
using UnityEngine;

namespace JupiterX.Classes
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ColorChanger : MonoBehaviour
    {
        public ColorChanger(IntPtr e) : base(e) { }
        public virtual void Start()
        {
            if (colors == null)
            {
                Destroy(this);
                return;
            }

            targetRenderer = GetComponent<Renderer>();

            if (colors.IsFlat())
            {
                Update();
                Destroy(this);
                return;
            }

            Update();
        }

        public virtual void Update()
        {
            targetRenderer.enabled = overrideTransparency ?? !colors.transparent;

            if (colors.transparent)
                return;

            targetRenderer.material.color = colors.GetCurrentColor();

            Color color = targetRenderer.material.color;
            color.a = 0.5f;
            targetRenderer.material.color = color;
        }

        public Renderer targetRenderer;
        public ExtGradient colors;
        public bool? overrideTransparency;
    }
}