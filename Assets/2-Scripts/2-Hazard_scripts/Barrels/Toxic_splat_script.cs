using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toxic_splat_script : MonoBehaviour
{
    private Dima_script dimaScript;
    private bool hit = false;
    private AudioSource toxicAudio;
    private float lifeTimer = 0;

    void Start()
    {
        dimaScript = GameObject.Find("Dima").GetComponent<Dima_script>();
        toxicAudio = GetComponent<AudioSource>();
    }

    void Update() {
        lifeTimer += Time.deltaTime;
        if (lifeTimer > Random.Range(10, 20)) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player") && !hit) {
            dimaScript.health = 0;
            toxicAudio.Play();
            hit = true;
        }
    }
}
