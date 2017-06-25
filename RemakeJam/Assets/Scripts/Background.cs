using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Camera mainCamera;
    private float scaleMax = 1.0f;
    private float scaleMin = 4.0f;
    [Range(1f, 4f)] public float currentScale = 4.0f;
    public float growthRate = 0.01f;

    public bool isMainMenu = false;
    public float speed = 1f;
    private float direction;
    private float scaling;
    private float cameraLimit;
    private float bgLeft;
    private float bgRight;
    public float imageSize = 7f * 0.55f;

    void Start ()
    {
        direction = 1f;
        scaling = (scaleMin - scaleMax) / MainGameTracker.levelMax;
        cameraLimit = mainCamera.orthographicSize * mainCamera.aspect;
        bgLeft = (transform.localScale.x * imageSize * -1f) + transform.position.x;
        bgRight = (transform.localScale.x * imageSize)  + transform.position.x;
    }

    void Update ()
    {
        imageSize = 7f *  transform.GetChild(0).GetComponent<Transform>().localScale.x / 0.55f;
        cameraLimit = mainCamera.orthographicSize * mainCamera.aspect;
        bgLeft = (transform.localScale.x * imageSize * -1f) + transform.position.x;
        bgRight = (transform.localScale.x * imageSize) + transform.position.x;
        //print(cameraLimit);
        //print(bgLeft + " / " + bgRight + " // " +  (-cameraLimit) + " / " + cameraLimit + " = " + direction);
        if (currentScale > scaleMin - (scaling * MainGameTracker.CURRENT_LEVEL) && currentScale > scaleMax)
        {
            currentScale = currentScale - growthRate * Time.deltaTime;
        }
        if(isMainMenu)
        {
            transform.localScale = new Vector3(currentScale, currentScale, 1);
            if (bgRight < cameraLimit) direction = -1f;
            else if (bgLeft > -cameraLimit) direction = 1f;

            transform.position += Vector3.left * direction * speed * Time.deltaTime;
        }

    }
}
