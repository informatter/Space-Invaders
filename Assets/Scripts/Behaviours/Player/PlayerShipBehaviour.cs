using System;
using System.Collections.Generic;
using Assets.Scripts.SpaceInvaders.Core;
using Assets.Scripts.SpaceInvaders.Core.Controllers;
using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using Assets.Scripts.SpaceInvaders.Core.Weapons;
using UnityEngine;

namespace Assets.Scripts.Behaviours
{
    /// <summary>
    ///     This <see cref="PlayerShipBehaviour" /> is what
    ///     will actually get attached to the <see cref="GameObject" />
    ///     int Unity.
    /// </summary>
    public class PlayerShipBehaviour : MonoBehaviour, IPlayerShip
    {
        [SerializeField] private int _initHealth;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private int _maxSteerSpeed;
        [SerializeField] private GameObject _rocketLauncherLaserPrefab;
        [SerializeField] private GameObject _sparksPrefab;
        [SerializeField] private GameObject _lowHealthExplosionPrefab;
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private GameObject _meteoriteExplosionPrefab;


        private Transform _playerShip;

        private IPlayerShipMove _playerShipMove;

        /// <summary>
        ///     Occurs when this <see cref="IGameEntity" />'s
        ///     health reaches zero;
        /// </summary>
        public event EventHandler<ExplosionData> Exploded;

        /// <summary>
        ///     The total health this <see cref="IGameEntity" />
        ///     has.
        /// </summary>
        public int Health { get; set; }


        /// <summary>
        ///     The <see cref="IWeapon" /> this <see cref="IPlayerShip" />
        ///     uses.
        /// </summary>
        public IWeapon Weapon { get; set; }

        public void Awake()
        {
            _playerShip = this.transform;

            this.Health = 3000;

            var meteoriteExplosionFactory = new MeteoriteExplosionController(_meteoriteExplosionPrefab);

            var weaponHitSparksFactory = new WeaponHitSparksController(_sparksPrefab);

            var shipExplosionFactory = new ShipExplosionController(_explosionPrefab, _lowHealthExplosionPrefab);

            var explosionManager = new ExplosionManager(
                new List<IExplosionController> {weaponHitSparksFactory,
                    shipExplosionFactory,meteoriteExplosionFactory

                });

            var rocketLauncherLaserBehaviour = _rocketLauncherLaserPrefab.GetComponent<RocketLauncherLaserBehaviour>();

            var rocketLauncherLaser = rocketLauncherLaserBehaviour.RocketLauncherLaser;

            var weaponRayCast = new WeaponRayCast();

            this.Weapon = new RocketLauncher(rocketLauncherLaser, _playerShip, weaponRayCast, explosionManager);

            _playerShipMove = new PlayerShipMove(_playerShip, _maxSpeed, _maxSteerSpeed);
        }

        /// <summary>
        ///     Determines the behavior how this <see cref="IPlayerShip" />
        ///     flies.
        /// </summary>
        public void Fly()
        {
            _playerShipMove.Steer();

            _playerShipMove.Propulsion();
        }

        /// <summary>
        ///     Determines the behaviour of how this <see cref="IPlayerShip" />
        ///     should shoot.
        /// </summary>
        public void Shoot()
        {
            this.Weapon.Shoot();
        }

        public void FixedUpdate()
        {
            if (_playerShip == null) return;

            this.Shoot();
        }

        public void Update()
        {
            if (_playerShip == null) return;

            this.Fly();

            if (this.Health > 0) return;

           this.gameObject.GetComponent<PlayerShipBehaviour>().enabled = false;

           UnityEditor.EditorApplication.isPlaying = false;


            var explosionData = new ExplosionData(_playerShip.gameObject, _playerShip.position);

            this.Exploded?.Invoke(this, explosionData);
        }

    }
}