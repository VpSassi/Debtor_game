using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine_script : MonoBehaviour
{
    public GameObject damage_Area;
    private bool blown = false;
    public GameObject sprite;
    public AudioSource mineAudio;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player") && !blown || col.CompareTag("blast") && !blown) {
            BlowUpMine();
        }
    }

    void BlowUpMine() {
        mineAudio.Play();
        Instantiate(damage_Area, transform.position, Quaternion.identity);
        sprite.SetActive(false);
        blown = true;
    }
}
