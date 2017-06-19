using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public static int STATUS_NORMAL = 0;
    public static int STATUS_INVINCIBLE = -1;
    public static int STATUS_DEAD = -2;

    public static float MAX_SPEED = 2f;
    public static float MIN_SPEED = 0.1f;
    public static float START_SPEED = 0.5f;
    public static float RANDOM_RANGE = 0.1f;
    public static float RANDOM_TIME = 1.5f;
    public int id = 0;

    public float currentSpeed;
    private int status;
    public float movementDirection;
    private float randomValue;

    // Use this for initialization
    void Start()
    {
        currentSpeed = START_SPEED;
        status = STATUS_NORMAL;
        movementDirection = 1f;

        StartCoroutine(UpdateSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.position += Vector3.right * currentSpeed * Time.deltaTime * MainGameTracker.GAME_SPEED * movementDirection;
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        GameObject otherObject = collide.gameObject;
        if(otherObject.GetComponent<Cactus>())
        {
            Cactus otherCactus = otherObject.GetComponent<Cactus>();
            if(id > otherCactus.id)
            {
                if(movementDirection == otherCactus.movementDirection)
                {
                    if(movementDirection == 1f)
                    {
                        if(transform.position.x > otherCactus.transform.position.x)
                        {
                            otherCactus.movementDirection *= -1f;
                        }
                        else
                        {
                            movementDirection *= -1f;
                        }
                    }
                    else
                    {
                        if (transform.position.x < otherCactus.transform.position.x)
                        {
                            otherCactus.movementDirection *= -1f;
                        }
                        else
                        {
                            movementDirection *= -1f;
                        }
                    }
                }
                else
                {
                    movementDirection *= -1f;
                    otherCactus.movementDirection *= -1f;
                }
            }
        }
        else movementDirection *= -1f;
    }   

    IEnumerator UpdateSpeed()
    {
        while(true)
        {
            currentSpeed += Random.Range(-RANDOM_RANGE, RANDOM_RANGE);
            currentSpeed = Mathf.Clamp(currentSpeed, MIN_SPEED, MAX_SPEED);
            yield return new WaitForSeconds(RANDOM_TIME * MainGameTracker.GAME_SPEED);
        }
    }
}