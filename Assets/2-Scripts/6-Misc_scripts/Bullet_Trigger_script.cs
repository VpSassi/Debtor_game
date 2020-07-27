using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Bullet_Trigger_script : MonoBehaviour
{
    private float lifeTime = 0;
    private GameObject parent;
    private GameObject Dima;
    private Dima_script dimaScript;
    private AudioSource hitSound;
    private Overlord_script overlord;
    public AudioClip[] clips;
    private bool damageGiven = false;

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
            if (!damageGiven) {
                DamagePlayer();
                AdjustVolume();
                PlaySound(0);
                parent.transform.Find("Bullet").gameObject.SetActive(false);
                parent.GetComponent<Bullet_script>().finalHit = true;
                Destroy(parent, 0.3f);
            }
        }

        /*
        if (col.gameObject.CompareTag("button")) {
                // Kill or let bounce?
            }   
        }
        */

        if (col.gameObject.CompareTag("box")) {
            
            PlaySound(1);
            // Kill or let bounce?
        }

        if (col.gameObject.CompareTag("sentry_gun")) {
            if (!damageGiven) {
                DamageSentryGun(col);
                parent.transform.Find("Bullet").gameObject.SetActive(false);
                parent.GetComponent<Bullet_script>().finalHit = true;
                Destroy(parent, 0.3f);
            }
        }
    }

    void DamagePlayer() {
        dimaScript.health--;
        damageGiven = true;
    }

    void PlaySound(int clipNumber) {
        hitSound.clip = clips[clipNumber];
        hitSound.Play();
    }

    void AdjustVolume() {
        float distance = GenericFunctions.GetDistance(gameObject, Dima);
        distance = distance > 100 ? 100 : distance;
        hitSound.volume = 1f - (distance / 40);
    }

    void DamageSentryGun(Collider2D col) {
        Sentry_gun_script sentryGunScript = col.gameObject.GetComponent<Sentry_gun_script>();

        PlaySound(2);

        sentryGunScript.health--;
        sentryGunScript.damaged = true;
        damageGiven = true;
    }
}