using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _dont_destroy_on_load : MonoBehaviour {
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
}
