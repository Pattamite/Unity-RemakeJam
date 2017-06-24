using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FertSpawner : MonoBehaviour
{
    public GameObject umbrella;
    public GameObject fert;
    public GameObject fertBar;

    public Color normalBarColor;
    public Color notEnoughBarColor;

    public bool mobileControl = true;

    public float startCharges = 2f; // 1 = 100% for one drop
    public float refillRate = 0.02f;
    public float maxCharges = 5f;
    public float chargesUse = 1f;
    public float lastDropped;
    public float cooldown = 0.06f;


    public float currentCharges;
    // Use this for initialization
    void Start ()
    {
        currentCharges = startCharges;
        lastDropped = Time.time;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tryDrop();
        }
<<<<<<< HEAD
        if (mobileControl) MobileInput();
            
=======

>>>>>>> refs/heads/pr/4
        refill(refillRate);
        fertBar.GetComponent<HealthBar>().SetValue(currentCharges / maxCharges);
        setBarColor();
    }

    private void MobileInput()
    {
        if (CrossPlatformInputManager.GetButtonDown("FertScreen")) tryDrop();
    }

    private void setBarColor()
    {
        if (currentCharges < chargesUse) fertBar.GetComponent<HealthBar>().SetColor(notEnoughBarColor);
        else fertBar.GetComponent<HealthBar>().SetColor(normalBarColor);
    }

    private void tryDrop()
    {
        if (currentCharges >= chargesUse && umbrella.GetComponent<Umbrella>().getStun() == false)
        {
            GameObject newFert = Instantiate(fert, new Vector3(umbrella.transform.position.x, umbrella.transform.position.y, 0),
                                             Quaternion.identity) as GameObject;
            newFert.transform.parent = this.transform;
            currentCharges -= chargesUse;

            lastDropped = Time.time;
        }
        // TODO: Add not enough charges sfx
    }

    private void refill (float amount)
    {
        if (currentCharges < maxCharges)
        {
            currentCharges += amount * Time.deltaTime * MainGameTracker.GAME_SPEED;
            if (currentCharges > maxCharges)
            {
                currentCharges = maxCharges;
            }
        }
    }
}
