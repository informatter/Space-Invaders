namespace Assets.Scripts.SpaceInvaders.Core.Interfaces
{
    /// <summary>
    ///     A contract which determines the movement
    ///     specifically for any player space ship.
    /// </summary>
    public interface IPlayerShipMove : IShipMove
    {
        /// <summary>
        ///     Determines the maximum speed this  <see cref="IPlayerShipMove" />
        ///     can steer.
        /// </summary>
        float MaxSteerSpeed { get; set; }
    }
}