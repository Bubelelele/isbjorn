using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AmbienceController))]
public class AmbienceControllerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        AmbienceController ambienceController = (AmbienceController) target;

        if (GUILayout.Button("Toggle Mute Wind")) {
            ambienceController.ToggleMuteWind();
        } else if (GUILayout.Button("Toggle Mute Piano")) {
            ambienceController.ToggleMutePiano();
        } else if (GUILayout.Button("Mute All")) {
            ambienceController.MuteAll();
        } else if (GUILayout.Button("Reset Interrupt Timer")) {
            ambienceController.ResetInterruptTimer();
        } else if (GUILayout.Button("Set Song Ready")) {
            ambienceController.SetSongReady();
        } else if (GUILayout.Button("Set Chord Ready")) {
            ambienceController.SetChordReady();
        }
    }
}
