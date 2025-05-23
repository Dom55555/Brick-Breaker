using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball instance;
    public int speed = 10;
    public int lives = 3;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        Paddle.instance.transform.position = new Vector2(0,-4.5f);
        transform.position = new Vector2(0,-3);
        rb.velocity = new Vector2(Random.Range(0, 0.6f), 1).normalized * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Death"))
        {
            lives--;
            if (lives == 0) Game.instance.GameOver();
            else StartGame();
        }
        if (collision.transform.CompareTag("Paddle"))
        {
            if (GenerateBricks.instance.bricks.Count == 0)
            {
                transform.position = new Vector2(5,-5.6f);
                rb.velocity = Vector2.zero;
                Game.instance.GameOver(true);
            }
        }
    }
}
