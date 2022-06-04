using Assets.Scripts.Settings;
using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.SpaceInvaders.Core.Controllers
{
    public class ShipExplosionController : IExplosionController
    {
        private readonly GameObject _shipExplosion;
        private readonly GameObject _shipLowHealthExplosion;
        private bool _createdLowHealthFire;

        private GameObject _shipLowHealthExplosionInstance;
        public ShipExplosionController(GameObject shipExplosion, GameObject shipLowHealthShipExplosion)
        {
            _shipExplosion = shipExplosion;

            _createdLowHealthFire = false;

            _shipLowHealthExplosion = shipLowHealthShipExplosion;

            _shipExplosion.SetActive(false);

            _shipLowHealthExplosion.SetActive(false);
        }

        /// <summary>
        ///     Creates a spark effect in the <see cref="RayCastShootResult.RayCastHit" />
        ///     location where a <see cref="IWeapon" /> shot.
        /// </summary>
        /// <param name="shootResult"></param>
        public void Create(RayCastShootResult shootResult)
        {
            
            if (shootResult.GameEntity.Health <= InternalGameSettings.ShipLowHealthThreshold && !_createdLowHealthFire)
            {
                if (shootResult.GameEntity is IMeteorite) return;

                _shipLowHealthExplosion.SetActive(true);

                 _shipLowHealthExplosionInstance = Object.Instantiate(
                    _shipLowHealthExplosion, shootResult.RayCastHit.point, Quaternion.identity);

                 _shipLowHealthExplosionInstance.transform.SetParent(shootResult.RayCastHit.transform,false);

               _createdLowHealthFire = true;


            }

            if (shootResult.GameEntity.Health <= 0)
            {
                if (shootResult.GameEntity is IMeteorite) return;

                _shipExplosion.SetActive(true);

                var explosion = Object.Instantiate(_shipExplosion, shootResult.RayCastHit.point, Quaternion.identity);

                float duration = explosion.GetComponent<ParticleSystem>().main.duration;

                var gameObject = shootResult.RayCastHit.transform.gameObject;

                Object.Destroy(explosion, duration);

                Object.Destroy(_shipLowHealthExplosionInstance, duration);

                if(shootResult.GameEntity is IPlayerShip == false) Object.Destroy(gameObject, duration);

                _shipExplosion.SetActive(false);

                _shipLowHealthExplosion.SetActive(false);
            }

            
        }
    }
}
