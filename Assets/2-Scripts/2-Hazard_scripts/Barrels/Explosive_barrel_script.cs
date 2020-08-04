using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Explosive_barrel_script : MonoBehaviour
{
    public GameObject barrel_A;
    public GameObject barrel_B;
    public Damage_area_script fire_damage;
    public GameObject blast;
    public bool blown = false;
    private float explosiondDmgTimer = 0;
    public AudioSource explosionAudio;
    public AudioSource burnAudio;
    private GameObject Dima;

    void Start()
    {
        barrel_B.SetActive(false);
        fire_damage.enableDamage = false;
        Dima = GameObject.Find("Dima");
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (!blown && col.CompareTag("bullet") || !blown && col.CompareTag("blast")) {
            BlowUpBarrel();
        }
    }

    void Update() {
        float distance = GenericFunctions.GetDistance(gameObject, Dima);
        distance = distance > 100 ? 100 : distance;
        burnAudio.volume = 0.3f - (distance / 80);
    }

    void BlowUpBarrel() {
        barrel_A.SetActive(false);
        barrel_B.SetActive(true);
        fire_damage.enableDamage = true;

        Instantiate(blast, transform.position, Quaternion.identity);
        explosionAudio.Play();
        burnAudio.Play();

        blown = true;
    }
}
