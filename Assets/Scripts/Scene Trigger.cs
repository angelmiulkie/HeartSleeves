using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{

    [SerializeField] private PlayerController playerControllerScript;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private int sceneToLoad;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Player Entered!");
        if (other.CompareTag(playerTag)) {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
