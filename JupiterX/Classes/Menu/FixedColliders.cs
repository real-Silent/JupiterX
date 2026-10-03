// ============================================================
//  JupiterX | Classes/Menu/FixedColliders.cs
//  Copyright (c) 2026 Jupiterx (@NAuth). All rights reserved.
//
//  This software and its source code are the property of the
//  author. Unauthorized copying, redistribution, modification,
//  or reuse of any part of this project, in whole or in part,
//  without express written permission is strictly prohibited.
//
//  Version: 2.0.0 | By Silent/Ashley/Nova (@s1lnt)
// ============================================================

using JupiterX.Menu;
using UnityEngine;

namespace JupiterX.Classes.Menu
{
    public class FixedColliders
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
