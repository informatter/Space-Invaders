using System.Collections.Generic;
using Assets.Scripts.Behaviours;
using Assets.Scripts.SpaceInvaders.Core;
using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Populations
{
    /// <summary>
    ///     This <see cref="MeteoritePopulation" /> stores all the
    ///     <see cref="Meteorite" />'s in the game.
    /// </summary>
    public class MeteoritePopulation : MonoBehaviour //, IPopulation<IMeteorite> //IEnumerable<IMeteorite>
    {
        //private IList<IMeteorite> _meteorites;
        private float _size;

        /// <summary>
        ///     The total number of meteorites in the gae.
        /// </summary>
        public int Number;

        /// <summary>
        ///     A collection of all meteorite prefabs.
        /// </summary>
        public List<GameObject> Prefabs;

        /// <summary>
        ///     A radius which describes the are
        ///     where the meteorites will be places in.
        /// </summary>
        public int WorldSize;

        //public IEnumerator<IMeteorite> GetEnumerator()
        //{
        //    return _meteorites.GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return this.GetEnumerator();
        //}

        public void Start()
        {
          //  _meteorites = new List<IMeteorite>();

            this.Create();
        }

        /// <summary>
        ///     Instantiates all <see cref="Meteorite" />'s in the game.
        /// </summary>
        private void Create()
        {
            int totalPrefabs = Prefabs.Count;

            for (int i = 0; i < Number; i++)
            {
                var position = Random.insideUnitSphere * WorldSize;

                var prefab = Prefabs[Random.Range(0, totalPrefabs - 1)];

                var gameObject = Instantiate(prefab, position, Quaternion.identity);

                var meteorite = gameObject.GetComponent<IMeteorite>();

                meteorite.Exploded += this.Meteorite_Exploded;

            }
        }

        /// <summary>
        ///     An event handler for all <see cref="Meteorite" /> explosions.
        /// </summary>
        private void Meteorite_Exploded(object sender, ExplosionData explosionData)
        {
            var explosion = Instantiate(explosionData.ExplosionPrefab, explosionData.HitPoint, Quaternion.identity);

            var particleSystem = explosion.GetComponent<ParticleSystem>();

            Destroy(explosion, particleSystem.main.duration);

            Destroy(explosionData.ExplosionPrefab, particleSystem.main.duration);
        }
    }
}