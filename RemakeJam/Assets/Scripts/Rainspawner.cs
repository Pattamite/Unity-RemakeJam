using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainspawner : MonoBehaviour
{
    public GameObject cacti;
    public Object raindrop;
    [Range(0f, 4.5f)] public float maxRangeFromCacti = 1f;
    [Range(0f, 2f)]   public float spawnDelay = 0.5f;
    [Range(0f, 4.5f)] public float spawnRange = 4f;
    [Range(7f, 9f)]   public float verticalSpawnLocation = 9f;
    
    private float lastSpawnTime;
    private float sigma;
    private int totalCacti;
    private int selectedCacti;
    private int currentCacti;
    private bool isSelected;
    private float cactiPosition;


	void Start ()
    {
        SetupVar();
    }
	
	void Update ()
    {
        sigma = maxRangeFromCacti / 3;
        if (spawnDelay > 0f)
        {
            if (Time.time - lastSpawnTime >= (spawnDelay / MainGameTracker.GAME_SPEED))
            {
                lastSpawnTime = Time.time;
                totalCacti = cacti.transform.childCount;
                selectedCacti = Random.Range(0, totalCacti);

                isSelected = false;
                currentCacti = 0;
                foreach (Transform child in cacti.transform)
                {
                    if (currentCacti == selectedCacti)
                    {
                        isSelected = true;
                        cactiPosition = child.position.x;
                        break;
                    }
                    else currentCacti++;
                }

                if(isSelected)
                {
                    GameObject newRaindrop = Instantiate(raindrop
                        , new Vector3(Mathf.Clamp(cactiPosition + NormalDistributionRandom(), -spawnRange, spawnRange), verticalSpawnLocation, 0),
                         Quaternion.identity) as GameObject;
                    newRaindrop.transform.parent = this.transform;
                }
            }
        }

	}

    private void SetupVar()
    {
        sigma = maxRangeFromCacti / 3;
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
                            -maxRangeFromCacti, maxRangeFromCacti);
    }
}
