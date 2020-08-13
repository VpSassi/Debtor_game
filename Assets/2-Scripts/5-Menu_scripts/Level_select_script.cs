using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using HajyGames;

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
    private LevelsData levelsData;

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

        string json = File.ReadAllText(Application.streamingAssetsPath + "/Level_data.json");

        levelsData = JsonUtility.FromJson<LevelsData>(json);
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
            levelItem.unlocked = item.unlocked;

            levels[i] = levelItem;
            i++;
            foundLevels++;
        }

        selectedLevel = levels[levelsData.latestLevel];

        UpdateLevelSelection();
    }

    void UpdateLevelInfo() { // updates selected level displayed info
        Sprite newImage;
        Texture2D newTexture = new Texture2D(2, 2);

        if (!File.Exists(Application.streamingAssetsPath + "/" + selectedLevel.imageUrl + ".png")) {
            newTexture.LoadImage(File.ReadAllBytes(Application.streamingAssetsPath + "/1-Level_pictures/T.png"));
        } else {
            newTexture.LoadImage(File.ReadAllBytes(Application.streamingAssetsPath + "/" + selectedLevel.imageUrl + ".png"));
        }

        newImage = Sprite.Create(newTexture, new Rect(0.0f, 0.0f, newTexture.width, newTexture.height), new Vector2(0, 0), 100.0f);

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
            GameObject newLevelButton = Instantiate(levelButton) as GameObject;
            Level_object_script newLevelButtonScript = newLevelButton.GetComponent<Level_object_script>();

            newLevelButton.transform.parent = levelSelection.transform;
            newLevelButton.transform.localPosition = new Vector3(0f, topMargin + (levelSelectionRectTransform.sizeDelta.y / 2) - 20, 0f);
            newLevelButtonScript.levelData = item;

            topMargin = topMargin - 20;
            }
        }
    }

    public void PlayButtonPress() { // extra function for the play button
        playLevel = true;

        LevelsData newLevelsData = new LevelsData();
        newLevelsData.levelData = levelsData.levelData;
        newLevelsData.latestLevel = selectedLevel.index;
        File.WriteAllText(Application.streamingAssetsPath + "/Level_data.json", JsonUtility.ToJson(newLevelsData, true));

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

    // !!! REMOVE FROM FINAL BUILD !!! vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv ******
    public void CalibrateLevelsData() { // calibrate levels scenes wit hthe JSON, so that it doesnt need to be done manually
        DirectoryInfo dir = new DirectoryInfo("Assets/1-Scenes/2-Map_scenes");
        FileInfo[] info = dir.GetFiles("*.unity");

        LevelData[] newLevelData = new LevelData[info.Length];
        Settings newSettings = new Settings();

        print(info[0].Name.Replace(".unity", ""));

        Array.Sort(info, delegate(FileInfo item1, FileInfo item2) {
            return item1.Name.Length.CompareTo(item2.Name.Length);
        });
        
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
            newLevel.unlocked = i == 0 ? true : false;

            newLevelData[i] = newLevel;
            i++;
        }

        LevelsData newLevelsData = new LevelsData();
        newLevelsData.levelData = newLevelData;
        newLevelsData.latestLevel = 0;
        newLevelsData.settings = newSettings;

        File.WriteAllText(Application.streamingAssetsPath + "/Level_data.json", JsonUtility.ToJson(newLevelsData,true));

        SceneManager.LoadScene("Main_menu");
    }
    // !!! REMOVE FROM FINAL BUILD !!! ^^^^^^^^^^^^^^^^^^^^^^^^^^^^ ******

    [System.Serializable]
    public class LevelsData {
        public LevelData[] levelData;
        public int latestLevel;
        public Settings settings;
    }
    
    [System.Serializable]
    public class LevelData {
        public int index;
        public string name;
        public string description;
        public int attempts;
        public int topScore;
        public string imageUrl;
        public bool unlocked;
    }

    [System.Serializable]
    public class Settings {

        // TODO: settings
    }
}
