using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManuManager : LevelManager
{

	// Use this for initialization
	void Start () {
		if(!PlayerPrefs.HasKey(PlayerPrefsKeys.HIGH_SCORE))
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.HIGH_SCORE, -1);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
