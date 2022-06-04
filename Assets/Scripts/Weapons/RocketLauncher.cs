using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using UnityEngine;

namespace Assets.Scripts.SpaceInvaders.Core.Weapons
{
    /// <summary>
    ///     The most simple weapon a <see cref="IPlayerShip" />
    ///     can have in the 3D Space Invaders game.
    /// </summary>
    internal class RocketLauncher : IWeapon
    {
        private readonly Transform _playerShip;
        private readonly ExplosionManager _explosionManager;
        private readonly WeaponRayCast _weaponRayCast;

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
        ///     Construct a new <see cref="RocketLauncher" />.
        /// </summary>
        internal RocketLauncher(
            ILaser laser,
            Transform playerShip,
            WeaponRayCast weaponRayCast,
            ExplosionManager explosionManager)
        {
            this.Laser = laser;

            this.Damage = 50;

            this.Reach = 100;

            _playerShip = playerShip;

            _weaponRayCast = weaponRayCast;

            _explosionManager = explosionManager;
        }


        /// <summary>
        ///     Specifies how this <see cref="IWeapon" />
        ///     shoots.
        /// </summary>
        public void Shoot()
        {
            if (!Input.GetKey(KeyCode.R))
            {
                this.Laser.Display(Vector3.zero, new RayCastShootResult());

                return;
            }

            this.CreateRay();
        }

        /// <summary>
        ///     Creates a ray for shooting this <see cref="IWeapon" />.
        /// </summary>
        private void CreateRay()
        {
            var direction = _playerShip.forward;

            var delta = direction * this.Reach;

            var targetPos = _playerShip.position + delta;

            var shootResult = _weaponRayCast.ShootRay(this.Laser.Origin, direction, this.Reach);

            this.Laser.Display(targetPos, shootResult);

            if (!shootResult.Success) return;

            shootResult.GameEntity.Health -= this.Damage;

            _explosionManager.Manage(shootResult);

        }
    }
}