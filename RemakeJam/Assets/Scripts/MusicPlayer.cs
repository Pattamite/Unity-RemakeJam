using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static AudioSource AUDIOSOURCE;
    private bool lastFromPrefs;
    private static bool IS_GAME_PAUSE;
	
	void Start ()
    {
        IS_GAME_PAUSE = false;
        AUDIOSOURCE = this.gameObject.GetComponent<AudioSource>();
        AUDIOSOURCE.Play();
        lastFromPrefs = SoundPrefsManager.IsMusicOn();
        if (!lastFromPrefs) AUDIOSOURCE.Pause();
    }

    void Update()
    {
        if (lastFromPrefs && (!SoundPrefsManager.IsMusicOn() || IS_GAME_PAUSE))
        {
            Pause();
            lastFromPrefs = false;
        }
        else if (!lastFromPrefs && SoundPrefsManager.IsMusicOn() && !IS_GAME_PAUSE)
        {
            UnPause();
            lastFromPrefs = true;
        }
    }


    public static void Pause()
    {
        AUDIOSOURCE.Pause();
    }

    public static void UnPause()
    {
        AUDIOSOURCE.UnPause();
    }

    public static void Stop()
    {
        AUDIOSOURCE.Stop();
    }

    public static void GamePause()
    {
        IS_GAME_PAUSE = true;
    }

    public static void GameResume()
    {
        IS_GAME_PAUSE = false;
    }

}
