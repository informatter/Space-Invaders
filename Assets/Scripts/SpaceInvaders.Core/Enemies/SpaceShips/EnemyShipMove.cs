using System.Collections.Generic;
using Assets.Scripts.Settings;
using Assets.Scripts.SpaceInvaders.Core.Extensions;
using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    /// <summary>
    ///     This class is responsible for the movement of
    ///     enemy space ships in the game.
    /// </summary>
    public class EnemyShipMove : IEnemyMove
    {
        private readonly Transform _enemySpaceShip;
        private readonly Transform _playerShip;
        private IEnemyMove _enemyMoveImplementation;
        private float _maxSpeed;
        private float _rotationDamp = 4f;

        /// <summary>
        ///     Construct a new <see cref="EnemyShipMove" />.
        /// </summary>
        /// <param name="playerShip">
        ///     the player <see cref="IPlayerShip" /> this <see cref="EnemyShipMove" />
        ///     should take in to account.
        /// </param>
        public EnemyShipMove(Transform playerShip, Transform enemySpaceShip, float maxSpeed, float sightThreshold)
        {
            _playerShip = playerShip;

            _enemySpaceShip = enemySpaceShip;

            this.MaxSpeed = maxSpeed;

            this.SightThreshold = sightThreshold;
        }

        /// <summary>
        ///     The maximum speed which controls the movement.
        /// </summary>
        public float MaxSpeed
        {
            get => _maxSpeed;

            set
            {
                if (value < 0)
                    _maxSpeed = 0;

                _maxSpeed = value;
            }
        }

        /// <summary>
        ///     The amount of damp applied to the rotation.
        ///     smaller values have a larger damping effect.
        /// </summary>
        public float RotationDamp
        {
            get => _rotationDamp;

            set
            {
                if (value < 0)
                    _rotationDamp = 0;

                _rotationDamp = value;
            }
        }

        /// <summary>
        ///     A value in degrees which determines factor
        ///     by which this <see cref="IEnemy" /> can spot
        ///     a player.
        /// </summary>
        public float SightThreshold { get; set; }

        /// <summary>
        ///     Follows a player in the game with the goal
        ///     to make some damage to it.
        /// </summary>
        public void Follow()
        {
            var direction = _playerShip.position - _enemySpaceShip.position;

            var delta = Quaternion.LookRotation(direction /*, _enemySpaceShip.transform.up*/);

            _enemySpaceShip.rotation =
                Quaternion.Slerp(_enemySpaceShip.rotation, delta, Time.deltaTime * _rotationDamp);
        }

        /// <summary>
        ///     Moves organically around the game
        ///     when a player is not in sight
        ///     of a <see cref="IEnemy" />
        /// </summary>
        public bool AvoidObstacle()
        {
            var vecDirA = Quaternion.AngleAxis(this.SightThreshold * 0.5f, _enemySpaceShip.transform.up) *
                          _enemySpaceShip.forward;

            var vecDirB = Quaternion.AngleAxis(-this.SightThreshold * 0.5f, _enemySpaceShip.transform.up) *
                          _enemySpaceShip.forward;

            var vecDirC = _enemySpaceShip.forward;

            var sensorRayA = new Ray(_enemySpaceShip.position, vecDirA);

            var sensorRayB = new Ray(_enemySpaceShip.position, vecDirB);

            var sensorRayC = new Ray(_enemySpaceShip.position, vecDirC);

            var sensors = new[] {sensorRayA, sensorRayB, sensorRayC};

            int totalSensors = sensors.Length;

            int hitCount = 0;

            var hitSensorsFarObstacle = new List<Ray>(totalSensors);

            var hitSensorsCloseObstacle = new List<int>(totalSensors);

            for (int i = 0; i < sensors.Length; i++)
            {
                var sensor = sensors[i];
                bool hit = Physics.Raycast(sensor, out var hitInfo);

                if (hit)
                {
                    var gameEntity = hitInfo.transform.gameObject.GetComponent<IGameEntity>();

                    // Don't avoid the player!
                    if (gameEntity is IPlayerShip || gameEntity == null) continue;

                    float rayHitMagnitude = (hitInfo.point - _enemySpaceShip.position).magnitude;

                    if (rayHitMagnitude <= InternalGameSettings.AvoidanceActivation)
                    {
                        hitCount++;

                        hitSensorsCloseObstacle.Add(i);
                    }

                    if (rayHitMagnitude > InternalGameSettings.AvoidanceActivation)
                    {
                        float crossMagnitude = Vector3.Cross(sensor.direction.normalized, _enemySpaceShip.forward)
                                                      .magnitude;

                        bool isForwardSensor = crossMagnitude == 0;

                        if (isForwardSensor)
                        {
                            this.SlerpRotate(sensor.direction);

                            return true;
                        }

                        hitSensorsFarObstacle.Add(sensor);

                        hitCount++;
                    }
                }
            }

            if (hitSensorsFarObstacle.Count == 0 && hitCount > 0)
            {
                //float record = float.MaxValue;

                //var closestSensor = Vector3.zero;

                //for (int i = 0; i < totalSensors; i++)
                //{
                //    var sensorVec = sensors[i].direction;

                //    if (hitSensorsCloseObstacle.Contains(i)) continue;

                //    float beta = Vector3.Angle(sensorVec, _enemySpaceShip.forward);

                //    if (!(beta < record)) continue;

                //    closestSensor = sensorVec;

                //    record = beta;
                //}

                var closestSensor = this.FindClosestRay(sensors, _enemySpaceShip.forward, hitSensorsCloseObstacle);

                this.SlerpRotate(closestSensor);

                return true;
            }

            if (hitSensorsFarObstacle.Count > 0 && hitCount != totalSensors && hitCount != 0)
            {
                // get closest sensor to forward dir

                //float record = float.MaxValue;

                //var closestSensor = Vector3.zero;

                //foreach (var sensor in hitSensorsFarObstacle)
                //{
                //    var sensorVec = sensor.direction;

                //    float beta = Vector3.Angle(sensorVec, _enemySpaceShip.forward);

                //    if (!(beta < record)) continue;

                //    closestSensor = sensorVec;

                //    record = beta;
                //}

                //this.SlerpRotate(closestSensor);

                var closestSensor = this.FindClosestRay(sensors, _enemySpaceShip.forward, hitSensorsCloseObstacle);

                this.SlerpRotate(closestSensor);

                return true;
            }

            // If all sensors where hit, rotate by a random vector.
            if (hitCount == totalSensors)
            {
                // var direction = Vector3.Cross(_enemySpaceShip.forward, _enemySpaceShip.transform.up);

                var direction = _enemySpaceShip.forward.VectorRotate(Random.Range(-1f, 1f));

                this.SlerpRotate(direction);

                return true;
            }

            // if no sensors where hit, keep going...
            return false;
        }

        /// <summary>
        ///     Determines how an enemy in the
        ///     game should move.
        /// </summary>
        public void Move()
        {
            var moveDirection = _enemySpaceShip.transform.forward;

            float distance = (_playerShip.position - _enemySpaceShip.position).magnitude;

            float damping = 1;

            if (distance <= 10) damping = distance * 0.1f;

            var delta = moveDirection * (Time.deltaTime * this.MaxSpeed) * damping;

            _enemySpaceShip.position += delta;
        }

        private void SlerpRotate(Vector3 direction)
        {
            var delta = Quaternion.LookRotation(direction /*, _enemySpaceShip.transform.up*/);

            _enemySpaceShip.rotation =
                Quaternion.Slerp(_enemySpaceShip.rotation, delta, Time.deltaTime * _rotationDamp);
        }

        /// <summary>
        ///     Find the ray which is most similar to the
        ///     provided <paramref name="vectorToCompareAgainst" />.
        ///     Similarity is measured by computing the angle between
        ///     the provided <paramref name="vectorToCompareAgainst" />.
        ///     and each <see cref="Ray" /> in <paramref name="rays" />.
        /// </summary>
        private Vector3 FindClosestRay(IList<Ray> rays, Vector3 vectorToCompareAgainst, IList<int> rayIndicesToIgnore)
        {
            float record = float.MaxValue;

            var closestSensor = Vector3.zero;

            for (int i = 0; i < rays.Count; i++)
            {
                if (rayIndicesToIgnore.Contains(i)) continue;

                var sensor = rays[i];

                var sensorVec = sensor.direction;

                float beta = Vector3.Angle(sensorVec, _enemySpaceShip.forward);

                if (!(beta < record)) continue;

                closestSensor = sensorVec;

                record = beta;
            }

            return closestSensor;
        }
    }
}