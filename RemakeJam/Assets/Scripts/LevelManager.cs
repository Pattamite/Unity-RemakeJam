using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }

    public static void LoadSceneStatic(int level)
    {
        SceneManager.LoadScene(level);
    }
}
