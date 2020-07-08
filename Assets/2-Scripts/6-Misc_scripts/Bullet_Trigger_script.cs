using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Trigger_script : MonoBehaviour
{
    private float lifeTime = 0;
    private GameObject parent;
    private GameObject Dima;
    private Dima_script dimaScript;
    private AudioSource hitSound;
    private Overlord_script overlord;

    void Start() {
        parent = transform.parent.gameObject;
        Dima = GameObject.Find("Dima");
        dimaScript = Dima.GetComponent<Dima_script>();
        hitSound = GetComponent<AudioSource>();
        overlord = GameObject.Find("OVERLORD").GetComponent<Overlord_script>();
    }

    void Update()
    {
        lifeTime += 1 * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player") && lifeTime > 1 && !overlord.playerDead && !overlord.stageWin) {
            DamagePlayer();
            hitSound.Play();
            Destroy(parent, 0.3f);
        }
    }

    void DamagePlayer() {
        dimaScript.health = dimaScript.health - 1;
    }
}