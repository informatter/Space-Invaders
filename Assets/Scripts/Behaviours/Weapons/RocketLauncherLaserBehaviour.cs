using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using Assets.Scripts.SpaceInvaders.Core.Weapons;
using UnityEngine;

namespace Assets.Scripts.Behaviours
{
    [RequireComponent(typeof(LineRenderer))]
    public class RocketLauncherLaserBehaviour : MonoBehaviour
    {
        public ILaser RocketLauncherLaser;

        /// <summary>
        ///     Called once during the lifetime of a script or after a
        ///     Game Object has been activated after being deactivated.
        ///     Awake is called before any Start function.
        /// </summary>
        public void Awake()
        {
            RocketLauncherLaser = new RocketLauncherLaser(this.transform, this.GetComponent<LineRenderer>());
        }
    }
}