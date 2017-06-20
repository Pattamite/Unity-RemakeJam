using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public static int STATUS_NORMAL = 0;
    public static int STATUS_INVINCIBLE = -1;
    public static int STATUS_DEAD = -2;

    public float cooldown = 6f;
    public float fallSpeed = 2.5f;
    public float riseSpeed = 0.3f;
    public float maxSpeed = 2f;
    public float minSpeed = 0.1f;
    public float startSpeed = 0.5f;
    public float randomSpeedRange = 0.1f;
    public float randomTime = 1.5f;
    public int maxHealth = 5;
    [Range(0f, 4.5f)]  public float upperHorizontalcalLimit = 4.5f;
    [Range(-4.5f, 0f)] public float lowerHorizontalLimit = -4.5f;

    public int id = 0;
    public int currentHealth;

    public float currentSpeed;
    private int status;
    public float movementDirection;
    private float randomValue;
    private float spawnTime;

    void Start()
    {
        currentSpeed = startSpeed;
        status = STATUS_INVINCIBLE;
        movementDirection = 1f;
        currentHealth = maxHealth;
        spawnTime = Time.time;

        StartCoroutine(UpdateSpeed());
    }

    void Update()
    {
        Movement();
        if (Time.time - spawnTime >= cooldown && status != STATUS_NORMAL)
        {
            status = STATUS_NORMAL;
        }
    }

    private void Movement()
    {
        if (transform.position.y > MainGameTracker.FLOOR_LEVEL)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime * MainGameTracker.GAME_SPEED;
        }
        else
        {
            if (transform.position.y < MainGameTracker.FLOOR_LEVEL - riseSpeed)
            {
                transform.position += Vector3.up * riseSpeed * Time.deltaTime * MainGameTracker.GAME_SPEED;
            }
            transform.position += Vector3.right * currentSpeed * Time.deltaTime * MainGameTracker.GAME_SPEED * movementDirection;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, lowerHorizontalLimit, upperHorizontalcalLimit),
            transform.position.y, transform.position.z);
    }

    public void GetHit()
    {
        currentHealth--;
        if(currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(this.gameObject);
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
        else if(otherObject.GetComponent<Raindrop>())
        {
            otherObject.GetComponent<Raindrop>().Kill();
            if (status == STATUS_NORMAL)
            {
                GetHit();
            }
            print(id + " hit by raindrop / HP = " + currentHealth);
        }
        else if(otherObject.layer == 9) movementDirection *= -1f;
    }

    void OnTriggerStay2D(Collider2D collide)
    {
        GameObject otherObject = collide.gameObject;
        if (otherObject.GetComponent<Cactus>())
        {
            Cactus otherCactus = otherObject.GetComponent<Cactus>();

            if (id < otherCactus.id)
            {
                movementDirection = otherCactus.movementDirection * -1f;
            }
        }
    }

    IEnumerator UpdateSpeed()
    {
        while(true)
        {
            currentSpeed += Random.Range(-randomSpeedRange, randomSpeedRange);
            currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
            yield return new WaitForSeconds(randomTime * MainGameTracker.GAME_SPEED);
        }
    }
}
