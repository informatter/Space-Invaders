namespace Assets.Scripts.SpaceInvaders.Core.Interfaces
{
    /// <summary>
    ///     A contract which contains the behaviour
    ///     for all <see cref="IEnemyShip" />'s
    ///     for this game.
    /// </summary>
    public interface IEnemyShip : IEnemy
    {
        /// <summary>
        /// A class responsible
        /// for analyzing the field of view of a
        /// <see cref="IEnemyShip"/>, and determining if a
        /// <see cref="IPlayerShip"/> is visible or not.
        /// </summary>
        EnemyShipViewAnalyzer EnemyShipViewAnalyzer { get; set; }

        /// <summary>
        ///     Determines the behavior how this <see cref="IEnemyShip" />
        ///     flies.
        /// </summary>
        void Fly(LineOfSightResults lineOfSightResults);

        /// <summary>
        ///     Determines the behaviour of how this <see cref="IEnemyShip" />
        ///     should shoot.
        /// </summary>
        void Shoot(LineOfSightResults lineOfSightResults);
    }
}