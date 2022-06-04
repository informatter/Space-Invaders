using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using UnityEngine;

namespace Assets.Scripts.SpaceInvaders.Core.Weapons
{
    public class RocketLauncherLaser : ILaser
    {
        private readonly Transform _laserPrefab;
        private readonly LineRenderer _lineRenderer;

        /// <summary>
        ///     Determines if this <see cref="ILaser" />
        ///     can be displayed or not.
        /// </summary>
        public bool CanDisplay { get; set; }

        /// <summary>
        ///     The origin of this <see cref="ILaser" />.
        /// </summary>
        public Vector3 Origin => _laserPrefab.position;

        /// <summary>
        ///     Construct a new <see cref="RocketLauncherLaser" />.
        /// </summary>
        /// <param name="laserPrefab"></param>
        public RocketLauncherLaser(Transform laserPrefab, LineRenderer lineRenderer)
        {
            _laserPrefab = laserPrefab;

            _lineRenderer = lineRenderer;

            _lineRenderer.enabled = false;
        }

        /// <summary>
        ///     Displays this <see cref="ILaser" />.
        /// </summary>
        /// <param name="target">
        ///     The target this <see cref="ILaser" />
        ///     will stop at.
        /// </param>
        public void Display(Vector3 target, RayCastShootResult shootResult)
        {
            _lineRenderer.enabled = false;

            if (shootResult.WithinWeaponReach)
            {
                _lineRenderer.enabled = true;

                _lineRenderer.SetPosition(0, _laserPrefab.position);

                _lineRenderer.SetPosition(1, target);
            }
        }
    }
}