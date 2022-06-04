using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using UnityEngine;

namespace Assets.Scripts.SpaceInvaders.Core.Weapons
{
    public class EnemyRocketLauncher : IEnemyWeapon
    {
        private readonly Transform _enemy;
        private readonly WeaponRayCast _weaponRayCast;
        private readonly ExplosionManager _explosionManager;

        private int _framesAfterShot;
        private bool _shot;

        /// <summary>
        ///     The <see cref="ILaser" /> used by this <see cref="IWeapon" />.
        /// </summary>
        public ILaser Laser { get; }

        /// <summary>
        ///     The amount of damage this <see cref="IWeapon" />
        ///     does in a single shot.
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        ///     The amount of units this <see cref="IWeapon" />
        ///     can reach. Not all weapons can have the same reach.
        /// </summary>
        public int Reach { get; set; }

        /// <summary>
        ///     Construct a new <see cref="EnemyRocketLauncher" />.
        /// </summary>
        public EnemyRocketLauncher(
            ILaser laser,
            Transform enemy,
            WeaponRayCast weaponRayCast,
            ExplosionManager explosionManager)
        {
            _enemy = enemy;

            _framesAfterShot = 0;

            _shot = false;

            _weaponRayCast = weaponRayCast;

            _explosionManager = explosionManager;

            this.Laser = laser;

            this.Reach = 100;

            this.Damage = 50;
        }


        /// <summary>
        ///     Specifies how this <see cref="IWeapon" />
        ///     shoots.
        /// </summary>
        public void Shoot(LineOfSightResults lineOfSightResults)
        {
            _framesAfterShot++;

            this.Laser.Display(lineOfSightResults.RayTarget, new RayCastShootResult());

            if (_framesAfterShot >= 20)
            {
                _framesAfterShot = 0;

                _shot = false;
            }

            if (lineOfSightResults.Within && _shot == false)
            {
                var direction = lineOfSightResults.RayTarget - _enemy.position;

                var shootResult = _weaponRayCast.ShootRay(this.Laser.Origin, direction, this.Reach);

                this.Laser.Display(lineOfSightResults.RayTarget, shootResult);

                _shot = true;

                _framesAfterShot++;

                if (!shootResult.Success) return;

                // Don't damage other enemy ships!
                if (shootResult.GameEntity is IEnemyShip) return;

                shootResult.GameEntity.Health -= this.Damage;

                _explosionManager.Manage(shootResult);

            }
        }
    }
}