using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using UnityEngine;

namespace Assets.Scripts.SpaceInvaders.Core
{
    /// <summary>
    ///     A structure that contains the results from the
    ///     <see cref="WeaponRayCast" />.
    /// </summary>
    public readonly struct RayCastShootResult
    {
        /// <summary>
        ///     Determines of the ray hit something or not.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        ///     Determines if the <see cref="WeaponRayCast" />
        ///     was within the <see cref="IWeapon" />'s reach threshold.
        ///     If not, this <see cref="Success" /> will be false.
        /// </summary>
        public bool WithinWeaponReach { get; }

        /// <summary>
        ///     The <see cref="IGameEntity" /> that was hit.
        /// </summary>
        public IGameEntity GameEntity { get; }

        /// <summary>
        ///     Unity's <see cref="RaycastHit" />.
        /// </summary>
        public RaycastHit RayCastHit { get; }

        /// <summary>
        ///     Construct a new <see cref="RayCastShootResult" />.
        /// </summary>
        public RayCastShootResult(IGameEntity gameEntity, RaycastHit rayCastHit, bool success, bool withinWeaponReach)
        {
            this.GameEntity = gameEntity;

            this.RayCastHit = rayCastHit;

            this.Success = success;

            this.WithinWeaponReach = withinWeaponReach;
        }
    }
}