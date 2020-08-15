using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_base_script : MonoBehaviour
{
    private Button_script buttonScript;
    private bool playerClose = false;
    void Start()
    {
        buttonScript = transform.Find("Button").GetComponent<Button_script>();
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
}
