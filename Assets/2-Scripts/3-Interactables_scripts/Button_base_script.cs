using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_base_script : MonoBehaviour
{
    private Dima_script dimaScript;
    private Button_script buttonScript;
    private bool playerClose = false;
    void Start()
    {
        dimaScript = GameObject.Find("Dima").GetComponent<Dima_script>();
        buttonScript = transform.Find("Button").GetComponent<Button_script>();
    }

    void Update() {
        CheckPlayerButtonAction();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            playerClose = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            playerClose = false;
        }
    }

    void CheckPlayerButtonAction() { // call button action func with action button
        if (playerClose && dimaScript.pressedActionButton) {
            buttonScript.ButtonActivated();
        }
    }
}
