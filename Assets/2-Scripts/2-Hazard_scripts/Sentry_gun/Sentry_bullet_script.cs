using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentry_bullet_script : MonoBehaviour
{
    public Transform vittusaatana;
    private Transform spawn;
    private float timer = 0;
    public float bulletSpeed;
    private bool hit = false;
    public GameObject sparks;
    public AudioSource bulletAudio1;
    public AudioSource bulletAudio2;
    public AudioClip[] bulletClips;
    private Dima_script dimaScript;

    void Start()
    {
        spawn = gameObject.transform.Find("Spawn");
        dimaScript = GameObject.Find("Dima").GetComponent<Dima_script>();
        bulletAudio1.clip = bulletClips[0];
    }

    void Update()
    {
        if (!hit) {
            MoveBullet();

            timer += Time.deltaTime;

            if (timer >= 0.05) {
                SpawnMotionLine();
                timer = 0;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        BulletHit();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            bulletAudio2.clip = bulletClips[1];
            bulletAudio2.Play();
            KillDima();
        }
        
        if (col.CompareTag("box")) {
            bulletAudio2.clip = bulletClips[2];
            bulletAudio2.Play();
        }
    }

    void MoveBullet() { // go bullet, go!
        transform.position += transform.right * Time.deltaTime * bulletSpeed;
    }

    void SpawnMotionLine() { // spawns bullet motion sprite
        Transform newLine =  Instantiate(vittusaatana, spawn.transform.position, Quaternion.identity);
        newLine.transform.rotation = transform.localRotation;
    }

    void BulletHit() {
        hit = true;

        transform.Find("Bullet").gameObject.SetActive(false);
        Instantiate(sparks, transform.position, Quaternion.identity);
        bulletAudio1.Play();
        Destroy(gameObject, 0.3f);
    }

    void KillDima() {
        hit = true;

        dimaScript.health = 0;
        transform.Find("Bullet").gameObject.SetActive(false);
        Instantiate(sparks, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.3f);
    }
}
