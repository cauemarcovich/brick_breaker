using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    public AudioClip[] Musics;
    public AudioClip Fanfare;
    private AudioSource _audioSource;

    int _playerLevel;

    void Start () {
        Destroy (GameObject.Find ("audioManager"));

        _audioSource = GetComponent<AudioSource> ();
        StartSong ();
    }

    void StartSong () {
        var _playerLevel = PlayerPrefs.GetInt ("Level");

        var audioNumber = -1;
        if (_playerLevel <= 12) {
            Debug.Log (_playerLevel);
            if (_playerLevel % 3 == 0) {
                audioNumber = 2;
            } else if (_playerLevel % 3 == 2) {
                audioNumber = 1;
            } else {
                audioNumber = 0;
            }
        } else if (_playerLevel <= 14) {
            audioNumber = 3;
        } else {
            audioNumber = 4;
        }

        _audioSource.Stop ();
        _audioSource.clip = Musics[audioNumber];
        _audioSource.Play ();
    }

    void Update () {
        if (_playerLevel == PlayerPrefs.GetInt ("Level")) return;

    }

    public void StopSound () {
        _audioSource.volume = 0f;
        _audioSource.Stop ();
    }

    public void PlayFanfare () {
        _audioSource.clip = Fanfare;
        _audioSource.volume = 1;
        _audioSource.Play ();
    }
}