using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CongratulationsController : MonoBehaviour {
    TextMeshProUGUI Congratulations;
    Text Aux;

    void Start () {
        Congratulations = GameObject.Find ("Congratulations").GetComponent<TextMeshProUGUI> ();
        StartCoroutine ("ShowCongratulations");
    }

    IEnumerator ShowCongratulations () {
        while (Congratulations.color.a < 0.99f) {
            var cColor = Congratulations.color;
            Congratulations.color = new Color (cColor.r, cColor.g, cColor.b, Mathf.Clamp01 (cColor.a + 0.01f));
            yield return 0;
        }
        
        yield return new WaitForSeconds (5f);
        var sceneController = GameObject.Find ("SceneController").GetComponent<SceneController> ();
        sceneController.LoadMenu ();
    }
}