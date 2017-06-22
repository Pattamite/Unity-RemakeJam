using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameTracker : LevelManager
{
    public Text livesCountText;
    public Text ScoreCountText;

    public static float GAME_SPEED = 1.0f;
    public static float FLOOR_LEVEL = -4.0f;
    public static float MAX_FLOOR_LEVEL = 3.0f;
    public static float RISING_SPEED = 0.2f;
    public static float SAFE_COOLDOWN = 2f;
    public static float SAFE_TIME;
    public static int MAX_LIVES = 10;
    public static Text LIVES_COUNT_TEXT;
    public static Text SCORE_COUNT_TEXT;
    public static int SCORE_RAIN = 100;
    public static int SCORE_FERT = 100;
    public static int CURRENT_SCORE = 0;
    [Range(0.0f, 10.0f)] public float gameSpeed = 1.0f;
    [Range(-7.0f, 8.0f)] public float floorLevel = -4.0f;
    [Range(1, 20)] public int maxLives = 10;
    public float risingSpeed = 0.01f;
    public int scoreRain = 100;
    public int scoreFert = 100;

    private static int CURRENT_LIVES;

    void Start ()
    {
        CURRENT_SCORE = 0;
        MAX_LIVES = maxLives;
        CURRENT_LIVES = MAX_LIVES;
        LIVES_COUNT_TEXT = livesCountText;
        LIVES_COUNT_TEXT.text = CURRENT_LIVES.ToString();
        SCORE_COUNT_TEXT = ScoreCountText;
        SCORE_COUNT_TEXT.text = CURRENT_SCORE.ToString();
        SCORE_RAIN = scoreRain;
        SCORE_FERT = scoreFert;
        FLOOR_LEVEL = floorLevel;
        SAFE_TIME = Time.time;
        RISING_SPEED = risingSpeed;
    }

    void Update ()
    {
        GAME_SPEED = gameSpeed;
        if (FLOOR_LEVEL < MAX_FLOOR_LEVEL && Time.time - SAFE_TIME >= SAFE_COOLDOWN)
        {
            FLOOR_LEVEL = FLOOR_LEVEL + (RISING_SPEED * Time.deltaTime * GAME_SPEED);
            if (FLOOR_LEVEL > MAX_FLOOR_LEVEL)
            {
                FLOOR_LEVEL = MAX_FLOOR_LEVEL;
            }
        }
    }

    public static void LifeLost()
    {
        CURRENT_LIVES--;
        LIVES_COUNT_TEXT.text = CURRENT_LIVES.ToString();
        if (CURRENT_LIVES <= 0) GameOver();
    }

    public static void AddScoreRain()
    {
        CURRENT_SCORE += SCORE_RAIN;
        SCORE_COUNT_TEXT.text = CURRENT_SCORE.ToString();
    }

    public static void AddScoreFert()
    {
        CURRENT_SCORE += SCORE_FERT;
        SCORE_COUNT_TEXT.text = CURRENT_SCORE.ToString();
    }

    public static void GameOver()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.CURRENT_SCORE, CURRENT_SCORE);
        LoadSceneStatic(ScenesNumber.GAME_OVER);
    }
}
