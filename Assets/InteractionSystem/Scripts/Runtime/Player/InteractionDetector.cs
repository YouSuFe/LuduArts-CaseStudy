using UnityEngine;
using InteractionSystem.Core;

namespace InteractionSystem.Player
{
    /// <summary>
    /// Detects nearby interactable objects using a throttled overlap sphere
    /// and selects the closest valid interactable.
    /// </summary>
    public class InteractionDetector : MonoBehaviour
    {
        #region Constants
        private const int k_MaxColliders = 16;
        #endregion

        #region Private Fields
        [SerializeField]
        [Tooltip("Radius used to detect nearby interactable objects.")]
        private float m_DetectionRadius = 2.0f;

        [SerializeField]
        [Tooltip("Time interval (in seconds) between detection checks.")]
        private float m_DetectionInterval = 0.2f;

        [SerializeField]
        [Tooltip("Layer mask used to filter interactable objects.")]
        private LayerMask m_InteractableLayerMask;

        private readonly Collider[] m_OverlapResults = new Collider[k_MaxColliders];

        private IInteractable m_CurrentInteractable;
        private float m_NextDetectionTime;
        #endregion

        #region Public Properties
        /// <summary>
        /// Currently detected closest interactable, or null if none found.
        /// </summary>
        public IInteractable CurrentInteractable => m_CurrentInteractable;
        #endregion

        #region Unity Lifecycle
        private void Update()
        {
            if (Time.time < m_NextDetectionTime)
            {
                return;
            }

            m_NextDetectionTime = Time.time + m_DetectionInterval;
            DetectInteractables();
        }
        #endregion

        #region Private Methods
        private void DetectInteractables()
        {
            int hitCount = Physics.OverlapSphereNonAlloc(
                transform.position,
                m_DetectionRadius,
                m_OverlapResults,
                m_InteractableLayerMask);

            IInteractable closestInteractable = null;
            float closestSqrDistance = float.MaxValue;

            for (int i = 0; i < hitCount; i++)
            {
                Collider hitCollider = m_OverlapResults[i];

                if (!hitCollider.TryGetComponent(out IInteractable interactable))
                {
                    continue;
                }

                Transform interactionPoint = interactable.InteractionPoint != null
                    ? interactable.InteractionPoint
                    : hitCollider.transform;

                float sqrDistance =
                    (interactionPoint.position - transform.position).sqrMagnitude;

                float maxRange = interactable.MaxInteractionRange;
                if (sqrDistance > maxRange * maxRange)
                {
                    continue;
                }

                if (sqrDistance < closestSqrDistance)
                {
                    closestSqrDistance = sqrDistance;
                    closestInteractable = interactable;
                }
            }

            m_CurrentInteractable = closestInteractable;
        }
        #endregion

#if UNITY_EDITOR
        #region Editor Only
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, m_DetectionRadius);
        }
        #endregion
#endif
    }
}
