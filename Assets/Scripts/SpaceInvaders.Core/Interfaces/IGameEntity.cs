using System;

namespace Assets.Scripts.SpaceInvaders.Core.Interfaces
{
    /// <summary>
    ///     A contract for the most simplest of objects
    ///     for the 3D space invaders game.
    /// </summary>
    public interface IGameEntity
    {
        /// <summary>
        ///     Occurs when this <see cref="IGameEntity" />'s
        ///     health reaches zero;
        /// </summary>
        event EventHandler<ExplosionData> Exploded;

        /// <summary>
        ///     The total health this <see cref="IGameEntity" />
        ///     has.
        /// </summary>
        int Health { get; set; }

        /// <summary>
        ///     Computes the desired behaviour for this <see cref="IGameEntity" />
        /// </summary>
       // void ComputeBehaviour();


    }
}