using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fert : MonoBehaviour
{
    public float dropSpeed = 0.8f;
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
        if(otherObject.layer == 8)
        {
            Kill();
        }
        if(otherObject.GetComponent<Cactus>())
        {
            otherObject.GetComponent<Cactus>().Heal();
            MainGameTracker.AddScoreFert();
            Kill();
        }
    }
}
