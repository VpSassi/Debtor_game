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
    public bool unlocked = false;
    public GameObject lockIcon;

    void Start() {
        levelSelect = GameObject.Find("Level_select").GetComponent<Level_select_script>();
        title = gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
        title.text = "LEVEL " + levelData.name;
        unlocked = levelData.unlocked;

    }

    void Update() {
        CheckDisableButton();
        if (levelSelect.selectedLevel.index == levelData.index && unlocked) {
            GetComponent<Button>().Select();
        }
    }
    
    public void SelectLevel() {
        if (unlocked) {
            buttonAudio.clip = clips[1];
            buttonAudio.Play();
            levelSelect.selectedLevel = levelData;
        }
    }

    void CheckDisableButton() {
        if (!unlocked) {
            GetComponent<Button>().enabled = false;
            lockIcon.SetActive(true);
        } else {
            GetComponent<Button>().enabled = true;
            lockIcon.SetActive(false);
        }
    }
}