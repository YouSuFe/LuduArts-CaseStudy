using UnityEngine;

namespace InteractionSystem.Core
{
    /// <summary>
    /// Defines a contract for all interactable objects in the world.
    /// Implementations decide whether they can be interacted with
    /// and how the interaction lifecycle is handled.
    /// </summary>
    public interface IInteractable
    {
        #region Public Properties
        /// <summary>
        /// World-space point used for interaction checks and UI positioning.
        /// </summary>
        Transform InteractionPoint { get; }

        /// <summary>
        /// Maximum distance at which this object can be interacted with.
        /// </summary>
        float MaxInteractionRange { get; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Determines whether interaction is currently allowed.
        /// </summary>
        /// <param name="context">Interaction context.</param>
        /// <param name="failReason">
        /// Reason why interaction is not allowed (used for UI feedback).
        /// Should be null or empty if interaction is allowed.
        /// </param>
        /// <returns>True if interaction is allowed.</returns>
        bool CanInteract(in InteractionContext context, out string failReason);

        /// <summary>
        /// Returns prompt data used by the UI (e.g. "Press E to Open").
        /// </summary>
        /// <param name="context">Interaction context.</param>
        /// <returns>Prompt string to display.</returns>
        string GetInteractionPrompt(in InteractionContext context);

        /// <summary>
        /// Called when interaction begins (press or hold start).
        /// </summary>
        /// <param name="context">Interaction context.</param>
        void BeginInteraction(in InteractionContext context);

        /// <summary>
        /// Called every frame while interaction is ongoing (optional).
        /// Used mainly for hold-based interactions.
        /// </summary>
        /// <param name="context">Interaction context.</param>
        void UpdateInteraction(in InteractionContext context);

        /// <summary>
        /// Called when interaction ends (completed or cancelled).
        /// </summary>
        /// <param name="context">Interaction context.</param>
        void EndInteraction(in InteractionContext context);
        #endregion
    }
}
