using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour {
    public Transform LevelPanel;

    private void Start () {
        var playerLevel = PlayerPrefs.GetInt ("Pers_Level");

        var levelList = new List<Transform> ();
        foreach (Transform world in LevelPanel) {
            foreach (Transform level in world) {
                if (level.name.Contains ("level_")) {
                    levelList.Add (level);
                }
            }
        }

        foreach (Transform level in levelList) {
            var levelNumber = System.Convert.ToInt32 (level.name.Replace ("level_", ""));
            if (levelNumber < playerLevel) {
                var button = level.GetComponent<Button> ();
                var oldStar = level.Find ("stars/Image");
                var newStar = level.Find ("stars/Image (selected)");

                oldStar.gameObject.SetActive(false);
                newStar.gameObject.SetActive(true);
                button.targetGraphic = newStar.GetComponent<Image>();
            } else if (levelNumber > playerLevel) {
                var button = level.GetComponent<Button> ();
                button.interactable = false;
            }
        }
    }
}