using System;
using UnityEngine;

namespace InteractionSystem.Core
{
    /// <summary>
    /// Immutable interaction context passed to interactables.
    /// Keeps interactables decoupled from concrete player implementations.
    /// </summary>
    public readonly struct InteractionContext
    {
        #region Private Fields
        private readonly Transform m_InteractorTransform;
        private readonly float m_DeltaTime;
        private readonly bool m_WasPressedThisFrame;
        private readonly bool m_IsHeld;
        private readonly bool m_WasReleasedThisFrame;
        #endregion

        #region Public Properties
        /// <summary>
        /// The interactor (usually the player) transform.
        /// </summary>
        public Transform InteractorTransform => m_InteractorTransform;

        /// <summary>
        /// Delta time for time-based interactions (e.g., hold progress).
        /// </summary>
        public float DeltaTime => m_DeltaTime;

        /// <summary>
        /// Indicates whether the interaction input was pressed this frame.
        /// </summary>
        public bool WasPressedThisFrame => m_WasPressedThisFrame;

        /// <summary>
        /// Indicates whether the interaction input is currently held.
        /// </summary>
        public bool IsHeld => m_IsHeld;

        /// <summary>
        /// Indicates whether the interaction input was released this frame.
        /// </summary>
        public bool WasReleasedThisFrame => m_WasReleasedThisFrame;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new interaction context.
        /// </summary>
        /// <param name="interactorTransform">Interactor transform (e.g., player).</param>
        /// <param name="deltaTime">Delta time for progress-based interactions.</param>
        /// <param name="wasPressedThisFrame">True if input pressed this frame.</param>
        /// <param name="isHeld">True if input is currently held.</param>
        /// <param name="wasReleasedThisFrame">True if input released this frame.</param>
        /// <exception cref="ArgumentNullException">Thrown if interactorTransform is null.</exception>
        public InteractionContext(
            Transform interactorTransform,
            float deltaTime,
            bool wasPressedThisFrame,
            bool isHeld,
            bool wasReleasedThisFrame)
        {
            if (interactorTransform == null)
            {
                throw new ArgumentNullException(nameof(interactorTransform));
            }

            m_InteractorTransform = interactorTransform;
            m_DeltaTime = deltaTime;
            m_WasPressedThisFrame = wasPressedThisFrame;
            m_IsHeld = isHeld;
            m_WasReleasedThisFrame = wasReleasedThisFrame;
        }
        #endregion
    }
}
