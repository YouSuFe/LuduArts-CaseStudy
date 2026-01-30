using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InteractionSystem.Core;
using InteractionSystem.Player;
using InteractionSystem.Interactables;

namespace InteractionSystem.UI
{
    /// <summary>
    /// Handles interaction-related UI feedback such as
    /// state prompts, failure messages, and hold progress visualization.
    /// </summary>
    public class InteractionUIController : MonoBehaviour
    {
        #region Private Fields
        [SerializeField]
        [Tooltip("Reference to the player's interaction detector.")]
        private InteractionDetector m_InteractionDetector;

        [SerializeField]
        [Tooltip("Text element used to display interaction state prompts.")]
        private TMP_Text m_StatePromptText;

        [SerializeField]
        [Tooltip("Text element used to display temporary failure messages.")]
        private TMP_Text m_FailMessageText;

        [SerializeField]
        [Tooltip("UI image used to display hold interaction progress.")]
        private Image m_HoldProgressBar;

        [SerializeField]
        [Tooltip("Duration (in seconds) that failure messages remain visible.")]
        private float m_FailMessageDuration = 1.5f;

        private float m_FailMessageTimer;
        #endregion

        #region Unity Lifecycle
        private void Awake()
        {
            if (m_StatePromptText != null)
            {
                m_StatePromptText.gameObject.SetActive(false);
            }

            if (m_FailMessageText != null)
            {
                m_FailMessageText.gameObject.SetActive(false);
            }

            if (m_HoldProgressBar != null)
            {
                m_HoldProgressBar.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            UpdateFailMessageTimer();
            UpdateStatePrompt();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Displays a temporary failure message such as
        /// "Key Required" or "Use the switch".
        /// </summary>
        public void ShowFailMessage(string message)
        {
            if (string.IsNullOrEmpty(message) || m_FailMessageText == null)
            {
                return;
            }

            m_FailMessageText.text = message;
            m_FailMessageText.gameObject.SetActive(true);
            m_FailMessageTimer = m_FailMessageDuration;

            if (m_StatePromptText != null)
            {
                m_StatePromptText.gameObject.SetActive(false);
            }
        }
        #endregion

        #region Private Methods
        private void UpdateFailMessageTimer()
        {
            if (m_FailMessageTimer <= 0f)
            {
                return;
            }

            m_FailMessageTimer -= Time.deltaTime;

            if (m_FailMessageTimer <= 0f && m_FailMessageText != null)
            {
                m_FailMessageText.gameObject.SetActive(false);
            }
        }

        private void UpdateStatePrompt()
        {
            // Fail message active → state prompt hidden
            if (m_FailMessageTimer > 0f)
            {
                return;
            }

            IInteractable currentInteractable = m_InteractionDetector.CurrentInteractable;

            if (currentInteractable == null)
            {
                HideAll();
                return;
            }

            // --- STATE PROMPT ---
            string prompt = currentInteractable.GetInteractionPrompt(default);

            if (string.IsNullOrEmpty(prompt))
            {
                m_StatePromptText.gameObject.SetActive(false);
            }
            else
            {
                m_StatePromptText.text = prompt;
                m_StatePromptText.gameObject.SetActive(true);
            }

            // --- HOLD PROGRESS ---
            if (currentInteractable is ChestInteractable chest)
            {
                float progress = chest.CurrentHoldTime / chest.HoldDuration;
                m_HoldProgressBar.fillAmount = Mathf.Clamp01(progress);
                m_HoldProgressBar.gameObject.SetActive(!chest.IsOpened);
            }
            else
            {
                m_HoldProgressBar.gameObject.SetActive(false);
            }
        }

        private void HideAll()
        {
            if (m_StatePromptText != null)
            {
                m_StatePromptText.gameObject.SetActive(false);
            }

            if (m_HoldProgressBar != null)
            {
                m_HoldProgressBar.gameObject.SetActive(false);
            }
        }
        #endregion
    }
}
