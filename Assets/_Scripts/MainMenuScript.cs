using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject levelsMenu;
    public GameObject controlsMenu;
    public GameObject optionsMenu;

    //star sprites
    public Sprite yellowStar;
    public Sprite grayStar;

    //Options checkboxes
    public Toggle colorblindToggle;


    public void LoadLevelsScreen() {
        //set self inactive
        gameObject.SetActive(false);
        //set levelsMenu active
        levelsMenu.SetActive(true);

        //set stars of levelMenu
        //Loop through each level
        GameObject levelButtons = levelsMenu.transform.Find("LevelButtons").gameObject;
        for (int i = 0; i < levelButtons.transform.childCount; i++) {
            //get levelButton
            GameObject levelButton = levelButtons.transform.GetChild(i).gameObject;
            //get stars
            int stars = PlayerPrefs.GetInt("Level" + (i+1) + "Stars", 0);
            //set stars
            for (int j = 0; j < 3; j++) {
                if (j < stars) {
                    levelButton.transform.Find("star"+(j+1)).gameObject.GetComponent<Image>().sprite = yellowStar;
                } else {
                    levelButton.transform.Find("star"+(j+1)).gameObject.GetComponent<Image>().sprite = grayStar;
                }
            }
        }
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void toggleColorblind() {
        if (colorblindToggle.isOn) {
            PlayerPrefs.SetInt("Colorblind", 1);
        } else {
            PlayerPrefs.SetInt("Colorblind", 0);
        }
    }
    

    public void Awake() {
        //Skip to the levels screen if the player quits a level
        if (PlayerPrefs.GetInt("SkipToLevelsScreen", 0) == 1) {
            PlayerPrefs.SetInt("SkipToLevelsScreen", 0);
            LoadLevelsScreen();
        }

        //Set options toggle on if colorblind is on
        if (PlayerPrefs.GetInt("Colorblind", 0) == 1) {
            colorblindToggle.isOn = true;
        }
    }
    
}
