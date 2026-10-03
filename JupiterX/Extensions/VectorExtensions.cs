// ============================================================
//  JupiterX | Extensions/VectorExtensions.cs
//  Copyright (c) 2026 Jupiterx (@NAuth). All rights reserved.
//
//  This software and its source code are the property of the
//  author. Unauthorized copying, redistribution, modification,
//  or reuse of any part of this project, in whole or in part,
//  without express written permission is strictly prohibited.
//
//  Version: 2.0.0 | By Silent/Ashley/Nova (@s1lnt)
// ============================================================

using UnityEngine;

namespace JupiterX.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 X_Z(this Vector3 vector3)
        {
            return new Vector3(vector3.x, 0f, vector3.z);
        }
    }
}