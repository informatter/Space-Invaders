using Assets.Scripts.SpaceInvaders.Core.Interfaces;
using UnityEngine;

/// <summary>
///     This <see cref="PlayerShipMove" /> depicts the behaviour movement of the
///     player space ship fighter.
/// </summary>
public class PlayerShipMove : IPlayerShipMove
{
    private readonly Transform _transform;
    private float _maxSpeed;
    private float _maxSteerSpeed;

    /// <summary>
    ///     Determines the maximum speed this  <see cref="IPlayerShipMove" />
    ///     can steer.
    ///     values which are smaller than zero will be clamped to zero.
    /// </summary>
    public float MaxSteerSpeed
    {
        get => _maxSteerSpeed;

        set
        {
            if (value < 0)
                _maxSteerSpeed = 0;

            _maxSteerSpeed = value;
        }
    }

    /// <summary>
    ///     The maximum speed this <see cref="PlayerShipMove" /> will move.
    ///     values which are smaller than zero will be clamped to zero.
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
    ///     Construct a new <see cref="PlayerShipMove" />.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="maxSpeed"></param>
    public PlayerShipMove(Transform transform, float maxSpeed, float maxSteerSpeed)
    {
        _transform = transform;

        this.MaxSpeed = maxSpeed;

        this.MaxSteerSpeed = maxSteerSpeed; // TODO: I think this should be a public game setting.
    }


    /// <summary>
    ///     Determines how a <see cref="IPlayerShip" />
    ///     moves forward.
    /// </summary>
    public void Propulsion()
    {
        var delta = _transform.forward * (Time.deltaTime * this.MaxSpeed * Input.GetAxis("Vertical"));

        _transform.position += delta;
    }

    /// <summary>
    ///     Determines how a <see cref="IPlayerShip" />
    ///     navigates and steers.
    /// </summary>
    public void Steer()
    {
        float yaw = Time.deltaTime * Input.GetAxis("Horizontal") * this.MaxSteerSpeed;

        float pitch = Time.deltaTime * Input.GetAxis("PlayerShipPitch") * this.MaxSteerSpeed;

        float roll = Time.deltaTime * Input.GetAxis("PlayerShipRoll") * this.MaxSteerSpeed;

        _transform.Rotate(pitch, yaw, roll);

        //float rotX = Input.GetAxis("Mouse X") * 500f * Time.deltaTime;

        //float rotY = Input.GetAxis("Mouse Y") * 100f * Time.deltaTime;

        //_transform.Rotate(_transform.up* rotX);
    }
}