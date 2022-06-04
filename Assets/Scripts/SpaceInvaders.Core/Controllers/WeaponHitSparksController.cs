
using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using UnityEngine;

namespace Assets.Scripts.SpaceInvaders.Core.Controllers
{
    /// <summary>
    ///     A class which is responsible for
    ///     instantiating and destroying the sparks prefab for
    ///     weapon hits.
    /// </summary>
    public class WeaponHitSparksController : IExplosionController
    {
        private readonly GameObject _sparks;

        /// <summary>
        ///     Create a new <see cref="WeaponHitSparksController" />.
        /// </summary>
        public WeaponHitSparksController( GameObject sparksPrefab)
        {
            _sparks = sparksPrefab;

            _sparks.SetActive(false);
        }

        /// <summary>
        ///     Creates a spark effect in the <see cref="RayCastShootResult.RayCastHit" />
        ///     location where a <see cref="IWeapon" /> shot.
        /// </summary>
        /// <param name="shootResult"></param>
        public void Create(RayCastShootResult shootResult)
        {
            _sparks.SetActive(true);

            var explosion = Object.Instantiate(_sparks, shootResult.RayCastHit.point, Quaternion.identity);

            float duration = explosion.GetComponent<ParticleSystem>().main.duration;

            Object.Destroy(explosion, duration);

            _sparks.SetActive(false);
        }
    }
}