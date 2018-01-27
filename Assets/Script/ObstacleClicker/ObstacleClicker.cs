using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleClicker : Obstacle {
    public int click;
    public Sprite[] sprites = new Sprite[0];
    private SpriteRenderer sprite;
    Rigidbody2D rb2d;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = sprites[0];
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector3(0, -fallingSpeed, 0);
    }

    public void ButtonClicked()
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
