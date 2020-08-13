using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Button_script : MonoBehaviour
{
    public bool pressed = false;
    private Animator buttonAnim;
    private GameObject Dima;
    private Dima_script dimaScript;
    private float timer = 0;
    public float timerDuration = 3;
    private AudioSource buttonAudio;

    void Start()
    {
        buttonAnim = GetComponent<Animator>();
        Dima = GameObject.Find("Dima");
        dimaScript = Dima.GetComponent<Dima_script>();
        buttonAudio = GetComponent<AudioSource>();
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
            timer = timerDuration;
            buttonAudio.volume = AdjustedVolume();
            buttonAudio.Play();
        }
    }

    float AdjustedVolume() {
        float distance = GenericFunctions.GetDistance(gameObject, Dima);
        distance = distance > 100 ? 100 : distance;
        return 1f - (distance / 50);
    }
}
