using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    public float dropSpeed = 1.2f;
    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        transform.position += Vector3.down * dropSpeed * Time.deltaTime * MainGameTracker.GAME_SPEED;
    }

    public void Kill()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        GameObject otherObject = collide.gameObject;
        if (otherObject.layer == 8)
        {
            Kill();
        }
        else if (otherObject.GetComponent<Umbrella>())
        {
            Umbrella collided = otherObject.GetComponent<Umbrella>();
            collided.stunned();
            Kill();
        }
    }
}
