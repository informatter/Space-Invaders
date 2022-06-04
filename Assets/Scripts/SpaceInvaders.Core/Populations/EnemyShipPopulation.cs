using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SpaceInvaders.Core;
using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Populations
{
    public class EnemyShipPopulation : MonoBehaviour, IPopulation<IEnemyShip>
    {
        private IList<IEnemyShip> _enemyShips;
        private float _size;
        private int _totalCreated;
        private int _frameCount;
        private bool _created;

        //public List<GameObject> Prefabs { get; set; } // Does not appear in editor...

        public List<GameObject> Prefabs;

        public int TotalEnemies;

        public int WorldSize;

        /// <summary>
        ///     The total number of meteorites in the gae.
        /// </summary>
        //public int TotalEnemies { get; set; } // Does not appear in editor...

        /// <summary>
        ///     A radius which describes the are
        ///     where the meteorites will be places in.
        /// </summary>
        // public int WorldSize { get; set; } // Does not appear in editor...
        public void Start()
        {
            _enemyShips = new List<IEnemyShip>();

            _frameCount = 0;

            _totalCreated = 0;

            _created = false;
        }

        public void Update()
        {
            _frameCount++;

            if (_totalCreated == TotalEnemies) return;

            if (_frameCount >= 500)
            {
                _frameCount = 0;

                _created = false;
            }

            if (_created == false)
            {
                int totalPrefabs = Prefabs.Count;

                var origin = Random.onUnitSphere * WorldSize;

                var prefab = Prefabs[Random.Range(0, totalPrefabs - 1)];

                var gameObject = Instantiate(prefab, origin, Random.rotation);

                var enemyShip = gameObject.GetComponent<IEnemyShip>();

                enemyShip.Exploded += this.EnemyShip_Exploded;

                _totalCreated++;

                _created = true;
            }
        }

        private void EnemyShip_Exploded(object sender, ExplosionData explosionData)
        {
            var explosion = Instantiate(explosionData.ExplosionPrefab, explosionData.HitPoint, Quaternion.identity);

            var particleSystem = explosion.GetComponent<ParticleSystem>();

            Destroy(explosion, particleSystem.main.duration);

            Destroy(explosionData.ExplosionPrefab, particleSystem.main.duration);
        }

        public IEnumerator<IEnemyShip> GetEnumerator()
        {
            return _enemyShips.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}