using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Bullet_script : MonoBehaviour
{
    private GameObject crosshair;
    private GameObject Dima;
    private Crosshair_script crosshairScript;
    private Vector3 direction;
    private float angle;
    private Bullet_script parent;
    private Transform spawn;
    private float timer = 0;
    public Transform vittusaatana;
    public GameObject sparks;
    private AudioSource hitSound;
    public AudioClip[] clips;
    public bool finalHit = false;

    void Start()
    {
        crosshair = GameObject.Find("Crosshair");
        Dima = GameObject.Find("Dima");
        crosshairScript = crosshair.GetComponent<Crosshair_script>();
        spawn = gameObject.transform.Find("Spawn");
        hitSound = GetComponent<AudioSource>();
        
        SetStartDirection();
    }

    void Update()
    {
        if (!finalHit) {
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
        RotateBullet(col);
    }

    void MoveBullet() { // go bullet, go!
        transform.position += direction.normalized * Time.deltaTime * crosshairScript.bulletSpeed;
    }

    void RotateBullet(Collision2D col) { // rotate bullet based on new reflect direction
        float rotation = Mathf.Atan2(NewReflectDir(col).y, NewReflectDir(col).x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, rotation);
        direction = NewReflectDir(col);
    }

    void SetStartDirection() { // set intitial direction and rotation
        direction = GenericFunctions.Vector3ZeroZ(crosshair.transform.position - transform.position).normalized;
        transform.rotation = GenericFunctions.GetLookRotation(crosshair.transform, transform);
    }

    void SpawnMotionLine() { // spawns bullet motion sprite
        Transform newLine =  Instantiate(vittusaatana, spawn.transform.position, Quaternion.identity);
        newLine.transform.rotation = transform.localRotation;
    }

    void PlayHitSound() { // plays one of 3 hit sounds with volume adjusted based on distance
        float distance = GenericFunctions.GetDistance(gameObject, Dima);
        distance = distance > 100 ? 100 : distance;
        hitSound.volume = 0.6f - (distance / 60);

        int number = Random.Range(0, 2);
        hitSound.clip = clips[number];
        hitSound.Play();
    }

    public void BulletHit() {
        PlayHitSound();
        Instantiate(sparks, transform.position, Quaternion.identity);  
    }

    Vector3 NewReflectDir(Collision2D col) { // return new reflected direction
        return Vector3.Reflect(direction, col.contacts[0].normal);
    }
}