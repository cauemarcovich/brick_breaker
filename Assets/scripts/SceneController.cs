using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public GameObject pause;
    public GameObject BallPrefab;
    bool paused = false;

    public void LoadMenu () {
        SceneManager.LoadScene ("Menu");
    }
    public void LoadLevelSelect () {
        if (!PlayerPrefs.HasKey ("Pers_Level")) {
            PlayerPrefs.SetInt ("Pers_Level", 1);
        }
        SceneManager.LoadScene ("Level_Select");
    }
    public void LoadNextLevel (int level = 1) {
        PlayerPrefs.SetInt ("Level", level);
        PlayerPrefs.Save ();
        SceneManager.LoadScene ("Level");
    }
    public void LoadCongratulations () {
        SceneManager.LoadScene ("Congratulations");
    }
    public void Quit () {
        Application.Quit ();
    }

    public void CheckBricksDestroyed () {
        if (Brick.BricksCount <= 0) {
            StartCoroutine (Fanfare ());
        }
    }

    IEnumerator Fanfare () {
        GameObject.Find ("ball(Clone)").GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
        GameObject.Find ("paddle_player").GetComponent<PlayerController> ().MoveSpeed = 0;
        var audioController = GameObject.Find ("audioController").GetComponent<AudioController> ();
        audioController.StopSound ();

        yield return new WaitForSeconds (1f);
        audioController.PlayFanfare ();
        yield return new WaitForSeconds (1f);
        var canvas = GameObject.Find ("Canvas").transform;
        GameObject child = null;
        foreach (Transform el in canvas) {
            child = el.gameObject;
            break;
        }
        child.SetActive (true);
        yield return new WaitForSeconds (5f);

        var newLevel = PlayerPrefs.GetInt ("Level") + 1;
        if (PlayerPrefs.GetInt ("Level") < 15) {
            if (newLevel > PlayerPrefs.GetInt ("Pers_Level"))
                PlayerPrefs.SetInt ("Pers_Level", newLevel);
            PlayerPrefs.Save ();
            LoadNextLevel (newLevel);
        } else {
            PlayerPrefs.SetInt ("Pers_Level", newLevel);
            PlayerPrefs.Save ();
            LoadCongratulations ();
        }
    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.Escape)) {
            if (!paused) {
                Time.timeScale = 0;
                pause.SetActive (true);
                paused = true;
            } else {
                Time.timeScale = 1;
                pause.SetActive (false);
                paused = false;
            }
        }
        if (Input.GetKeyDown (KeyCode.E)) {
            if (paused) {
                LoadLevelSelect ();
            }
        }
    }
}