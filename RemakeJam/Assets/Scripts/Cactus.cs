using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cactus : MonoBehaviour
{
    public static int STATUS_SPAWNING = 1;
    public static int STATUS_NORMAL = 0;
    public static int STATUS_INVINCIBLE = -1;
    public static int STATUS_DEAD = -2;

    public Object cactusHealthBar;
    private GameObject canvas;

    public float cooldownAfterHit = 1f;
    public float fallSpeed = 2.5f;
    public float submergeLevel = 0.2f;
    public float maxSpeed = 2f;
    public float minSpeed = 0.1f;
    public float startSpeed = 0.5f;
    public float randomSpeedRange = 0.1f;
    public float randomTime = 1.5f;
    public int maxHealth = 5;
    public int healAmount = 2;
    [Range(0f, 4.5f)]  public float upperHorizontalcalLimit = 4.5f;
    [Range(-4.5f, 0f)] public float lowerHorizontalLimit = -4.5f;

    public int id = 0;
    public int currentHealth;

    public float currentSpeed;
    private int status;
    public float movementDirection;
    private float randomValue;
    private float lastHitTime;

    public Color normalColor;
    public Color blinkColor;
    private bool isBlink = false;
    private float lastBlinkTime;
    [Range(0.1f, 1f)] public float blinkDelay = 0.2f;

    private GameObject healthBar;
    [Range(-3f, 0f)] public float healthBarYOffSet = -1.5f;

    void Start()
    {
        SetupVar();
        SetupHealthBar();
        StartCoroutine(UpdateSpeed());
    }

    void Update()
    {
        Movement();
        StatusChecking();
    }

    private void SetupVar()
    {
        currentSpeed = startSpeed;
        status = STATUS_SPAWNING;
        movementDirection = 1f;
        currentHealth = maxHealth;
        lastHitTime = Time.time;
        lastBlinkTime = Time.time;
        canvas = GameObject.Find("Canvas");
    }

    private void SetupHealthBar()
    {
        healthBar = Instantiate(cactusHealthBar,
            new Vector3(this.transform.position.x, this.transform.position.y + healthBarYOffSet, this.transform.position.z),
                                Quaternion.identity) as GameObject;

        healthBar.transform.SetParent(canvas.transform);
        SetHealthBarValue();
    }

    private void SetHealthBarValue()
    {
        healthBar.GetComponent<HealthBar>().SetValue((float)currentHealth / (float)maxHealth);
    }

    private void Movement()
    {
        if (transform.position.y > MainGameTracker.FLOOR_LEVEL)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime * MainGameTracker.GAME_SPEED;
        }
        else
        {
            if (status == STATUS_SPAWNING)
            {
                status = STATUS_NORMAL;
                StopBlinking();
            }
            if (transform.position.y < MainGameTracker.FLOOR_LEVEL)
            {
                transform.position += Vector3.up * MainGameTracker.RISING_SPEED * Time.deltaTime * MainGameTracker.GAME_SPEED;
            }
            transform.position += Vector3.right * currentSpeed * Time.deltaTime * MainGameTracker.GAME_SPEED * movementDirection;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, lowerHorizontalLimit, upperHorizontalcalLimit),
            transform.position.y, transform.position.z);

        if (transform.position.x == lowerHorizontalLimit) movementDirection = 1f;
        if (transform.position.x == upperHorizontalcalLimit) movementDirection = -1f;

        healthBar.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + healthBarYOffSet, this.transform.position.z);
    }

    private void StatusChecking()
    {
        if(status == STATUS_SPAWNING || status == STATUS_INVINCIBLE)
        {
            Blinking();
        }
        if(status == STATUS_INVINCIBLE)
        {
            if(Time.time - lastHitTime >= cooldownAfterHit / MainGameTracker.GAME_SPEED)
            {
                status = STATUS_NORMAL;
                StopBlinking();
            }
        }
    }

    public void GetHit()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            Kill();
        }
        else
        {
            status = STATUS_INVINCIBLE;
            lastHitTime = Time.time;
            SetHealthBarValue();
        }
    }

    public void Heal()
    {
        currentHealth = currentHealth + healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        SetHealthBarValue();
    }

    public void Kill()
    {
        MainGameTracker.LifeLost();
        Destroy(healthBar);
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
            //print(id + " hit by raindrop / HP = " + currentHealth);
        }
        else if(otherObject.GetComponent<Fert>())
        {
            otherObject.GetComponent<Fert>().Kill();
            if (status == STATUS_NORMAL || status == STATUS_INVINCIBLE)
            {
                Heal();
            }
            print(id + "said cactus never died / HP = " + currentHealth);
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

    void Blinking()
    {
        if(Time.time - lastBlinkTime >= blinkDelay / MainGameTracker.GAME_SPEED)
        {
            lastBlinkTime = Time.time;
            if(isBlink)
            {
                this.GetComponentInChildren<SpriteRenderer>().color = normalColor;
                isBlink = false;
            }
            else
            {
                this.GetComponentInChildren<SpriteRenderer>().color = blinkColor;
                isBlink = true;
            }
        }
    }

    void StopBlinking()
    {
        this.GetComponentInChildren<SpriteRenderer>().color = normalColor;
        isBlink = false;
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
