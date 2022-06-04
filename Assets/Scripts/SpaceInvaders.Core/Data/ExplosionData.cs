using System;
using UnityEngine;

namespace Assets.Scripts.SpaceInvaders.Core
{
    /// <summary>
    ///     Stores the data for an explosion event in the
    ///     game.
    /// </summary>
    public class ExplosionData : EventArgs
    {
        /// <summary>
        ///     The explosion prefab to instantiate.
        /// </summary>
        public GameObject ExplosionPrefab { get; }

        /// <summary>
        ///     The hit point where the explosion happened.
        /// </summary>
        public Vector3 HitPoint { get; }

        /// <summary>
        ///     Construct a new <see cref="ExplosionData" />.
        /// </summary>
        /// <param name="explosionPrefab"></param>
        /// <param name="hitPoint"></param>
        /// <remarks>
        ///     Stores the data for an explosion event in the
        ///     game.
        /// </remarks>
        public ExplosionData(GameObject explosionPrefab, Vector3 hitPoint)
        {
            this.ExplosionPrefab = explosionPrefab;

            this.HitPoint = hitPoint;
        }
    }
}