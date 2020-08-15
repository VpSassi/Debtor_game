using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter_End_script : MonoBehaviour
{
    private bool playerOnPlatform = false;
    private Dima_script dimaScript;
    private Overlord_script overlord;
    private GameObject wavesSpawn;
    private GameObject effect;
    public GameObject waves;
    public bool playWaves;
    private bool wavesPlayed = false;

    void Start()
    {
        dimaScript = GameObject.Find("Dima").GetComponent<Dima_script>();
        overlord = GameObject.Find("OVERLORD").GetComponent<Overlord_script>();
        wavesSpawn = transform.Find("Waves_spawn").gameObject;
        effect = transform.Find("Teleporter_effect").gameObject;

        effect.SetActive(false);
    }

    void Update()
    {
        CheckIfWin();

        if (overlord.nextLevelBtnPressed && !wavesPlayed) {
            SpawnWaves();
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            playerOnPlatform = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            playerOnPlatform = false;
        }
    }

    void CheckIfWin() {
        if (playerOnPlatform && !overlord.playerDead) {
            overlord.stageWin = true;
            effect.SetActive(true);
        }
    }

    public void SpawnWaves() { // spwan teleporter waves
        Instantiate(waves, wavesSpawn.transform.position, Quaternion.identity);
        wavesPlayed = true;
    }
}
