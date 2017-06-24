using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    public float multiplier = 2f;
    public float extraSpace = 7f;

    private float wavePosition = 0f;
    private float waveHeight = 0f;
    private float randomOffsetPos = 0f;
    public bool isHTP = false;
    // Use this for initialization
    void Start ()
    {
        wavePosition = 0f;
        waveHeight = 0f;
        randomOffsetPos = Random.value * Mathf.PI;
    }
    // Update is called once per frame
    void Update ()
    {
        wavePosition = Mathf.Sin(Time.time + randomOffsetPos) * multiplier;
        transform.position += Vector3.right * wavePosition * MainGameTracker.GAME_SPEED * Time.deltaTime;
        if(!isHTP) transform.position = new Vector3(transform.position.x, MainGameTracker.FLOOR_LEVEL - extraSpace, transform.position.z);
        /*if (transform.position.y < MainGameTracker.FLOOR_LEVEL - extraSpace && !isHTP)
        {
            transform.position += Vector3.up * MainGameTracker.GAME_SPEED * Time.deltaTime * MainGameTracker.RISING_SPEED;
        }*/
        //transform.position += Vector3.up * MainGameTracker.GAME_SPEED * Time.deltaTime * Mathf.Cos(Time.time);
    }
}
