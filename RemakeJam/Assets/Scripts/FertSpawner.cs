using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertSpawner : MonoBehaviour
{
    public GameObject umbrella;
    public GameObject fert;

    public float charges = 2f; // 1 = 100% for one drop
    public float refillRate = 0.001f;
    public float maxCharges = 5f;
    public float lastDropped;
    public float cooldown = 0.06f;
    public bool dropped = false;
    // Use this for initialization
    void Start ()
    {
        lastDropped = 0f;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            tryDrop();
        }
        else
        {
            dropped = false;
        }
        refill(refillRate);
    }
    private void tryDrop()
    {
        if (charges >= 1f && dropped == false)
        {
            GameObject newFert = Instantiate(fert, new Vector3(umbrella.transform.position.x, umbrella.transform.position.y, 0),
                                             Quaternion.identity) as GameObject;
            newFert.transform.parent = this.transform;
            charges = charges - 1f;
            lastDropped = Time.time;
            dropped = true;
        }
        // TODO: Add not enough charges sfx
    }

    private void refill (float amount)
    {
        if (charges < maxCharges)
        {
            charges = charges + amount;
            if (charges > maxCharges)
            {
                charges = maxCharges;
            }
        }
    }
}
