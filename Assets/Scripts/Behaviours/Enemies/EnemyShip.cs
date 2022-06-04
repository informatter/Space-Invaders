using System;
using System.Collections.Generic;
using Assets.Scripts.Behaviours;
using Assets.Scripts.SpaceInvaders.Core;
using Assets.Scripts.SpaceInvaders.Core.Controllers;
using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using Assets.Scripts.SpaceInvaders.Core.Weapons;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    /// <summary>
    /// TODO: Every IEnemy Ship would have its own concrete MonoBehaviour
    /// With their unique characteristics
    /// </summary>
    public class EnemyShip : MonoBehaviour, IEnemyShip
    {
        private IEnemyMove _enemySpaceShipMove;
        private Transform _playerSpaceShip;
        private Transform _thisEnemySpaceShip;

        [SerializeField] private GameObject _playerSpaceShipPrefab;
        [SerializeField] private GameObject _rocketLauncherLaserPrefab;
        [SerializeField] private GameObject _sparksPrefab;
        [SerializeField] private GameObject _lowHealthExplosionPrefab;
        [SerializeField] private GameObject _shipExplosionPrefab;
        [SerializeField] private GameObject _meteoriteExplosionPrefab;


        /// <summary>
        ///     The <see cref="EnemyShip" /> this <see cref="IPlayerShip" />
        ///     uses.
        /// </summary>
        public IEnemyWeapon Weapon { get; set; }

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
        ///     A value in radians which determines factor
        ///     by which this <see cref="IEnemy" /> can spot
        ///     a player.
        /// </summary>
        public float SightThreshold { get; set; }

        /// <summary>
        /// A class responsible
        /// for analyzing the field of view of a
        /// <see cref="IEnemyShip"/>, and determining if a
        /// <see cref="IPlayerShip"/> is visible or not.
        /// </summary>
        public EnemyShipViewAnalyzer EnemyShipViewAnalyzer { get; set; }

        /// <summary>
        ///     Called once during the lifetime of a script and after
        ///     any Awake functions.
        /// </summary>
        public void Start()
        {
            _thisEnemySpaceShip = this.transform;

            _playerSpaceShip = _playerSpaceShipPrefab.transform;

            this.Health = 3000;

            this.SightThreshold = 100;

            var rocketLauncherLaserBehaviour = _rocketLauncherLaserPrefab.GetComponent<RocketLauncherLaserBehaviour>();

            var rocketLauncherLaser = rocketLauncherLaserBehaviour.RocketLauncherLaser;


            var meteoriteExplosionFactory = new MeteoriteExplosionController(_meteoriteExplosionPrefab);

            var weaponHitSparksFactory = new WeaponHitSparksController(_sparksPrefab);

            var shipExplosionFactory = new ShipExplosionController(_shipExplosionPrefab, _lowHealthExplosionPrefab);

            var explosionManager = new ExplosionManager(
                new List<IExplosionController> {weaponHitSparksFactory,
                    shipExplosionFactory,meteoriteExplosionFactory

                });


            var weaponRayCast = new WeaponRayCast();

            this.Weapon =
                new EnemyRocketLauncher(rocketLauncherLaser, _thisEnemySpaceShip, weaponRayCast, explosionManager);

            _enemySpaceShipMove = new EnemyShipMove(_playerSpaceShip, _thisEnemySpaceShip, 5, this.SightThreshold);

            this.EnemyShipViewAnalyzer =
                new EnemyShipViewAnalyzer(_thisEnemySpaceShip, _playerSpaceShip, this);
        }

        public void Update()
        {
            if (_thisEnemySpaceShip == null || _playerSpaceShip ==null) return;

            var result = this.EnemyShipViewAnalyzer.Analyze();

            this.Fly(result);

            this.Shoot(result);

            if (this.Health > 0) return;

            var explosionData = new ExplosionData(_thisEnemySpaceShip.gameObject, _thisEnemySpaceShip.position);

            this.Exploded?.Invoke(this,explosionData);
        }

        /// <summary>
        ///     Determines the behavior how this <see cref="IPlayerShip" />
        ///     flies.
        /// </summary>
        public void Fly(LineOfSightResults lineOfSightResults)
        {
            bool avoided = _enemySpaceShipMove.AvoidObstacle();

            if (!avoided)
                _enemySpaceShipMove.Follow();

            _enemySpaceShipMove.Move();
        }

        /// <summary>
        ///     Determines the behaviour of how this <see cref="IPlayerShip" />
        ///     should shoot.
        /// </summary>
        public void Shoot(LineOfSightResults lineOfSightResults)
        {
            this.Weapon.Shoot(lineOfSightResults);
        }

    }
}