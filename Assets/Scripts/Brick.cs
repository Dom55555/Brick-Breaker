using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hp = 1;
    private Color[] colors = new Color[] {
        new Color(1,0,0),
        new Color(1,0.5f,0),
        new Color(1,0.878f,0),
        new Color(0,0.831f,0),
        new Color(0,0.624f,1),
        new Color(0,0.24f,1),
        new Color(0.573f,0,1),
    };
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<SpriteRenderer>().color = colors[hp - 1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Ball"))
        {
            hp--;
            Game.instance.score+=Game.instance.scoreMultiplier;
            if(hp==0)
            {
                GenerateBricks.instance.RemoveBrick(gameObject);
                Destroy(gameObject);
            }
            else transform.GetComponent<SpriteRenderer>().color = colors[hp - 1];
        }
    }
}
