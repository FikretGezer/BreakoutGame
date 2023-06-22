using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public Rigidbody2D brickRigidbody {get; private set;}
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private int health;
    public int pointsPerBrickState = 100;
    
    private void Awake() {
        brickRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = sprites.Length;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Ball ball = other.collider.GetComponent<Ball>();
        if(ball != null)
        {
            Hit();
            FindObjectOfType<GameManager>().Hit(this);
        }
    }
    private void Hit()
    {
        health--;

        if(health <= 0)
            this.gameObject.SetActive(false);
        else
            spriteRenderer.sprite = sprites[health-1];
    }
}
