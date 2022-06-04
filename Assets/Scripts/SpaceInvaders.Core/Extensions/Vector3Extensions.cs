using UnityEngine;

namespace Assets.Scripts.SpaceInvaders.Core.Extensions
{
    public static class Vector3Extensions
    {
        /// <summary>
        ///     Rotate a vector by a specific angle in radians
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="radians"></param>
        public static Vector3 VectorRotate(this Vector3 vector, float radians)
        {
            //Jhon Vince, Mathematics for computer graphics
            float cosTheta = Mathf.Cos(radians);

            float sinTheta = Mathf.Sin(radians);

            return new Vector3(cosTheta * vector.x - sinTheta * vector.z,
                sinTheta * vector.x + cosTheta * vector.z,
                sinTheta * vector.z + vector.y * cosTheta);
        }
    }
}