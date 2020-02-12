using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MapController : MonoBehaviour {
    public ColorToPrefab[] Mapping;
    public int debug_level = 1;
    public bool enableDebug;

    private Transform _map;
    private Texture2D _levelSprite;

    private void Awake () {
        if (enableDebug) {
            PlayerPrefs.SetInt ("Level", debug_level);
            PlayerPrefs.SetInt ("Pers_Level", debug_level);
        }
    }

    void Start () {
        //_levelSprite = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/sprites/levels/level_" + PlayerPrefs.GetInt("Level").ToString("00") + ".png", typeof(Texture2D));
        _levelSprite = Resources.Load<Texture2D> ("levels/level_" + PlayerPrefs.GetInt ("Level").ToString ("00"));
        _map = GameObject.Find ("map").transform;
        Brick.BricksCount = 0;

        SetBackground ();
        GenerateMap ();
    }
    void SetBackground () {
        var p_level = PlayerPrefs.GetInt ("Level");

        var bg_number = 0;
        if (p_level <= 3) { bg_number = 1; } else if (p_level <= 6) bg_number = 2;
        else if (p_level <= 9) bg_number = 3;
        else if (p_level <= 12) bg_number = 4;
        else if (p_level <= 14) bg_number = 5;
        else bg_number = 6;

        var path = "background/background_" + bg_number.ToString ();
        var bg_renderer = GameObject.Find ("stage").transform.Find ("background").GetComponent<SpriteRenderer> ();
        bg_renderer.sprite = Resources.Load<Sprite> (path);
    }

    void GenerateMap () {
        Vector2 prefabSize = Mapping.FirstOrDefault ().Prefab.GetComponent<SpriteRenderer> ().size;
        for (int x = 0; x < _levelSprite.width; x++) {
            for (int y = 0; y < _levelSprite.height; y++) {
                var pixel = _levelSprite.GetPixel (x, y);

                if (pixel.a == 0) continue;

                var mapper = Mapping.FirstOrDefault (_ => _.Color.Equals (pixel));
                if (mapper == null) {
                    Debug.LogError ("Mapper não encontrado. Você está fazendo isto errado. \nr:" + pixel.r + " g:" + pixel.g + "b:" + pixel.b);
                    return;
                }
                var prefab = mapper.Prefab;

                var position = new Vector2 (x + (prefabSize.x / 2f), (5 + (y * 0.333f)) + prefabSize.y / 2);
                Instantiate (prefab, position, Quaternion.identity, _map);
            }
        }
    }

    [System.Serializable]
    public class ColorToPrefab {
        public Color Color;
        public GameObject Prefab;
    }
}