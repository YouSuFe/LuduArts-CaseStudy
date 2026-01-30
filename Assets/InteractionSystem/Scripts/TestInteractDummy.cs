using UnityEngine;
using InteractionSystem.Core;

public class TestInteractable : MonoBehaviour, IInteractable
{
    public Transform InteractionPoint => transform;
    public float MaxInteractionRange => 5f;

    public bool CanInteract(in InteractionContext context, out string failReason)
    {
        failReason = null;
        return true;
    }

    public string GetInteractionPrompt(in InteractionContext context)
    {
        return "Test";
    }

    public void BeginInteraction(in InteractionContext context)
    {
        Debug.Log("BeginInteraction");
    }

    public void UpdateInteraction(in InteractionContext context) { }

    public void EndInteraction(in InteractionContext context)
    {
        Debug.Log("EndInteraction");
    }
}
