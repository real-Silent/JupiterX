using UnityEngine;
using static JupiterX.Menu.Main;

namespace JupiterX.Extensions
{
    public static class StringExtensions
    {
        public static string ClearTags(this string input) =>
            NoRichtextTags(input);

        public static string ColorToHex(Color color) =>
            ColorUtility.ToHtmlStringRGB(color);
    }
}