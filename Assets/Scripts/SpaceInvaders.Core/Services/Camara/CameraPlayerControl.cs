using UnityEngine;

namespace Assets.Scripts.SpaceInvaders.Core
{
    public class CameraPlayerControl : MonoBehaviour
    {
        private readonly Vector3 _distance = new Vector3(0, 1.5f, -8f);

        private Transform _camaraTransform;
        [SerializeField] private Transform _playerShipFighter; // TODO: set automatically 

        private Vector3 _velocity = new Vector3(0, 0, 0);

        private readonly float CameraDistanceDamp = 0.08f;

        public void Start()
        {
            _camaraTransform = this.transform;
        }

        public void LateUpdate()
        {
            var targetPosition = _playerShipFighter.position + _playerShipFighter.rotation * _distance;

            var delta = Vector3.SmoothDamp(_camaraTransform.position, targetPosition, ref _velocity,
                CameraDistanceDamp);

            _camaraTransform.position = delta;

            _camaraTransform.LookAt(_playerShipFighter, _playerShipFighter.up);
        }
    }
}