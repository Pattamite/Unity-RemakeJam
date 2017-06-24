using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float scaleMax = 1.0f;
    private float scaleMin = 4.0f;
    [Range(1f, 4f)] public float currentScale = 4.0f;
    public float growthRate = 0.01f;
    // Use this for initialization
    void Start ()
    {
    }
    // Update is called once per frame
    void Update ()
    {
        if (currentScale > scaleMin - MainGameTracker.CURRENT_LEVEL && currentScale > scaleMax)
        {
            currentScale = currentScale - growthRate * Time.deltaTime;
        }
        transform.localScale = new Vector3(currentScale, currentScale, 1);
    }
}
