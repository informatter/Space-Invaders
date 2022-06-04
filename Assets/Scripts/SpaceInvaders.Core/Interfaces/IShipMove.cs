namespace Assets.Scripts.SpaceInvaders.Core
{
    /// <summary>
    ///     A contract for the movement of all space ships
    ///     in the 3D Space Invadors Game.
    /// </summary>
    public interface IShipMove
    {
        float MaxSpeed { get; set; }

        /// <summary>
        ///     Determines how a <see cref="IPlayerSpaceShip" />
        ///     moves forward.
        /// </summary>
        void Propulsion();

        /// <summary>
        ///     Determines how a <see cref="IPlayerSpaceShip" />
        ///     navigates and steers.
        /// </summary>
        void Steer();
    }
}