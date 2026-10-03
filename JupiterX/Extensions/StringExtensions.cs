// ============================================================
//  JupiterX | Extensions/StringExtensions.cs
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
using static JupiterX.Menu.Main;

namespace JupiterX.Extensions
{
    public static class StringExtensions
    {
        public static string ClearTags(this string input) =>
            NoRichtextTags(input);

        public static string EnforceLength(this string str, int maxLength) =>
            str.Length > maxLength ? str[..maxLength] : str;

        public static string ColorToHex(Color color) =>
            ColorUtility.ToHtmlStringRGB(color);
    }
}