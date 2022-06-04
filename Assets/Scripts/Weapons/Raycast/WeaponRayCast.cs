using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using UnityEngine;

namespace Assets.Scripts.SpaceInvaders.Core.Weapons
{
    /// <summary>
    ///     This class contains the core shooting
    ///     infrastructure that can be used by all <see cref="IWeapon" />'s
    /// </summary>
    public class WeaponRayCast
    {
        /// <summary>
        ///     Performs a ray cast.
        /// </summary>
        /// <param name="direction"></param>
        public RayCastShootResult ShootRay(Vector3 origin, Vector3 direction, float maxDistance)
        {
            if (direction.magnitude > maxDistance)
                return new RayCastShootResult(null, default, false, false);

            var ray = new Ray(origin, direction);

            bool hit = Physics.Raycast(ray, out var hitInfo);

            if (!hit)
                return new RayCastShootResult(default, hitInfo, false, true);

            // This could become a potential bug in the future... I am assuming here that
            // I will always be attaching an empty game object to my prefabs.
            var gameEntity = hitInfo.transform.gameObject.GetComponentInParent<IGameEntity>();

            return new RayCastShootResult(gameEntity, hitInfo, true, true);
        }
    }
}