using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class AIDialogue : MonoBehaviour
{
    [System.Serializable]
    public class DialogueSet
    {
        [TextArea(10, 10)]
        public string[] lines;
    }

    [Header("UI")]
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;

    [Header("Dialogue")]
    public DialogueSet[] interactions;
    private int interactionIndex = 0;

    [Header("Timing")]
    public float secondsPerLine  = 2.5f;
    public float typewriterSpeed = 0.04f;

    private Action onCompleteCallback;
    private Coroutine dialogueCoroutine;

    public bool HasDialogueRemaining() => interactionIndex < interactions.Length;

    public void StartDialogue(Action onComplete)
    {
        if (!HasDialogueRemaining()) return;

        onCompleteCallback = onComplete;

        if (dialogueCoroutine != null)
            StopCoroutine(dialogueCoroutine);

        dialogueCoroutine = StartCoroutine(RunDialogue());
    }

    IEnumerator RunDialogue()
    {
        if (dialoguePanel) dialoguePanel.SetActive(true);

        foreach (string line in interactions[interactionIndex].lines)
        {
            if (typewriterSpeed > 0f)
                yield return StartCoroutine(TypeLine(line));
            else
                if (dialogueText) dialogueText.text = line;

            yield return new WaitForSeconds(secondsPerLine);
        }

        interactionIndex++;
        EndDialogue();
    }

    IEnumerator TypeLine(string line)
    {
        if (!dialogueText) yield break;

        dialogueText.text = "";
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typewriterSpeed);
        }
    }

    void EndDialogue()
    {
        if (dialogueText) dialogueText.text = "";
        if (dialoguePanel) dialoguePanel.SetActive(false);

        onCompleteCallback?.Invoke();
    }

    public void SkipDialogue()
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
            dialogueCoroutine = null;
        }
        EndDialogue();
    }
}