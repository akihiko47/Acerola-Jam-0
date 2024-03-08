using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {

    public static ScenesManager Instance;

    [SerializeField]
    private Animator transition;

    [SerializeField]
    private float transitionTime = 1f;

    public enum Scene {
        MainMenu,
        Intro,
        MainScene,
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void LoadScene(Scene scene) {
        StartCoroutine(SceneTransition(scene));
    }

    private IEnumerator SceneTransition(Scene scene) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadNextScene() {
        LoadScene((Scene)(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadMainMenu() {
        LoadScene(Scene.MainMenu);
    }
}
