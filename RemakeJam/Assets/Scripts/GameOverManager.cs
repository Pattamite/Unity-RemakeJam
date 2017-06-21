using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : LevelManager {

    public Text currentScoreText;
    public Text highScoreText;
    public Text newHighScoreText;

    void Start () {
        int currentScore = PlayerPrefs.GetInt(PlayerPrefsKeys.CURRENT_SCORE);
        int highScore = PlayerPrefs.GetInt(PlayerPrefsKeys.HIGH_SCORE);

        if(currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt(PlayerPrefsKeys.HIGH_SCORE, currentScore);
            newHighScoreText.enabled = true;
        }
        else newHighScoreText.enabled = false;

        currentScoreText.text = currentScore.ToString();
        highScoreText.text = highScore.ToString();
    }
	
	
	void Update () {
		
	}
}
