using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBricks : MonoBehaviour
{
    public static GenerateBricks instance;
    public List<GameObject> bricks;
    public GameObject brickPrefab;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateNewBricks(int rows,int columns, int chanceToUpgrade)
    {
        float startPosX = -8f;
        float startPosY = 4.25f;
        float gapX = 16f / columns;
        float gapY = 1f;
        foreach (var brick in bricks)
        {
            Destroy(brick);
        }
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                var brick = Instantiate(brickPrefab,new Vector2(startPosX+gapX/2+gapX*x,startPosY-gapY*y),Quaternion.identity);
                brick.transform.localScale = new Vector2(gapX*0.9f,0.7f);
                int currentHp = 1;
                while(currentHp<7)
                {
                    if (Random.Range(0,100)<chanceToUpgrade) currentHp++;
                    else break;
                }
                brick.GetComponent<Brick>().hp = currentHp;
                bricks.Add(brick);
            }
        }
    }
    public void RemoveAllBricks()
    {
        foreach (var brick in bricks)
        {
            Destroy(brick);
        }
        bricks.Clear();
    }
    public void RemoveBrick(GameObject brick)
    {
        bricks.Remove(brick);
    }
}
