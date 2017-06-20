using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameTracker : MonoBehaviour
{
    public static float GAME_SPEED = 1.0f;
    [Range(0.0f, 10.0f)] public float gameSpeed = 1.0f;

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        GAME_SPEED = gameSpeed;
    }
}
