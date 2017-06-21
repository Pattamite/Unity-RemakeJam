using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raindrop : MonoBehaviour
{

    public float dropSpeed = 0.8f;


    void Start()
    {

    }


    void Update()
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
        else if(otherObject.GetComponent<Umbrella>())
        {
            MainGameTracker.AddScoreRain();
            Kill();
        }
    }
}
