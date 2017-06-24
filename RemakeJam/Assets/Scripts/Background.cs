using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float scaleMax = 1.0f;
    private float scaleMin = 4.0f;
    [Range(1f, 4f)] public float currentScale = 4.0f;
    public float growthRate = 0.01f;

    public bool isMainMenu = false;
    public float minXPosition = -22f;
    public float maxXPosition = 22f;
    public float speed = 1f;
    private float direction;
    // Use this for initialization
    void Start ()
    {
        direction = 1f;
    }
    // Update is called once per frame
    void Update ()
    {
        if (currentScale > scaleMin - MainGameTracker.CURRENT_LEVEL && currentScale > scaleMax)
        {
            currentScale = currentScale - growthRate * Time.deltaTime;
        }
        if(isMainMenu)
        {
            transform.localScale = new Vector3(currentScale, currentScale, 1);
            if (transform.position.x < minXPosition) direction = -1f;
            if (transform.position.x > maxXPosition) direction = 1f;

            transform.position += Vector3.left * direction * speed * Time.deltaTime;
        }

    }
}
