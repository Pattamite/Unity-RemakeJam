using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTPRainSpawner : MonoBehaviour {

    public Object raindrop;

    [Range(0f, 2f)]
    public float spawnDelay = 0.1f;
    [Range(0f, 4.5f)]
    public float spawnRange = 4f;
    [Range(7f, 9f)]
    public float verticalSpawnLocation = 9f;

    private float lastSpawnTime;
    private float sigma;

    void Start()
    {
        SetupVar();
    }

    void Update()
    {
        sigma = spawnRange / 3;
        if (spawnDelay > 0f)
        {
            if (Time.time - lastSpawnTime >= (spawnDelay / MainGameTracker.GAME_SPEED))
            {
                lastSpawnTime = Time.time;
                GameObject newRaindrop = Instantiate(raindrop
                                                     , new Vector3(this.transform.position.x +  NormalDistributionRandom(), verticalSpawnLocation, 0),
                                                     Quaternion.identity) as GameObject;
                newRaindrop.transform.SetParent(this.transform);
            }
        }
    }

    private void SetupVar()
    {
        sigma = spawnRange / 3;
        lastSpawnTime = Time.time;
    }

    private float NormalDistributionRandom()
    {
        float r1;
        float r2;
        float S;

        do
        {
            r1 = 2f * Random.value - 1f;
            r2 = 2f * Random.value - 1f;
            S = r1 * r1 + r2 * r2;
        } while (S >= 1.0f);

        return Mathf.Clamp(r1 * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S) * sigma,
                           -spawnRange, spawnRange);
    }
}
