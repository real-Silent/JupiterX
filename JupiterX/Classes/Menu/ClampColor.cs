using System;
using UnityEngine;

namespace JupiterX.Classes
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ClampColor : MonoBehaviour
    {
        public ClampColor(IntPtr e) : base(e) { }
        public virtual void Start()
        {
            targetRenderer.gameObject.GetComponent<ColorChanger>()?.Start();

            gameObjectRenderer = GetComponent<Renderer>();
            Update();
        }

        public virtual void Update()
        {
            if (gameObjectRenderer.material.shader != targetRenderer.material.shader)
                gameObjectRenderer.material = new Material(targetRenderer.material.shader);

            if (targetRenderer.material.mainTexture != null && gameObjectRenderer.material.mainTexture != targetRenderer.material.mainTexture)
                gameObjectRenderer.material.mainTexture = targetRenderer.material.mainTexture;

            gameObjectRenderer.material.color = targetRenderer.material.color;
        }

        public Renderer gameObjectRenderer;
        public Renderer targetRenderer;
    }
}