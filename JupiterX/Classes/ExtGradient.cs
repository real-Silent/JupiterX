using UnityEngine;

namespace JupiterX.Classes
{
    public class ExtGradient
    {
        public GradientColorKey[] colors = new GradientColorKey[]
        {
            new GradientColorKey(Color.black, 0f),
            new GradientColorKey(Color.red, 0.5f),
            new GradientColorKey(Color.black, 1f)
        };

        private static Gradient getColorGradient;
        public Color GetColorTime(float time)
        {
            getColorGradient ??= new Gradient();

            getColorGradient.colorKeys = colors;
            return getColorGradient.Evaluate(time);
        }

        public Color GetCurrentColor(float offset = 0f) =>
            GetColorTime((offset + Time.time / 2f) % 1f);

        public bool isRainbow = false;
        public bool copyRigColors = false;
    }
}
