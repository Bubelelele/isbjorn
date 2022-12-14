using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportMenu : MonoBehaviour {
    
    [SerializeField] private Transform[] teleportLocations;

    private void Start() {
        if (PlayerPrefs.HasKey("Section")) {
            Teleport(PlayerPrefs.GetInt("Section", 1));
            PlayerPrefs.DeleteKey("Section");
            PlayerPrefs.Save();
        }
    }

    public void RequestTeleport(int section) {
        PlayerPrefs.SetInt("Section", section);
        PlayerPrefs.Save();
        Invoke(nameof(DelayedSceneLoad), .5f);
    }

    private void DelayedSceneLoad() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    private void Teleport(int section) {
        PlayerStateMachine.StaticPlayerTransform.position = teleportLocations[section - 1].position;
        PlayerStateMachine.StaticPlayerTransform.GetChild(0).rotation = teleportLocations[section - 1].rotation;
    }
}