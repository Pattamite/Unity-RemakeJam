using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTPThunderSpawner : MonoBehaviour {

    public Object thunder;

    [Range(0f, 3f)]
    public float spawnDelay = 3f;
    [Range(7f, 9f)]
    public float verticalSpawnLocation = 9f;

    private float lastSpawnTime;

    void Start()
    {
    }

    void Update()
    {
        if (spawnDelay > 0f)
        {
            if (Time.time - lastSpawnTime >= (spawnDelay / MainGameTracker.GAME_SPEED))
            {
                lastSpawnTime = Time.time;
                GameObject newRaindrop = Instantiate(thunder
                                                     , new Vector3(this.transform.position.x , verticalSpawnLocation, 0),
                                                     Quaternion.identity) as GameObject;
                newRaindrop.transform.SetParent(this.transform);
            }
        }
    }

    
}
