using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawn : MonoBehaviour {
	public GameObject BallPrefab;

	void Start () {
		CreateNewBall ();
	}

	public void DestroyBall (GameObject ball) {
		Destroy (ball);
		CreateNewBall ();
	}

	void CreateNewBall () {
		var player = FindObjectOfType<PlayerController> ();
		var x = Instantiate (BallPrefab, player.transform.position + new Vector3 (0f, 0.5f), Quaternion.identity);
		Debug.Log (x);
	}
}