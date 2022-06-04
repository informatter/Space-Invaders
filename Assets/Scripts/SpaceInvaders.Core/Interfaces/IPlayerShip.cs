namespace Assets.Scripts.SpaceInvaders.Core.Interfaces
{
    public interface IPlayerShip : IGameEntity
    {
        // TODO: Add a collection of IWeapons.

        /// <summary>
        ///     The <see cref="IWeapon" /> this <see cref="IPlayerShip" />
        ///     uses.
        /// </summary>
        IWeapon Weapon { get; set; }

        /// <summary>
        ///     Determines the behavior how this <see cref="IPlayerShip" />
        ///     flies.
        /// </summary>
        void Fly();

        /// <summary>
        ///     Determines the behaviour of how this <see cref="IPlayerShip" />
        ///     should shoot.
        /// </summary>
        void Shoot();
    }
}