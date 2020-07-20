using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter_End_script : MonoBehaviour
{
    private bool playerOnPlatform = false;
    private Dima_script dimaScript;
    private Overlord_script overlord;
    void Start()
    {
        dimaScript = GameObject.Find("Dima").GetComponent<Dima_script>();
        overlord = GameObject.Find("OVERLORD").GetComponent<Overlord_script>();
    }

    void Update()
    {
        CheckIfWin();
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
        if (playerOnPlatform && dimaScript.pressedActionButton) {
            overlord.stageWin = true;
        }
    }
}
