using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    public float multiplier = 2f;
    public float extraSpace = 7f;

    private float wavePosition = 0;
    // Use this for initialization
    void Start ()
    {
        wavePosition = 0;
    }
    // Update is called once per frame
    void Update ()
    {
        wavePosition = Mathf.Sin(Time.time) * multiplier;
        transform.position += Vector3.right * wavePosition * MainGameTracker.GAME_SPEED * Time.deltaTime;
        if (transform.position.y < MainGameTracker.FLOOR_LEVEL - extraSpace)
        {
            transform.position += Vector3.up * MainGameTracker.GAME_SPEED * Time.deltaTime * MainGameTracker.RISING_SPEED;
        }
    }
}
