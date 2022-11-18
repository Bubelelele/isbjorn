using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private PlayableDirector playableDirector;

    private void OnEnable() {
        playableDirector.stopped += LoadScene;
    }

    private void OnDisable() {
        playableDirector.stopped -= LoadScene;
    }

    public void Play() {
        PlayerPrefs.DeleteKey("Cutscene Done");
        PlayerPrefs.Save();
        playableDirector.Play();
    }

    public void Quit() {
        Application.Quit();
    }

    private void LoadScene(PlayableDirector director) {
        SceneManager.LoadScene("Cycle 1");
    }

}
