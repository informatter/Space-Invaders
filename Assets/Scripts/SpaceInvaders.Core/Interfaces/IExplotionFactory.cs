namespace Assets.Scripts.SpaceInvaders.Core.Interfaces
{
    /// <summary>
    /// A contract to control the creation and deletion
    /// of explosion prefabs in the game.
    /// </summary>
    public interface IExplosionController
    {
        /// <summary>
        ///     Creates a spark effect in the <see cref="RayCastShootResult.RayCastHit" />
        ///     location where a <see cref="IWeapon" /> shot.
        /// </summary>
        /// <param name="shootResult"></param>
        void Create(RayCastShootResult shootResult);
    }
}
