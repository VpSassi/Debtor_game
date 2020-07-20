using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_script : MonoBehaviour
{
    public bool pressed = false;
    public Animator buttonAnim;
    private Dima_script dimaScript;
    public float timer = 0;
    void Start()
    {
        buttonAnim = GetComponent<Animator>();
        dimaScript = GameObject.Find("Dima").GetComponent<Dima_script>();
    }

    void Update() {
        if (timer > 0) {
            timer -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("bullet")) {
            ButtonActivated();
        }
    }

    public void ButtonActivated() { // button gets activated/deactivated
        if (timer < 1) {
            pressed = !pressed;
            buttonAnim.SetBool("pressed", pressed);
            timer = 3;
        }
    }
}
