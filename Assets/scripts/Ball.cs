using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public float Speed = 10f;
    public AudioClip Hit;

    private PlayerController Player;

    Vector3 _positionToPlayer;
    bool hasStarted = false;

    AudioSource _hitSound;
    SceneController _sceneController;
    BallRespawn _respawn;

    void Start () {
        Player = FindObjectOfType<PlayerController> ();
        _positionToPlayer = transform.position - Player.transform.position;
        _hitSound = GetComponent<AudioSource> ();
        _sceneController = FindObjectOfType<SceneController> ();
        _respawn = FindObjectOfType<BallRespawn> ();

        Speed += PlayerPrefs.GetInt ("Level") / 3;
    }

    void Update () {
        if (!hasStarted) {
            transform.position = Player.transform.position + _positionToPlayer;

            if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (1)) {
                GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-2f, 2f), Random.Range (1, 4f)).normalized * Speed;
                //transform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-(Speed / 3), Speed / 3), Speed);
                hasStarted = true;
            }
        }
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        var variance = new Vector2 (Random.Range (0f, 0.2f), Random.Range (0f, 0.2f));

        if (hasStarted) {
            //GetComponent<Rigidbody2D> ().velocity += variance;
            if (collision.collider.tag == "Player") {
                GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-2f, 2f), Random.Range (1, 4f)).normalized * Speed;
                _hitSound.Play ();
            } else {
                _hitSound.PlayOneShot (Hit);
            }
        }

        if (collision.collider.tag == "Deadline") {
            _respawn.DestroyBall (gameObject);
        }
    }
}