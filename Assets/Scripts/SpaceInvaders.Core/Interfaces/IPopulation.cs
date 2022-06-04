using System.Collections.Generic;

namespace Assets.Scripts.SpaceInvaders.Core.Interfaces
{
    public interface IPopulation<out T> : IEnumerable<T>
    {
        //List<GameObject> Prefabs { get; set; }

        ///// <summary>
        /////     The total number of meteorites in the gae.
        ///// </summary>
        //int TotalEnemies { get; set; }
    }
}
