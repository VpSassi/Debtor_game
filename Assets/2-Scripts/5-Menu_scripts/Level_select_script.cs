using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class Level_select_script : MonoBehaviour
{
    public Image levelImage;
    public Text levelName;
    public Text levelDesc;
    public Text attempts;
    public Text highScore;
    public GameObject levelSelection;
    public GameObject levelButton;
    public LevelData selectedLevel;
    private LevelData oldSelectedLevel;
    public LevelData[] levels = new LevelData[100];
    private int foundLevels = 0;
    public AudioSource levelSelectAudio;
    public AudioClip[] clips;
    public bool playLevel = false;
    private float levelChangeTimer = 0;

    void Awake()
    {
        InitializeLevelData();
    }

    void Update() {
        if (selectedLevel != oldSelectedLevel) {
            UpdateLevelInfo();
        }
        if (playLevel) {
            GoToLevel();
        }
    }

    void InitializeLevelData() {
        string json = File.ReadAllText("Assets/2-Scripts/5-Menu_scripts/Level_data.json");

        LevelsData levelsData = JsonUtility.FromJson<LevelsData>(json);
        LevelData[] levelData = levelsData.levelData;

        int i = 0;
        foreach (LevelData item in levelData) {
            LevelData levelItem = new LevelData(); 
            levelItem.index = item.index;
            levelItem.name = item.name;
            levelItem.description = item.description;
            levelItem.attempts = item.attempts;
            levelItem.topScore = item.topScore;
            levelItem.imageUrl = item.imageUrl;

            levels[i] = levelItem;
            i++;
            foundLevels++;
        }

        selectedLevel = levels[0];

        UpdateLevelSelection();
    }

    void UpdateLevelInfo() { // updates selected level displayed info
        Sprite newImage;

        if (!File.Exists("Assets/Resources/" + selectedLevel.imageUrl + ".png")) {
            newImage = Resources.Load<Sprite>("1-Level_pictures/T");
        } else {
            newImage = Resources.Load<Sprite>(selectedLevel.imageUrl);
        }

        oldSelectedLevel = selectedLevel;
        levelName.text = "LEVEL " + selectedLevel.name;
        levelDesc.text = selectedLevel.description;
        attempts.text = selectedLevel.attempts.ToString();
        highScore.text = selectedLevel.topScore.ToString();
        levelImage.overrideSprite = newImage;
    }

    void UpdateLevelSelection() { // sets the height of the scrollable background and generates level buttons
        float topMargin = 0;

        var levelSelectionRectTransform = levelSelection.GetComponent<RectTransform>();
        levelSelectionRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 25f * (foundLevels + 1));
        // levelSelectionRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 25f * 101); // FOR TESTING

        if (levelSelectionRectTransform.sizeDelta.y < 218.0284f) {
            levelSelectionRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 218.0284f);
        }

        foreach (LevelData item in levels) {
            if (item?.name != "") {
            print(item.index); // KILL
            GameObject newLevelButton = Instantiate(levelButton) as GameObject;
            Level_object_script newLevelButtonScript = newLevelButton.GetComponent<Level_object_script>();

            newLevelButton.transform.parent = levelSelection.transform;
            newLevelButton.transform.localPosition = new Vector3(0f, topMargin + (levelSelectionRectTransform.sizeDelta.y / 2) - 25, 0f);
            newLevelButtonScript.levelData = item;

            topMargin = topMargin - 25;
            }
        }
    }

    public void PlayButtonPress() { // extra function for the play button
        playLevel = true;

        levelSelectAudio.clip = clips[0];
        levelSelectAudio.Play();
    }

    void GoToLevel() { // load selected level
        levelChangeTimer += Time.deltaTime;

        if (levelChangeTimer > 1.5f && playLevel) {
            playLevel = false;

            // if (SceneManager.GetSceneByName(selectedLevel.name.ToString()).IsValid()) { // FIX THIS
            SceneManager.LoadScene(selectedLevel.name);
            // }
        }
    }

    // !!! REMOVE FROM FINAL BUILD !!!
    public void CalibrateLevelsData() { // calibrate levels scenes wit hthe JSON, so that it doesnt need to be done manually
        DirectoryInfo dir = new DirectoryInfo("Assets/1-Scenes/2-Map_scenes");
        FileInfo[] info = dir.GetFiles("*.unity");

        LevelData[] newLevelData = new LevelData[info.Length];
        int i = 0;
        
        foreach (FileInfo file in info) {
            string name = file.Name.Replace(".unity", "");
            LevelData newLevel = new LevelData();

            newLevel.index = i;
            newLevel.name = name;
            newLevel.description = "";
            newLevel.attempts = 0;
            newLevel.topScore = 0;
            newLevel.imageUrl = "1-Level_pictures/" + name;

            newLevelData[i] = newLevel;
            i++;
        }

        LevelsData newLevelsData = new LevelsData();
        newLevelsData.levelData = newLevelData;

        File.WriteAllText("Assets/2-Scripts/5-Menu_scripts/Level_data.json", JsonUtility.ToJson(newLevelsData));

        // TODO: add the scenes to be built in code as well so you don't have to do it manually

        SceneManager.LoadScene("Main_menu");
    }
    // !!! REMOVE FROM FINAL BUILD !!!

    [System.Serializable]
    public class LevelsData {
        public LevelData[] levelData;
    }
    
    [System.Serializable]
    public class LevelData {
        public int index;
        public string name;
        public string description;
        public int attempts;
        public int topScore;
        public string imageUrl;
    }
}
