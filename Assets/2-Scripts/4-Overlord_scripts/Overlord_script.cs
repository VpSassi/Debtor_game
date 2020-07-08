using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using HajyGames;

public class Overlord_script : MonoBehaviour
{
    public bool playerDead = false;
    public bool stageWin = false;
    public string currentLevel;
    public GameObject ded;
    public GameObject win;
    public Text win_helper_text;
    public Text bullets_info;
    public int bulletsTotal;
    public int currentBullets;
    public Text timer;
    public float milliseconds = 0;
    public float seconds = 0;
    public float minutes = 0;
    public int timerScore = 1000;

    void Awake() {
        currentLevel = SceneManager.GetActiveScene().name;
        ded.SetActive(false);
        win.SetActive(false);

        bulletsTotal = 30;
        currentBullets = bulletsTotal;
    }

    void Update() {
        
        // TODO: move this to Class1.cs
        if (Input.GetKeyDown(KeyCode.X)) { // sets universal stop bool, that can be used in other functions for testing
            GlobalVariables.stop = !GlobalVariables.stop;

            // Currently used in:
            // * Dima_script --> RotateSprite()
            // * Crosshair_script --> MoveCrosshair()
        }

        if (playerDead && !stageWin) {
            ded.SetActive(true);
        }

        if (stageWin) {
            win.SetActive(true);
            WinHelperText();
        }

        Respawn();
        bullets_info.text = "Bullets - " + currentBullets + " / " + bulletsTotal;

        if (!stageWin || playerDead) {
            Timer();
        }
    }

    public void Respawn() { // respawn when R key is pressed
        if (Input.GetKeyDown(KeyCode.R) /* && !stageWin */) {
            SceneManager.LoadScene(currentLevel);
        }
    }

    void Timer() { // time rthat counts up when scene is loaded
        milliseconds += 1000 * Time.deltaTime;

        if (milliseconds >= 1000) {
            seconds++;
            milliseconds = 0;
            timerScore = timerScore >= 1 ? timerScore - 10 : timerScore;
        }

        if (seconds >= 60) {
            minutes++;
            seconds = 0;
            milliseconds = 0;
        }

        timer.text = minutes + ":" + seconds + ":" + Mathf.Floor(milliseconds); // TODO: formatting (00:00:00)
    }

    void WinHelperText() { // set win helper text
        int finalScore = 1000 + timerScore + (currentBullets * 30); // TODO: finalized score calculation
        win_helper_text.text =  "TIME - " + minutes + ":" + seconds + ":" + Mathf.Floor(milliseconds) + "\n" + // TODO: formatting (00:00:00)
                                "BULLETS - " + currentBullets + " / " + bulletsTotal + "\n \n" +
                                "SCORE - " + finalScore;
    }
}
