using System;
using Assets.Scripts.SpaceInvaders.Core;
using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Behaviours
{
    public class Meteorite : MonoBehaviour, IMeteorite
    {
        [SerializeField] private GameObject _explosionPrefab;

        private float _rotX, _rotY;

        private Transform _transform;

        /// <summary>
        ///     Occurs when this <see cref="Meteorite" />'s
        ///     health reaches zero;
        /// </summary>
        public event EventHandler<ExplosionData> Exploded;

        /// <summary>
        ///     The amount of damage this <see cref="Meteorite" />
        ///     does when the player crashes in to it.
        ///     This value should change as levels increase.
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        ///     The total health this <see cref="IGameEntity" />
        ///     has.
        /// </summary>
        public int Health { get; set; }


        public void Update()
        {
            if (_transform == null) return;

            if (this.Health <= 0)
            {
                var explosionData = new ExplosionData(_explosionPrefab, _transform.transform.position);



                this.OnExplosion(this, explosionData);

                _explosionPrefab.SetActive(false);

                return;
            }

            this.Rotation();
        }

        public void Start()
        {
            _transform = this.transform;

            _explosionPrefab.SetActive(false);

            this.Damage = 10;

            var random = new Random();

            _rotX = random.Next(3, 15);

            _rotY = random.Next(5, 20);

            int scale = random.Next(1, 8);

            this.Health = scale * scale * scale;

            _transform.localScale = new Vector3(scale, scale, scale);
        }

        private void Rotation()
        {
            _transform.Rotate(_rotX * Time.deltaTime, _rotY * Time.deltaTime, 0);
        }

        private void OnExplosion(object sender, ExplosionData explosionData)
        {
            _explosionPrefab.SetActive(true);

            var handler = this.Exploded;

            handler?.Invoke(sender, explosionData);
        }
    }
}