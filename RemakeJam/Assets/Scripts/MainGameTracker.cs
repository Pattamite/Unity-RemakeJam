using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameTracker : MonoBehaviour
{

    public static float GAME_SPEED = 1.0f;
    public static float FLOOR_LEVEL = -4.0f;
    public static int MAX_LIVES = 10;
    public static Text LIVES_COUNT_TEXT;
    [Range(0.0f, 10.0f)] public float gameSpeed = 1.0f;
    [Range(-4.0f, 8.0f)] public float floorLevel = -4.0f;
    [Range(1, 20)] public int maxLives = 10;
    public Text livesCountText;


    private static int CURRENT_LIVES;

    void Start ()
    {
        MAX_LIVES = maxLives;
        CURRENT_LIVES = MAX_LIVES;
        LIVES_COUNT_TEXT = livesCountText;
        LIVES_COUNT_TEXT.text = CURRENT_LIVES.ToString();
    }

    void Update ()
    {
        GAME_SPEED = gameSpeed;
        FLOOR_LEVEL = floorLevel;
    }

    public static void LifeLost()
    {
        CURRENT_LIVES--;
        LIVES_COUNT_TEXT.text = CURRENT_LIVES.ToString();
    }
}
