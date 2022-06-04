namespace Assets.Scripts.SpaceInvaders.Core.Interfaces
{
    /// <summary>
    ///     A contract for all concrete weapon implementations
    /// </summary>
    public interface IWeapon
    {
        /// <summary>
        ///     The <see cref="ILaser" /> used by this <see cref="IWeapon" />.
        /// </summary>
        ILaser Laser { get; }

        /// <summary>
        ///     The amount of damage this <see cref="IWeapon" />
        ///     does in a single shot.
        /// </summary>
        int Damage { get; set; }

        /// <summary>
        ///     The amount of units this <see cref="IWeapon" />
        ///     can reach. Not all weapons can have the same reach.
        /// </summary>
        int Reach { get; set; }

        /// <summary>
        ///     Specifies how this <see cref="IWeapon" />
        ///     shoots.
        /// </summary>
        void Shoot();
    }
}