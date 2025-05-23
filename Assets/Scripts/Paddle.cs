using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public static Paddle instance;
    public float moveSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x + moveSpeed * Time.deltaTime, -7.45f, 7.45f), transform.position.y);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x - moveSpeed * Time.deltaTime,-7.45f,7.45f), transform.position.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 contactPoint = collision.transform.position;
            Vector2 paddlePos = transform.position;

            float paddleWidth = GetComponent<BoxCollider2D>().bounds.size.x;
            float offset = (contactPoint.x - paddlePos.x) / paddleWidth;

            offset = Mathf.Clamp(offset, -0.5f, 0.5f);
            float maxBounceAngle = 75f * Mathf.Deg2Rad;
            float bounceAngle = offset * maxBounceAngle;
            float speed = ballRb.velocity.magnitude;
            Vector2 direction = new Vector2(Mathf.Sin(bounceAngle), Mathf.Cos(bounceAngle));
            if (direction.y < 0) direction.y = -direction.y;
            ballRb.velocity = direction.normalized * speed;
        }
    }
}
