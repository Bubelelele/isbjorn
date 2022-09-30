using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AmbianceController))]
public class AmbianceControllerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        AmbianceController ambianceController = (AmbianceController) target;

        if (GUILayout.Button("Toggle Mute Wind")) {
            ambianceController.ToggleMuteWind();
        } else if (GUILayout.Button("Toggle Mute Piano")) {
            ambianceController.ToggleMutePiano();
        } else if (GUILayout.Button("Mute All")) {
            ambianceController.MuteAll();
        } else if (GUILayout.Button("Reset Interrupt Timer")) {
            ambianceController.ResetInterruptTimer();
        } else if (GUILayout.Button("Set Song Ready")) {
            ambianceController.SetSongReady();
        } else if (GUILayout.Button("Set Chord Ready")) {
            ambianceController.SetChordReady();
        }
    }
}
