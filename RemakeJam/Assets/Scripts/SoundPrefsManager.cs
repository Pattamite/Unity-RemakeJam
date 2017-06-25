using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class SoundPrefsManager : MonoBehaviour
{

    public Image musicButton;
    public Image soundButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    void Start ()
    {
        KeysSetup();
        SetSprite();
    }


    private void KeysSetup()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsKeys.IS_SOUND_ON))
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.IS_SOUND_ON, 1);
        }
        if (!PlayerPrefs.HasKey(PlayerPrefsKeys.IS_MUSIC_ON))
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.IS_MUSIC_ON, 1);
        }
    }

    private void SetSprite()
    {
        if(PlayerPrefs.GetInt(PlayerPrefsKeys.IS_SOUND_ON) == 1) soundButton.sprite = soundOnSprite;
        else soundButton.sprite = soundOffSprite;

        if (PlayerPrefs.GetInt(PlayerPrefsKeys.IS_MUSIC_ON) == 1) musicButton.sprite = musicOnSprite;
        else musicButton.sprite = musicOffSprite;
    }
	
	public static bool IsSoundOn()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.IS_SOUND_ON) == 1;
    }

    public static bool IsMusicOn()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.IS_MUSIC_ON) == 1;
    }

    public void ToggleSound()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.IS_SOUND_ON, (PlayerPrefs.GetInt(PlayerPrefsKeys.IS_SOUND_ON) + 1) % 2);
        SetSprite();
    }

    public void ToggleMusic()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.IS_MUSIC_ON, (PlayerPrefs.GetInt(PlayerPrefsKeys.IS_MUSIC_ON) + 1) % 2);
        SetSprite();
    }
}
