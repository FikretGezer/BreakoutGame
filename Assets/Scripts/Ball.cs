using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public Rigidbody2D ballRigidbody {get; private set;}
    public float ballSpeed = 500f;
    private void Awake() {
        ballRigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        ResetBall();
    }
    public void ResetBall()
    {
        ballRigidbody.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        Invoke(nameof(MoveBall), 1f);
    }
    private void MoveBall()
    {
        Vector2 pos;
        pos.x = Random.Range(-0.3f, 0.3f);
        pos.y = -1f;

        ballRigidbody.AddForce(pos * ballSpeed);
    }
   
}
