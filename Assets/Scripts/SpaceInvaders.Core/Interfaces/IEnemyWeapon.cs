namespace Assets.Scripts.SpaceInvaders.Core.Interfaces
{
    public interface IEnemyWeapon
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
        void Shoot(LineOfSightResults lineOfSightResults);
    }
}