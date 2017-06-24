using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static AudioSource AUDIOSOURCE;
	
	void Start ()
    {
        AUDIOSOURCE = this.gameObject.GetComponent<AudioSource>();
        AUDIOSOURCE.Play();
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
}
