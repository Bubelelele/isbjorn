using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject teleportMenuUI;
    
    private bool _gamePaused;

    private void Update() {
        if (Input.GetKeyDown(pauseKey)) {
            if (_gamePaused) Resume();
            else Pause();
        }
    }

    private void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _gamePaused = true;
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        teleportMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _gamePaused = false;
    }

    public void RestartScene() {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit() => Application.Quit();
}