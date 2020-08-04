using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
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
    public bool alarmOn = false;
    public int[] timerScoreSteps = new int[5];
    private string json;
    private Level_select_script.LevelsData levelsData;
    public bool nextLevelBtnPressed = false;
    private GameObject Dima;

    void Awake() {
        currentLevel = SceneManager.GetActiveScene().name;
        ded.SetActive(false);
        win.SetActive(false);

        bulletsTotal = 30;
        currentBullets = bulletsTotal;

        string json = File.ReadAllText(Application.streamingAssetsPath + "/Level_data.json");

        levelsData = JsonUtility.FromJson<Level_select_script.LevelsData>(json);
        Level_select_script.LevelData[] levelData = levelsData.levelData;

        Level_select_script.LevelsData newLevelsData = levelsData;
        newLevelsData.levelData[levelsData.latestLevel].attempts++;
        File.WriteAllText(Application.streamingAssetsPath + "/Level_data.json", JsonUtility.ToJson(newLevelsData, true));

        Dima = GameObject.Find("Dima");
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
            Level_select_script.LevelsData newLevelsData = levelsData;
            newLevelsData.levelData[levelsData.latestLevel].attempts++;
            File.WriteAllText(Application.streamingAssetsPath + "/Level_data.json", JsonUtility.ToJson(newLevelsData, true));

            SceneManager.LoadScene(currentLevel);
        }
    }

    void Timer() { // timer that counts up when scene is loaded
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
        win_helper_text.text =  "TIME" + "\n \n - " + minutes + ":" + seconds + ":" + Mathf.Floor(milliseconds) + "\n \n" + // TODO: formatting (00:00:00)
                                "BULLETS" + "\n \n - " + currentBullets + " / " + bulletsTotal + "\n \n \n" +
                                "SCORE" + "\n \n - " + finalScore;
        
        Level_select_script.LevelsData newLevelsData = levelsData;
        newLevelsData.levelData[levelsData.latestLevel].topScore = finalScore;
        if (levelsData.latestLevel != 4) {
            newLevelsData.levelData[levelsData.latestLevel + 1].unlocked = true;
        }
        File.WriteAllText(Application.streamingAssetsPath + "/Level_data.json", JsonUtility.ToJson(newLevelsData, true));
    }

    public void ReturnToMainMenu() { // go back to main menu
        SceneManager.LoadScene("Main_menu");
    }

    public void NextLevel() { // load the next scene in order after delay
        Level_select_script.LevelsData newLevelsData = levelsData;
        newLevelsData.latestLevel++;
        File.WriteAllText(Application.streamingAssetsPath + "/Level_data.json", JsonUtility.ToJson(newLevelsData, true));
        nextLevelBtnPressed = true;
        Dima.SetActive(false);

        Invoke("LoadNextLevel", 0.6f);
    }

    void LoadNextLevel() { // level load func
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
