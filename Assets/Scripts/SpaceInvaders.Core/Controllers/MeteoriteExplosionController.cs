using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.SpaceInvaders.Core.Controllers
{
    public class MeteoriteExplosionController : IExplosionController
    {
        private GameObject _meteoriteExplosion;

        /// <summary>
        /// Construtc a new <see cref="MeteoriteExplosionController"/>.
        /// </summary>
        /// <param name="meteoriteExplosion"></param>
        public MeteoriteExplosionController(GameObject meteoriteExplosion)
        {
            _meteoriteExplosion = meteoriteExplosion;


            _meteoriteExplosion.SetActive(false);
        }

        /// <summary>
        ///     Creates a spark effect in the <see cref="RayCastShootResult.RayCastHit" />
        ///     location where a <see cref="IWeapon" /> shot.
        /// </summary>
        /// <param name="shootResult"></param>
        public void Create(RayCastShootResult shootResult)
        {
            var gameEntity = shootResult.GameEntity;

            if (gameEntity is IEnemyShip || gameEntity is IPlayerShip) return;

            if (gameEntity.Health <= 0)
            {
                _meteoriteExplosion.SetActive(true);

                var explosion = Object.Instantiate(_meteoriteExplosion, shootResult.RayCastHit.point, Quaternion.identity);

                float duration = explosion.GetComponent<ParticleSystem>().main.duration;

                var gameObject = shootResult.RayCastHit.transform.gameObject;

                Object.Destroy(explosion, duration);

                Object.Destroy(gameObject, duration);

                _meteoriteExplosion.SetActive(false);
            }
        }
    }
}
