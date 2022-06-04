namespace Assets.Scripts.SpaceInvaders.Core.Interfaces
{
    /// <summary>
    ///     A contract for all <see cref="IMeteorite" />
    ///     implementations in the 3D space invaders game.
    /// </summary>
    public interface IMeteorite : IGameEntity
    {
        /// <summary>
        ///     The amount of damage this <see cref="IMeteorite" />
        ///     does when the player crashes in to it.
        ///     This value should change as levels increase.
        /// </summary>
        int Damage { get; set; }

    }
}