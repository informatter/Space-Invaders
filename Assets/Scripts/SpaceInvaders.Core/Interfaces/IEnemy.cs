namespace Assets.Scripts.SpaceInvaders.Core.Interfaces
{
    /// <summary>
    ///     A contract which determines how
    ///     a <see cref="IEnemy" /> in this game should
    ///     behave.
    /// </summary>
    public interface IEnemy : IGameEntity
    {
        /// <summary>
        /// The <see cref="IEnemyWeapon"/> this
        /// <see cref="IEnemy"/> uses.
        /// </summary>
        IEnemyWeapon Weapon { get; set; }


        /// <summary>
        ///     A value in degrees which determines factor
        ///     by which this <see cref="IEnemy" /> can spot
        ///     a player.
        /// </summary>
        float SightThreshold { get; set; }

    }
}