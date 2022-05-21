using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public void resumePressed() {
        //Hide self
        gameObject.SetActive(false);
        //Set paused to false
        GameControllerScript.paused = false;
        //Unlock camera
        GameControllerScript.cameraLocked = false;
        //Relock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void quitPressed() {
        //Return to main menu
        PlayerPrefs.SetInt("SkipToLevelsScreen", 1);
        SceneManager.LoadScene("MainMenu");
    }
}
