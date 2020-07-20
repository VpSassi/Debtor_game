using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_object_script : MonoBehaviour
{
    public Level_select_script levelSelect;
    private Text title;
    public Level_select_script.LevelData levelData;
    public AudioSource buttonAudio;
    public AudioClip[] clips;

    void Start() {
        levelSelect = GameObject.Find("Level_select").GetComponent<Level_select_script>();
        title = gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
        title.text = "LEVEL " + levelData.name;
    }

    void Update() {
        if (levelSelect.selectedLevel.index == levelData.index) {
            GetComponent<Button>().Select();
        }
    }
    
    public void SelectLevel() {
        buttonAudio.clip = clips[1];
        buttonAudio.Play();
        levelSelect.selectedLevel = levelData;
    }
}