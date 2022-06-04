using System.Collections.Generic;
using Assets.Scripts.SpaceInvaders.Core.Interfaces;

namespace Assets.Scripts.SpaceInvaders.Core
{

    public class ExplosionManager
    {
        /// <summary>
        /// A collection of <see cref="IExplosionController"/>.
        /// </summary>
        public IList<IExplosionController> ExplosionFactories { get; }

        /// <summary>
        /// Construct a new <see cref="ExplosionManager"/>.
        /// </summary>
        /// <param name="explosionFactories"></param>
        public ExplosionManager(IList<IExplosionController> explosionFactories)
        {
            this.ExplosionFactories = explosionFactories;
        }

        /// <summary>
        ///     Computes this <see cref="IRunner" />.
        /// </summary>
        public void Manage(RayCastShootResult rayCastShootResult)
        {
            foreach (var explosionFactory in this.ExplosionFactories)
                explosionFactory.Create(rayCastShootResult);
        }
    }
}
