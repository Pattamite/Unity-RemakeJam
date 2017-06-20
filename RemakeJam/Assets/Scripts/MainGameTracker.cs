using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameTracker : MonoBehaviour
{
    public static float GAME_SPEED = 1.0f;
    public static float FLOOR_LEVEL = -4.0f;
    [Range(0.0f, 10.0f)] public float gameSpeed = 1.0f;
    [Range(-4.0f, 8.0f)] public float floorLevel = -4.0f;

    void Start ()
    {
    }

    void Update ()
    {
        GAME_SPEED = gameSpeed;
        FLOOR_LEVEL = floorLevel;
    }
}
