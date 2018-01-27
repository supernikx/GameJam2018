using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleClicker : Obstacle {
    public int click, clickToDestroy;
    public Sprite[] sprites = new Sprite[0];
    private SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = sprites[0];
    }
    private void OnMouseDown()
    {
        click++;
        if (click == sprites.Length)
        {
            Destroy(gameObject);
        }
        else
        {
            sprite.sprite = sprites[click];
        }
    }
}
