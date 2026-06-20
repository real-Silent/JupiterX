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