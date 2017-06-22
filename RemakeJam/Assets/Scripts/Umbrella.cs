using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour {

    public float speed = 5.0f;
    public float stunDuration = 1.0f;
    [Range(0f, 8f)]  public float upperVericalLimit = 8f;
    [Range(-8f, 0f)] public float lowerVerticalLimit;// = -8f;
    public float bottomMargin = 1f;
    [Range(0f, 4.5f)]  public float upperHorizontalcalLimit = 4.5f;
    [Range(-4.5f, 0f)] public float lowerHorizontalLimit = -4.5f;
    private float currentHorizontalDirection = 0f;
    private float currentVerticalDirection = 0f;
    private float sqrtHalf = Mathf.Sqrt(0.5f);
    private bool stunStatus = false;
    private float stunTime;

    void Start ()
    {
        stunTime = 0f;
        lowerVerticalLimit = MainGameTracker.FLOOR_LEVEL + bottomMargin;
    }

  // Update is called once per frame
    void Update ()
    {
        Movement();
        lowerVerticalLimit = MainGameTracker.FLOOR_LEVEL + bottomMargin;
    }

    public bool getStun()
    {
        return stunStatus;
    }

    public void stunned()
    {
        stunStatus = true;
        stunTime = Time.time;
    }

    private void Movement()
    {
        if (transform.position.y < MainGameTracker.FLOOR_LEVEL + bottomMargin)
        {
            transform.position += Vector3.up * Time.deltaTime * MainGameTracker.GAME_SPEED * MainGameTracker.RISING_SPEED;
        }
        // TODO: Raise umbrella with floor level
        currentHorizontalDirection = 0f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            currentHorizontalDirection += 1f;
            transform.localScale = (Vector3.left * 2) + Vector3.one ;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            currentHorizontalDirection += -1f;
            transform.localScale = Vector3.one;
        }
        currentVerticalDirection = 0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            currentVerticalDirection += 1f;
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && transform.position.y > MainGameTracker.FLOOR_LEVEL + bottomMargin)
            currentVerticalDirection += -1f;

        if (currentHorizontalDirection != 0f && currentVerticalDirection != 0f)
        {
            currentHorizontalDirection *= sqrtHalf;
            currentVerticalDirection *= sqrtHalf;
        }
        if (stunStatus == false)
        {
            transform.position += Vector3.right * speed * Time.deltaTime * currentHorizontalDirection * MainGameTracker.GAME_SPEED;
            transform.position += Vector3.up * speed * Time.deltaTime * currentVerticalDirection * MainGameTracker.GAME_SPEED;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, lowerHorizontalLimit, upperHorizontalcalLimit),
                                             Mathf.Clamp(transform.position.y, lowerVerticalLimit, upperVericalLimit), transform.position.z);
        }
        else if (Time.time - stunTime >= stunDuration)
        {
            stunStatus = false;
        }
    }
}
