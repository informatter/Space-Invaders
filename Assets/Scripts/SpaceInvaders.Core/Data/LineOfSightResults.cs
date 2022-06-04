using UnityEngine;

namespace Assets.Scripts.SpaceInvaders.Core
{
    /// <summary>
    ///     A structure which stores the results of
    ///     the line of sight computation.
    /// </summary>
    public readonly struct LineOfSightResults
    {
        /// <summary>
        ///     Determines if the <see cref="IGameEntity" />
        ///     is within the line of sight or not.
        /// </summary>
        public bool Within { get; }

        /// <summary>
        ///     The <see cref="Ray" /> that was used to
        ///     for the calculation.
        /// </summary>
        /// <remarks>
        ///     The <see cref="Ray" />'s direction is built from
        ///     a <see cref="IGameEntity" />'s position towards
        ///     a target <see cref="IGameEntity" />.
        /// </remarks>
        public Ray Ray { get; }

        public Vector3 RayTarget { get; }

        /// <summary>
        ///     Construct a new <see cref="LineOfSightResults" />.
        /// </summary>
        /// <param name="within">
        ///     Determines if the <see cref="IGameEntity" />
        ///     is within the line of sight or not.
        /// </param>
        /// <param name="ray">
        ///     The <see cref="Ray" /> that was used to
        ///     for the calculation.
        /// </param>
        public LineOfSightResults(bool within, Ray ray, Vector3 rayTarget)
        {
            this.Within = within;

            this.Ray = ray;

            this.RayTarget = rayTarget;
        }
    }
}