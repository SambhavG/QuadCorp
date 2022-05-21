using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndScreenScript : MonoBehaviour
{
    public void returnToMenuPressed() {
        PlayerPrefs.SetInt("SkipToLevelsScreen", 1);
        SceneManager.LoadScene("MainMenu");
    }
}
