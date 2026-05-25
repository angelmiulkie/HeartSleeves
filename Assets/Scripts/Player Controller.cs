using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; 

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;

    // Interactables
    [SerializeField] private GameObject ePromptSprite; // E Sprite Object
    private GameObject currentInteractable;

    // Scene Switch
    // [SerializeField] private string sceneToLoad;
    private bool canInteract = false; // This tracks if the player is in the interact bubble
    [SerializeField] Animator transitionAnim;

    [SerializeField] private int sceneToLoad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity= moveInput * moveSpeed;

        if (canInteract && Keyboard.current.eKey.wasPressedThisFrame) {
            InteractAndSwitchScene();
        }
    }

    public void Move(InputAction.CallbackContext context) {
        if (!enabled) {
            moveInput = Vector2.zero;
            if (rb != null) {
                rb.linearVelocity = Vector2.zero;
            }
            if (animator != null) {
                animator.SetBool("isWalking", false);
            }
            return;
        }

        moveInput = context.ReadValue<Vector2>();

        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);

        if (moveInput != Vector2.zero) {
            animator.SetBool("isWalking", true);
        } else if (context.canceled) {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
    }

    private void OnDisable() {
        if (rb != null) {
            rb.linearVelocity = Vector2.zero;
        }

        if (animator != null) {
            animator.SetBool("isWalking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Interactable")) {
            currentInteractable = other.gameObject;
            canInteract = true;
            ePromptSprite.SetActive(true); // This shows the "E"
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Interactable") && other.gameObject == currentInteractable) {
            currentInteractable = null;
            canInteract = false;
            ePromptSprite.SetActive(false); // This hides the "E"
        }
    }

    private void InteractAndSwitchScene() {
        if (ePromptSprite != null) {
            ePromptSprite.SetActive(false);
        }

       StartCoroutine(PlaySceneRoutine());
    }

    private IEnumerator PlaySceneRoutine() {
        if (transitionAnim != null) {
            transitionAnim.SetTrigger("End");
        }

        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
    
}
