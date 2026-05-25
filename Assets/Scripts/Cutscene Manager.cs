using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private DialogueUI dialogueUI; 
    [SerializeField] private DialogueObject cutsceneDialogue;
    [SerializeField] private PlayerController playerControllerScript;

    private Vector2 lockedPosition;
    private bool isDialogueActive = false;

    public void Start() {
        // DisablePlayerMovement(); // Disable the players movement when the game starts
    }

    public void PauseAndTriggerDialogue() {
        if (playerControllerScript != null) {
            lockedPosition = playerControllerScript.transform.position;
        }
        DisablePlayerMovement();
        director.Pause();
        // if (playerControllerScript != null) {
            // playerControllerScript.transform.position = lockedPosition;
        // }
        isDialogueActive = true;
        dialogueUI.ShowDialogue(cutsceneDialogue);
    }

    private void LateUpdate() {
        if (isDialogueActive && playerControllerScript != null) {
            playerControllerScript.transform.position = lockedPosition;
        }
    }

    public void ResumeCutscene() {
       Debug.Log("[CutsceneManager] ResumeCutscene() was successfully called! Telling director to play.");
       isDialogueActive = false;
       EnablePlayerMovement();
       director.Play();
    }

    public void PauseCutscene() {
        director.Pause();
    }

    public void EnablePlayerMovement() {
        if (playerControllerScript != null) {
            if (playerControllerScript.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)) {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            playerControllerScript.enabled = true;
        }
    }

    public void DisablePlayerMovement() {
        if (playerControllerScript != null) {
            // Debug.Log($"[CutsceneManager] Attempting to disable: {playerControllerScript.gameObject.name}'s controller!");
            playerControllerScript.enabled = false;
            // Debug.Log($"[CutsceneManager] Verification - Is script enabled now? {playerControllerScript.enabled}");
            if (playerControllerScript.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)) {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }

    public void ChangeSceneOnDialogueEnd() {
        SceneManager.LoadSceneAsync(3);
        
    }
    
}
