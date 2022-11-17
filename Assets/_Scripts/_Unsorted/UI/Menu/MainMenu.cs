using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private PlayableDirector playableDirector;

    private void Start() {
        playableDirector.stopped += LoadScene;
    }

    public void Play() {
        playableDirector.Play();
    }

    public void Quit() {
        Application.Quit();
    }

    private void LoadScene(PlayableDirector director) {
        playableDirector.stopped -= LoadScene;
        SceneManager.LoadScene("Cycle 1");
    }

}
