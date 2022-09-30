using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AmbianceController : MonoBehaviour {

    [Header("Settings")] 
    [Tooltip("How much time has to pass before a new song can play. Time chosen randomly between the values.")]
    [SerializeField] private Vector2 songDelayRange = new Vector2(60, 120);
    
    [Tooltip("How much time has to pass before a new chord can play. Time chosen randomly between the values.")]
    [SerializeField] private Vector2 chordsDelayRange = new Vector2(10, 20);
    
    [Tooltip("How long after a song/chord has stopped playing until a chord/song can interrupt.")]
    [SerializeField] private float delayGracePeriod = 2f;

    [Header("References")]
    [SerializeField] private AmbianceBlockSetSO ambianceBlockSet;
    [SerializeField] private AudioSource windSource;
    [SerializeField] private AudioSource pianoSource;

    private float _windSourceVolume;
    private float _pianoSourceVolume;
    private bool _songReady;
    private bool _chordReady;
    private float _canInterruptTime;

    private void Start() {
        StartSongDelay();
        StartChordDelay();
    }

    private void Update() {
        if (_canInterruptTime > Time.time) return;

        if (_songReady) 
            PlaySong();
        else if (_chordReady)
            PlayChord();
    }

    private void PlaySong() {
        var randomSong = GetRandomAudioClip(ambianceBlockSet.Songs);
        pianoSource.clip = randomSong;
        pianoSource.Play();
        _songReady = false;
        Invoke(nameof(StartSongDelay), randomSong.length);
        _canInterruptTime = Time.time + randomSong.length + delayGracePeriod;
    }
    
    private void PlayChord() {
        var randomChord = GetRandomAudioClip(ambianceBlockSet.Chords);
        pianoSource.clip = randomChord;
        pianoSource.Play();
        _chordReady = false;
        Invoke(nameof(StartChordDelay), randomChord.length);
        _canInterruptTime = Time.time + randomChord.length + delayGracePeriod;
    }

    private void StartSongDelay() {
        var randomDelay = Random.Range(songDelayRange.x, songDelayRange.y);
        Invoke(nameof(SetSongReady), randomDelay);
    }
    
    private void StartChordDelay() {
        var randomDelay = Random.Range(chordsDelayRange.x, chordsDelayRange.y);
        Invoke(nameof(SetChordReady), randomDelay);
    }

    public void SetSongReady() => _songReady = true;

    public void SetChordReady() => _chordReady = true;

    private AudioClip GetRandomAudioClip(List<AmbianceBlock> blocks) {
        return blocks[Random.Range(0, blocks.Count)].clip;
    }

    #region Debug
    
    public void ToggleMuteWind() {
        var vol = windSource.volume;
        if (vol > 0) {
            _windSourceVolume = vol;
            windSource.volume = 0;
        } else {
            windSource.volume = _windSourceVolume;
        }
    }

    public void ToggleMutePiano() {
        var vol = pianoSource.volume;
        if (vol > 0) {
            _pianoSourceVolume = vol;
            pianoSource.volume = 0;
        } else {
            pianoSource.volume = _pianoSourceVolume;
        }
    }

    public void MuteAll() {
        if (windSource.volume > 0)
            _windSourceVolume = windSource.volume;
        if (pianoSource.volume > 0)
            _pianoSourceVolume = pianoSource.volume;
        windSource.volume = 0;
        pianoSource.volume = 0;
    }

    public void ResetInterruptTimer() => _canInterruptTime = 0;

    #endregion
}
