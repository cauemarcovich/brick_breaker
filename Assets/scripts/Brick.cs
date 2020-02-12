using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{    
    public List<Sprite> sprites;
    public static int BricksCount;

    int _hits = 0;
    SceneController _sceneController;

    void Awake()
    {
        bool isBreakable = tag == "Breakable";
        if (isBreakable)
            BricksCount++;

        _sceneController = FindObjectOfType<SceneController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool isBreakable = tag == "Breakable";
        if (isBreakable)
            HandleHits();
    }

    void HandleHits()
    {
        _hits++;
        var maxHits = sprites.Count + 1;

        if (_hits >= maxHits)
        {
            Destroy(gameObject);
            BricksCount--;
            _sceneController.CheckBricksDestroyed();
        }
        else
        {
            LoadSprites();
        }
    }

    void LoadSprites()
    {
        var spriteIndex = _hits - 1;
        if(sprites[spriteIndex])
            GetComponent<SpriteRenderer>().sprite = sprites[spriteIndex];
    }
}
