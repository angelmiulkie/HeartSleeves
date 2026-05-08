using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    // Transition Variables
    [SerializeField] Animator transitionAnim;

    public void PlayGame(){
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(1);
    }
}
