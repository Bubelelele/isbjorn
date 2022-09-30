using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ambiance Block Set")]
public class AmbianceBlockSetSO : ScriptableObject {

    [SerializeField] private AmbianceBlock[] songBlocks;
    [SerializeField] private AmbianceBlock[] chordBlocks;

    public List<AmbianceBlock> Songs { get; private set; }
    public List<AmbianceBlock> Chords { get; private set; }

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
