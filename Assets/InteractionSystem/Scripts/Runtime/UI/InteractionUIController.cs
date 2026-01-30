using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InteractionSystem.Core;
using InteractionSystem.Player;

namespace InteractionSystem.UI
{
    /// <summary>
    /// Handles interaction-related UI feedback such as prompts,
    /// failure messages, and hold progress visualization.
    /// </summary>
    public class InteractionUIController : MonoBehaviour
    {
        #region Private Fields
        [SerializeField]
        [Tooltip("Reference to the player's interaction detector.")]
        private InteractionDetector m_InteractionDetector;

        [SerializeField]
        [Tooltip("Text element used to display interaction prompts and messages.")]
        private TMP_Text m_PromptText;

        [SerializeField]
        [Tooltip("UI image used to display hold interaction progress.")]
        private Image m_HoldProgressBar;

        [SerializeField]
        [Tooltip("Duration (in seconds) that failure messages remain visible.")]
        private float m_FailMessageDuration = 1.5f;

        private IInteractable m_LastInteractable;
        private float m_FailMessageTimer;
        #endregion

        #region Unity Lifecycle
        private void Awake()
        {
            if (m_PromptText != null)
            {
                m_PromptText.gameObject.SetActive(false);
            }

            if (m_HoldProgressBar != null)
            {
                m_HoldProgressBar.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            UpdatePrompt();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Displays a temporary failure message such as
        /// "Key Required" or "Cannot Interact".
        /// </summary>
        public void ShowFailMessage(string message)
        {
            if (string.IsNullOrEmpty(message) || m_PromptText == null)
            {
                return;
            }

            m_PromptText.text = message;
            m_PromptText.gameObject.SetActive(true);
            m_FailMessageTimer = m_FailMessageDuration;
        }

        /// <summary>
        /// Updates the hold interaction progress bar.
        /// </summary>
        /// <param name="progress01">Normalized progress value (0–1).</param>
        public void UpdateHoldProgress(float progress01)
        {
            if (m_HoldProgressBar == null)
            {
                return;
            }

            m_HoldProgressBar.gameObject.SetActive(true);
            m_HoldProgressBar.fillAmount = Mathf.Clamp01(progress01);
        }
        #endregion

        #region Private Methods
        private void UpdatePrompt()
        {
            if (m_FailMessageTimer > 0f)
            {
                m_FailMessageTimer -= Time.deltaTime;

                if (m_FailMessageTimer <= 0f && m_PromptText != null)
                {
                    m_PromptText.gameObject.SetActive(false);
                }

                return;
            }

            IInteractable currentInteractable = m_InteractionDetector.CurrentInteractable;

            if (currentInteractable == null)
            {
                m_LastInteractable = null;

                if (m_PromptText != null)
                {
                    m_PromptText.gameObject.SetActive(false);
                }

                if (m_HoldProgressBar != null)
                {
                    m_HoldProgressBar.gameObject.SetActive(false);
                }

                return;
            }

            if (currentInteractable == m_LastInteractable)
            {
                return;
            }

            m_LastInteractable = currentInteractable;

            string prompt = currentInteractable.GetInteractionPrompt(default);

            if (string.IsNullOrEmpty(prompt) || m_PromptText == null)
            {
                return;
            }

            m_PromptText.text = prompt;
            m_PromptText.gameObject.SetActive(true);
        }
        #endregion
    }
}
