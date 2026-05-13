using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;

    private bool spacePressed;

    private TypewriterEffect typewriterEffect;

    private void Start() {
        typewriterEffect = GetComponent<TypewriterEffect>();
        ShowDialogue(testDialogue);
    }

    private void Update() {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) {
            spacePressed = true;
        }
    }

    public void ShowDialogue(DialogueObject dialogueObject) {
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject) {
        yield return new WaitForSeconds(1);

        foreach (string dialogue in dialogueObject.Dialogue) {
            yield return typewriterEffect.Run(dialogue, textLabel); 
            spacePressed = false;
            yield return new WaitUntil(() => spacePressed);
        }
    }
}
