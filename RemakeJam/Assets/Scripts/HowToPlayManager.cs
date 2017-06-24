using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class HowToPlayManager : LevelManager
{
	void Update ()
    {
        if (CrossPlatformInputManager.GetButtonDown("MainMenu")) LoadScene(ScenesNumber.MAIN_MENU);
    }
}
