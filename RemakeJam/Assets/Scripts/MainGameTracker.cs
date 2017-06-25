using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

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
    public static int CURRENT_LEVEL = 0;
    [Range(0.0f, 10.0f)] public float gameSpeed = 1.0f;
    public bool forceGameSpeed = false;
    [Range(-7.0f, 8.0f)] public float floorLevel = -4.0f;
    public static float levelMax = 3;
    [Range(1, 20)] public int maxLives = 10;
    public float risingSpeed = 0.01f;
    public int scoreRain = 100;
    public int scoreFert = 100;

    private static int CURRENT_LIVES;
    private static bool IS_PAUSE;
    private static float CURRENT_GAME_SPEED;

    public bool isMainGame = false;
    public Image pauseButton;
    public Sprite pauseSprite;
    public Sprite resumeSprite;

    private static Image PAUSE_BUTTON;
    private static Sprite PAUSE_SPRITE;
    private static Sprite RESUME_SPRITE;

    

    void Start ()
    {
        CURRENT_SCORE = 0;
        CURRENT_LEVEL = 0;
        MAX_LIVES = maxLives;
        CURRENT_LIVES = MAX_LIVES;
        LIVES_COUNT_TEXT = livesCountText;
        SetLivesText();
        SCORE_COUNT_TEXT = ScoreCountText;
        SetScoreText();
        SCORE_RAIN = scoreRain;
        SCORE_FERT = scoreFert;
        FLOOR_LEVEL = floorLevel;
        SAFE_TIME = Time.time;
        RISING_SPEED = risingSpeed;
        GAME_SPEED = gameSpeed;
        CURRENT_GAME_SPEED = gameSpeed;
        if (isMainGame)
        {
            PAUSE_BUTTON = pauseButton;
            PAUSE_SPRITE = pauseSprite;
            RESUME_SPRITE = resumeSprite;
        }
    }

    void Update ()
    {
        if (!IS_PAUSE && forceGameSpeed) GAME_SPEED = gameSpeed;
        if (FLOOR_LEVEL < MAX_FLOOR_LEVEL && Time.time - SAFE_TIME >= SAFE_COOLDOWN && !IS_PAUSE)
        {
            FLOOR_LEVEL += (RISING_SPEED * Time.deltaTime * GAME_SPEED);
            if (FLOOR_LEVEL > MAX_FLOOR_LEVEL)
            {
                FLOOR_LEVEL = MAX_FLOOR_LEVEL;
            }
        }
        NextLevel();
        if (CrossPlatformInputManager.GetButtonDown("Pause")) TogglePause();
        if (CrossPlatformInputManager.GetButtonDown("Menu")) LoadScene(ScenesNumber.MAIN_MENU);
    }

    public static void NextLevel()
    {
        if (CURRENT_SCORE >= (100 * CURRENT_LEVEL) * Mathf.Pow(2 , CURRENT_LEVEL + 1) && CURRENT_LEVEL < levelMax)
        {
            CURRENT_LEVEL = CURRENT_LEVEL + 1;
            GAME_SPEED = GAME_SPEED + 0.25f;
            CURRENT_GAME_SPEED = GAME_SPEED;
        }
    }
    public static void LifeLost()
    {
        CURRENT_LIVES--;
        SetLivesText();
        if (CURRENT_LIVES <= 0) GameOver();
    }

    public static void AddScoreRain()
    {
        CURRENT_SCORE += SCORE_RAIN;
        SetScoreText();
    }

    public static void AddScoreFert()
    {
        CURRENT_SCORE += SCORE_FERT;
        SetScoreText();
    }

    public static void GameOver()
    {
        MusicPlayer.Stop();
        PlayerPrefs.SetInt(PlayerPrefsKeys.CURRENT_SCORE, CURRENT_SCORE);
        LoadSceneStatic(ScenesNumber.GAME_OVER);
    }

    private static void SetScoreText()
    {
        SCORE_COUNT_TEXT.text = "Score: " + CURRENT_SCORE.ToString();
    }

    private static void SetLivesText()
    {
        LIVES_COUNT_TEXT.text = "Lives: " + CURRENT_LIVES.ToString();
    }

    public static void TogglePause()
    {
        if(IS_PAUSE)
        {
            //print("Resume");
            IS_PAUSE = false;
            GAME_SPEED = CURRENT_GAME_SPEED;
            PAUSE_BUTTON.sprite = PAUSE_SPRITE;
            if(SoundPrefsManager.IsMusicOn()) MusicPlayer.GameResume();
        }
        else
        {
            //print("Pause");
            IS_PAUSE = true;
            CURRENT_GAME_SPEED = GAME_SPEED;
            GAME_SPEED = 0f;
            PAUSE_BUTTON.sprite = RESUME_SPRITE;
            if (SoundPrefsManager.IsMusicOn()) MusicPlayer.GamePause();
        }
    }
}
