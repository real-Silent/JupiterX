using System;
using System.Collections;
using UnityEngine;

namespace JupiterX.Notifications
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ShibaNotificationLib : MonoBehaviour
    {
        public ShibaNotificationLib(IntPtr e) : base(e) { }

        public static ShibaNotificationLib instance = null;
        public GameObject NotiBackground;

        public virtual void Start()
        {
            instance = this;
            CreateNotificationBackground(Camera.main.transform.position * 2f, Camera.main.transform.rotation);
        }

        public virtual void FixedUpdate()
        {

        }

        public void CreateNotificationBackground(Vector3 pos, Quaternion rot)
        {
            NotiBackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            NotiBackground.transform.localScale = new Vector3(0.3120967f, 0.1220963f, 0.01752696f);
            NotiBackground.transform.position = pos;
            NotiBackground.transform.rotation = rot;
            NotiBackground.GetComponent<Renderer>().material.color = Settings.backgroundColor.GetCurrentColor();
        }

        public IEnumerator DestroyObjectFading(GameObject go, float delayTime)
        {
            if (go == null)
                yield break;
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer == null)
                yield break;
            Material mat = renderer.material;
            Color color = mat.color;
            float startAlpha = color.a;
            float elapsed = 0f;
            while (elapsed < delayTime)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, 0f, elapsed / delayTime);
                color.a = alpha;
                mat.color = color;
                yield return null;
            }
            Destroy(go);
        }
    }
}