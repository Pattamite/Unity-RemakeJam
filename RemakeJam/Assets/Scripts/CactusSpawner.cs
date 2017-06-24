using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusSpawner : MonoBehaviour
{
    public GameObject cacti;
    public GameObject umbrella;
    public GameObject cactus;

    public int level; // level + 1 = # of cacti
    public float maxSpawnRange = 2f;
    public float maxRange = 1f;
    public float verticalSpawnLocation = 9f;
    public float spawnTime = 0.5f;

    private float lastSpawnTime;
    private float sigma;
    private float centerX;
    private int nextID;

    void Start ()
    {
        SetupVar();
    }

    void Update ()
    {
        if (spawnTime > 0f && Time.time - lastSpawnTime >= (spawnTime / MainGameTracker.GAME_SPEED))
        {
            if (cacti.transform.childCount <= level)
            {
                // Spawn cactus
                centerX = umbrella.transform.position.x * 1f;
                GameObject newCactus = Instantiate(cactus, new Vector3(Mathf.Clamp(centerX + NormalDistributionRandom(-maxRange, maxRange), -maxSpawnRange, maxSpawnRange), verticalSpawnLocation, 0), Quaternion.identity) as GameObject;
                newCactus.transform.SetParent(cacti.transform);
                newCactus.GetComponent<Cactus>().id = nextID;
                nextID++;
            }
        }
    }

    private void SetupVar()
    {
        level = MainGameTracker.CURRENT_LEVEL;
        sigma = maxSpawnRange / 3;
        lastSpawnTime = Time.time;
        nextID = cacti.transform.childCount;
    }

    private float NormalDistributionRandom(float lower, float upper)
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
                           lower, upper);
    }
}
