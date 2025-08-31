using JupiterX.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX.Classes
{
    internal class FixedColliders
    {
        public static void CheckButton()
        {
            float num = Vector3.Distance(FixedColliders.button.transform.position, FixedColliders.reference.transform.position);
            if (Time.frameCount >= Main.framePressCooldown + 30 && (double)num <= 0.02)
            {
                Main.Toggle(FixedColliders.relatedText);
                Main.framePressCooldown = Time.frameCount;
            }
        }

        static Transform smethod_0(GameObject gameObject_0)
        {
            return gameObject_0.transform;
        }
        static Vector3 smethod_1(Transform transform_0)
        {
            return transform_0.position;
        }
        static int smethod_2()
        {
            return Time.frameCount;
        }
        public static string relatedText;
        public static GameObject reference;
        public static GameObject button;
    }
}
