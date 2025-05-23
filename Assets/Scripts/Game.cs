using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game instance;
    // Start is called before the first frame update
    public GameObject startMenu;
    public GameObject settingsMenu;
    public GameObject basketballHop;
    public Slider slider;
    public TMP_Text[] propertyValues;
    public TMP_Text lastScore;
    public TMP_Text scoreMultiplierText;
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text catchBallText;
    public float scoreMultiplier = 1;
    public float score = 0;
    public int rows=3;
    public int columns=8;
    public int chanceToUpgrade=50;


    private int catchScore;
    private bool noBricks = true;
    private int chosenPropertyIndex = 0;
    private bool ignoreChange = false;

    void Start()
    {
        instance = this;
        scoreMultiplier = 2.08333f;
    }
    void Update()
    {
        scoreText.text = Mathf.Round(score).ToString();
        livesText.text = Ball.instance.lives + " lives";
        if(!noBricks && !startMenu.gameObject.activeSelf&& GenerateBricks.instance.bricks.Count == 0)
        {
            noBricks = true;
            basketballHop.gameObject.SetActive(true);
            Paddle.instance.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            catchBallText.gameObject.SetActive(true);
            catchScore = (int)Mathf.Round(25 * scoreMultiplier / (4 - Ball.instance.lives));
            catchScore = (int)Mathf.Round(Random.Range(catchScore/2,catchScore));
            catchBallText.text = "CATCH THE BALL\n+" + catchScore + " SCORE";
        }
    }

    public void GameOver(bool catchBall=false)
    {
        if(catchBall) score += catchScore;
        lastScore.text = "Last Score: "+Mathf.Round(score);
        score = 0;
        GenerateBricks.instance.RemoveAllBricks();
        ToggleStartMenu(true);
        catchBallText.gameObject.SetActive(false);
    }
    public void ToggleSettings(bool state)
    {
        settingsMenu.SetActive(state);
        scoreMultiplierText.text = "Score multiplier: +" + Mathf.Round(scoreMultiplier * 100) + "%";
    }
    public void ToggleStartMenu(bool state)
    {
        Ball.instance.lives = 3;
        startMenu.SetActive(state);
        noBricks = false;
        scoreText.gameObject.SetActive(!state);
        livesText.gameObject.SetActive(!state);
        if (!state) GenerateBricks.instance.GenerateNewBricks(rows,columns,chanceToUpgrade);
    }
    public void PropertyChosen(int index)
    {
        ignoreChange = true;
        chosenPropertyIndex = index;
        if(index==1)
        {
            slider.minValue = 3;
            slider.maxValue = 7;
            slider.value = rows;
        }
        else if(index==2)
        {
            slider.minValue = 3;
            slider.maxValue = 15;
            slider.value = columns;
        }
        else if(index==3)
        {
            slider.minValue = 0;
            slider.maxValue = 80;
            slider.value = chanceToUpgrade;
        }
        else if (index == 4)
        {
            slider.minValue = 4;
            slider.maxValue = 20;
            slider.value = Ball.instance.speed;
        }
        else
        {
            slider.minValue = 4;
            slider.maxValue = 40;
            slider.value = Paddle.instance.moveSpeed;
        }
        ignoreChange = false;
    }
    public void ChangeValue(float value)
    {
        if (ignoreChange) return;
        if(chosenPropertyIndex==1)
        {
            rows = (int)value;
            propertyValues[chosenPropertyIndex - 1].text = "Rows: " + rows;
        }
        if (chosenPropertyIndex == 2)
        {
            columns = (int)value;
            propertyValues[chosenPropertyIndex - 1].text = "Columns: " + columns;
        }
        if (chosenPropertyIndex == 3)
        {
            chanceToUpgrade = (int)value;
            propertyValues[chosenPropertyIndex - 1].text = "Brick upgrade chance (when generating): " + chanceToUpgrade+"%";
        }
        if (chosenPropertyIndex == 4)
        {
            Ball.instance.speed = (int)value;
            propertyValues[chosenPropertyIndex - 1].text = "Ball speed: " + Ball.instance.speed;
        }
        if (chosenPropertyIndex == 5)
        {
            Paddle.instance.moveSpeed = (int)value;
            propertyValues[chosenPropertyIndex - 1].text = "Paddle speed: " + Paddle.instance.moveSpeed;
        }
        scoreMultiplier = 1 + (rows - 3) * 0.25f + (columns - 3) / 12f + chanceToUpgrade * 0.0125f + (Ball.instance.speed - 4) / 16f;
        scoreMultiplierText.text = "Score multiplier: +" + Mathf.Round(scoreMultiplier * 100) + "%";
    }
}
