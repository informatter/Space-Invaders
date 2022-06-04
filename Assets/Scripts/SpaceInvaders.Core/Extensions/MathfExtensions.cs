using UnityEngine;

namespace Assets.Scripts.SpaceInvaders.Core.Extensions
{
    public static class MathfExtensions
    {
        /// <summary>
        ///     Convert the the provided <paramref name="angle" />
        ///     to radians.
        /// </summary>
        public static float ToRadians(this float angle)
        {
            return angle * 180f / Mathf.PI;
        }
    }
}