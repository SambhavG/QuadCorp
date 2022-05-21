using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartScreenScript : MonoBehaviour
{
    public GameObject crosshair;

    public void startPressed() {
        //Hide self
        gameObject.SetActive(false);

        //Show crosshair
        crosshair.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}
