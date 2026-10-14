using UnityEngine;
using TMPro;

public class PlayerInteractionDetector : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI interactPrompt;

    [Header("Settings")]
    public string npcTag = "NPC";

    private AIBehavior nearbyAI = null;

    void Update()
    {
        if (nearbyAI == null || !nearbyAI.IsInteractable()) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            nearbyAI.Attack();
            SetPrompt(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(npcTag)) return;

        var ai = other.GetComponent<AIBehavior>();
        if (ai == null) return;

        nearbyAI = ai;

        if (nearbyAI.IsInteractable())
            SetPrompt(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(npcTag)) return;

        var ai = other.GetComponent<AIBehavior>();
        if (ai == nearbyAI)
        {
            nearbyAI = null;
            SetPrompt(false);
        }
    }

    void LateUpdate()
    {
        if (nearbyAI == null) return;

        bool shouldShow = nearbyAI.IsInteractable();
        if (interactPrompt && interactPrompt.gameObject.activeSelf != shouldShow)
            SetPrompt(shouldShow);
    }

    void SetPrompt(bool visible)
    {
        if (interactPrompt)
            interactPrompt.gameObject.SetActive(visible);
    }
}