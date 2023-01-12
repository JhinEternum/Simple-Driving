using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{

    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private float scoreMultiplier = 1;

    public const string HighScoreKey = "HighScore";

    private float score;

    void Update()
    {
        this.score += Time.deltaTime * this.scoreMultiplier;

        scoreText.text = Mathf.FloorToInt(this.score).ToString();
    }

    private void OnDestroy()
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        if (this.score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(this.score));
        }
    }
}
