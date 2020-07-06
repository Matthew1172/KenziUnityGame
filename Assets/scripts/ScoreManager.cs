using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    public float scoreCount;
    public float highScoreCount;
    public float pointsPerSecond;
    public bool scoreIncreasing;

    void Start()
    {
        if(PlayerPrefs.HasKey("highScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("highScore");
        }
    }

    void Update()
    {
        if(scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }
        if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("highScore", highScoreCount);
        }
        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        highScoreText.text = "High-Score: " + Mathf.Round(highScoreCount);
    }

    public void AddScore(int pointsToAdd)
    {
        scoreCount += pointsToAdd;
    }
}
