using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raindrop : MonoBehaviour
{

    public static float DROP_SPEED = 0.8f;


    void Start()
    {

    }


    void Update()
    {
        transform.position += Vector3.down * DROP_SPEED * Time.deltaTime * MainGameTracker.GAME_SPEED;
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        GameObject otherObject = collide.gameObject;
        if(otherObject.layer == 8)
        {
            Destroy(this.gameObject);
        }
    }
}
