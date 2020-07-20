using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Russian_animal_trap_script : MonoBehaviour
{
    private Dima_script dimaScript;
    private Animator trapAnim;
    private AudioSource trapAudio;
    private bool snap = false;
    private bool activated = false;

    void Start()
    {
        dimaScript = GameObject.Find("Dima").GetComponent<Dima_script>();
        trapAnim = GetComponent<Animator>();
        trapAudio = GetComponent<AudioSource>();
    }

    void Update() {
        if (snap && !activated) {
            TrapActivate();
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            snap = true;
        }
    }

    void TrapActivate() { // play snapping animation, sound and kill Dima
        trapAnim.SetBool("snap", true);
        trapAudio.Play();
        dimaScript.health = 0;
        activated = true;
    }
}
