namespace Assets.Scripts.SpaceInvaders.Core.Interfaces
{
    /// <summary>
    ///     A contract which describes how an enemy in this
    ///     game should move.
    /// </summary>
    public interface IEnemyMove
    {
        /// <summary>
        ///     The maximum speed which controls the movement.
        /// </summary>
        float MaxSpeed { get; set; }

        /// <summary>
        ///     The amount of damp applied to the rotation.
        ///     smaller values have a larger damping effect.
        /// </summary>
        float RotationDamp { get; set; }

        /// <summary>
        ///     A value in degrees which determines factor
        ///     by which this <see cref="IEnemy" /> can spot
        ///     a player.
        /// </summary>
        float SightThreshold { get; set; }

        /// <summary>
        ///     Follows a player in the game with the goal
        ///     to make some damage to it.
        /// </summary>
        void Follow();

        bool AvoidObstacle();

        /// <summary>
        ///     Determines how an enemy in the
        ///     game should move.
        /// </summary>
        void Move();
    }
}