using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ambiance Block Set")]
public class AmbienceBlockSetSO : ScriptableObject {

    [SerializeField] private AmbienceBlock[] songBlocks;
    [SerializeField] private AmbienceBlock[] chordBlocks;

    public List<AmbienceBlock> Songs { get; } = new();
    public List<AmbienceBlock> Chords { get; } = new();

    private void OnEnable() {
        foreach (var block in songBlocks) {
            for (int i = 0; i < block.additionalWeight+1; i++) {
                Songs.Add(block);
            }
        }
        foreach (var block in chordBlocks) {
            for (int i = 0; i < block.additionalWeight+1; i++) {
                Chords.Add(block);
            }
        }
    }
}
