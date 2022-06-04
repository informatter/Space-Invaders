namespace Assets.Scripts.Settings
{
    /// <summary>
    /// A class which contains all the internal settings ( non-user modifiable)
    /// for the 3D space invaders game.
    /// </summary>
    internal class InternalGameSettings
    {
        internal const float ZeroTolerance = 0.001f;

        /// <summary>
        ///     A value which indicates what distance
        ///     from an obstacle is valid for a <see cref="IEnemy" />
        ///     ship to avoid it.
        /// </summary>
        internal const float AvoidanceActivation = 20f;

        /// <summary>
        ///     An integer describing the amount of FPS
        ///     which have to pass for a <see cref="IWeapon" />
        ///     to shoot.
        ///     TODO: This should be  property of the IWeapon
        /// </summary>
        internal const int EnemyShotFrecuency = 20;

        /// <summary>
        /// A value which indicates when a space ship
        /// has low health.
        /// </summary>
        internal const int ShipLowHealthThreshold = 70;
    }
}