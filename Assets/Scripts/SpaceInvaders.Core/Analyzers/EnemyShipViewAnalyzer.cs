using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using UnityEngine;

namespace Assets.Scripts.SpaceInvaders.Core
{
    /// <summary>
    /// This <see cref="EnemyShipViewAnalyzer"/> is responsible
    /// for analyzing the field of view of a
    /// <see cref="IEnemyShip"/>, and determining if a
    /// <see cref="IPlayerShip"/> is visible or not.
    /// </summary>
    public class EnemyShipViewAnalyzer
    {
        private readonly Transform _playerShip;
        private readonly Transform _enemyShipTransform;
        private readonly IEnemyShip _enemyShip;

        /// <summary>
        /// Construct a new <see cref="EnemyShipViewAnalyzer"/>.
        /// </summary>
        /// <remarks>
        /// This <see cref="EnemyShipViewAnalyzer"/> is responsible
        /// for analyzing the field of view of a
        /// <see cref="IEnemyShip"/>, and determining if a
        /// <see cref="IPlayerShip"/> is visible or not.
        /// </remarks>
        public EnemyShipViewAnalyzer( 
            Transform enemyShipTransform, 
            Transform playerShip,
            IEnemyShip enemyShip)
        {
            _playerShip = playerShip;

            _enemyShipTransform = enemyShipTransform;

            _enemyShip = enemyShip;

        }

        /// <summary>
        ///     A method which computes if the player is within
        ///     the line of sight of this <see cref="IEnemy" />.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if the player is within the <see cref="IEnemy.SightThreshold" />
        ///     otherwise <see langword="false" />.
        /// </returns>
        public LineOfSightResults Analyze()
        {
            var result = new LineOfSightResults();

            var enemyWeaponLaser = _enemyShip.Weapon.Laser;

            var directionToTarget = _playerShip.position - enemyWeaponLaser.Origin;

            var ray = new Ray(enemyWeaponLaser.Origin, directionToTarget);

            // ALWAYS USE DEBUG.DRAWRAY to display vectors! Debug.DrawLine shows incorrectly!
            //Debug.DrawRay(this.Weapon.Laser.Origin, directionToTarget.normalized*this.Weapon.Reach,Color.cyan);

            bool hit = Physics.Raycast(ray, out var hitInfo);

            float angle = Vector3.Angle(_enemyShipTransform.forward, directionToTarget);

            if (hit)
            {
                var player = hitInfo.transform.gameObject.GetComponentInParent(typeof(IPlayerShip));

                // this means that an obstacle is in the way.
                if (player == null) return result;

                if (angle <= _enemyShip.SightThreshold)
                    return new LineOfSightResults(true, ray, _playerShip.position);
            }

            else
            {
                if (angle <= _enemyShip.SightThreshold)
                    return new LineOfSightResults(true, ray, _playerShip.position);
            }

            return result;
        }
    }
}