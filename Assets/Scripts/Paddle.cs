using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    public Rigidbody2D paddleRigidbody {get; private set;}
    public Vector2 moveDirection {get; private set;}
    public float moveSpeed = 50f;
    private Vector2 beginningPosition;

    private void Awake() {
        this.paddleRigidbody = GetComponent<Rigidbody2D>();
        beginningPosition = transform.position;
    }

    private void Update() {
        bool isLeft = Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow);
        bool isRight = Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow);

        if(isLeft)
            moveDirection = Vector2.left;
        else if(isRight)
            moveDirection = Vector2.right;
        else
            moveDirection = Vector2.zero;
    }
    private void FixedUpdate() {
        if(moveDirection != Vector2.zero)
        {
            this.paddleRigidbody.AddForce(moveDirection * moveSpeed);
        }
    }
    public void ResetPaddle()
    {
        transform.position = beginningPosition;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        
        Ball ball = other.collider.GetComponent<Ball>();

        if(ball != null)
        {
            Vector2 paddlePosition = transform.position;
            Vector2 contactPosition = other.GetContact(0).point;

            float offset = paddlePosition.x - contactPosition.x;
            float width = other.otherCollider.bounds.size.x / 2f;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.ballRigidbody.velocity.normalized);
            float bounceAngle = (offset / width) * 75f;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -75f, 75f);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.ballRigidbody.velocity = rotation * Vector2.up * ball.ballRigidbody.velocity.magnitude;
        }
    }
}
