using System;
using System.Collections;
using UnityEngine;

namespace JupiterX.Notifications
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ShibaNotificationLib : MonoBehaviour
    {
        public ShibaNotificationLib(IntPtr ptr) : base(ptr) { }

        public static ShibaNotificationLib instance;

        private void Start()
        {
            instance = this;
        }

        public static void SendNoti(string noti, float duration = -1f)
        {
            if (instance == null)
                return;

            NotificationManager.SendNotification(noti, duration);

            if (NotificationManager.NotifiText == null)
                return;

            MelonLoader.MelonCoroutines.Start(
                instance.AnimatedNotification(
                    NotificationManager.NotifiText.gameObject,
                    duration < 0 ? 3f : duration
                )
            );
        }

        public IEnumerator AnimatedNotification(GameObject textObj, float lifetime)
        {
            if (textObj == null)
                yield break;

            GameObject bg = GameObject.CreatePrimitive(PrimitiveType.Cube);

            Destroy(bg.GetComponent<Collider>());

            bg.transform.localScale = Vector3.zero;

            Renderer renderer = bg.GetComponent<Renderer>();

            renderer.material = new Material(Shader.Find("Standard"));

            Color baseColor = Settings.backgroundColor.GetCurrentColor();
            baseColor.a = 0f;

            renderer.material.color = baseColor;

            SetupTransparentMaterial(renderer.material);

            Transform textTransform = textObj.transform;

            float intro = 0.15f;
            float outro = 0.25f;

            Vector3 targetScale = new Vector3(
                0.3120967f,
                0.1220963f,
                0.01752696f
            );

            /*
             * INTRO ANIMATION
             */

            float t = 0f;

            while (t < intro)
            {
                t += Time.deltaTime;

                float lerp = t / intro;

                UpdateFollow(bg, textTransform);

                bg.transform.localScale =
                    Vector3.Lerp(Vector3.zero, targetScale, lerp);

                Color c = baseColor;
                c.a = Mathf.Lerp(0f, 0.85f, lerp);

                renderer.material.color = c;

                yield return null;
            }

            bg.transform.localScale = targetScale;

            Color holdColor = baseColor;
            holdColor.a = 0.85f;

            renderer.material.color = holdColor;

            /*
             * HOLD
             */

            float holdTime = Mathf.Max(0, lifetime - outro);

            t = 0f;

            while (t < holdTime)
            {
                t += Time.deltaTime;

                UpdateFollow(bg, textTransform);

                yield return null;
            }

            /*
             * OUTRO
             */

            t = 0f;

            while (t < outro)
            {
                t += Time.deltaTime;

                float lerp = t / outro;

                UpdateFollow(bg, textTransform);

                Color c = holdColor;
                c.a = Mathf.Lerp(0.85f, 0f, lerp);

                renderer.material.color = c;

                bg.transform.localScale =
                    Vector3.Lerp(targetScale,
                                 targetScale * 0.85f,
                                 lerp);

                yield return null;
            }

            Destroy(bg);
        }

        private void UpdateFollow(GameObject bg, Transform target)
        {
            if (bg == null || target == null)
                return;

            bg.transform.position =
                target.position + target.forward * 0.01f;

            bg.transform.rotation =
                Camera.main.transform.rotation;
        }

        private void SetupTransparentMaterial(Material mat)
        {
            mat.SetFloat("_Mode", 3);

            mat.SetInt("_SrcBlend",
                (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

            mat.SetInt("_DstBlend",
                (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

            mat.SetInt("_ZWrite", 0);

            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");

            mat.renderQueue = 3000;
        }
    }
}