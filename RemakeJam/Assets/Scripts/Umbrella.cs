using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour {

    public float speed = 5.0f;
    private float currentHorizontalDirection = 0f;
    private float currentVerticalDirection = 0f;
    private float sqrtHalf = Mathf.Sqrt(0.5f);


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
    }

    private void Movement()
    {
        currentHorizontalDirection = 0f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            currentHorizontalDirection += 1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            currentHorizontalDirection += -1f;

        currentVerticalDirection = 0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            currentVerticalDirection += 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            currentVerticalDirection += -1f;

        if (currentHorizontalDirection != 0f && currentVerticalDirection != 0f)
        {
            currentHorizontalDirection *= sqrtHalf;
            currentVerticalDirection *= sqrtHalf;
        }

        transform.position += Vector3.right * speed * Time.deltaTime * currentHorizontalDirection * MainGameTracker.GAME_SPEED;
        transform.position += Vector3.up * speed * Time.deltaTime * currentVerticalDirection * MainGameTracker.GAME_SPEED;
    }
}
