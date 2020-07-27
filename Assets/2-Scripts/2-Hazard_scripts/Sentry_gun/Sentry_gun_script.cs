using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Sentry_gun_script : MonoBehaviour
{
    private Animator turretAnim;
    private GameObject Dima;
    private Dima_script dimaScript;
    private Overlord_script overlord;
    private bool active = false;
    public bool spottedPlayer = false;
    public bool shoot = false;
    public int health = 3;
    public GameObject turretShot;
    public Transform barrelEnd;
    private AudioSource turretAudio;
    public AudioClip[] turretClips;
    private AudioSource shootAudio;
    private float bleepTimer = 0;
    public GameObject target1;
    public GameObject target2;
    private float turretAimWaitTimer = 0;
    public float turretWaitTimer = 5;
    private int turretCurrentTarget = 1;
    public GameObject laser_A;
    public GameObject laser_B;
    public GameObject OK;
    private Color color1;
    private Color color2;
    public SpriteRenderer ballSprite;
    public SpriteRenderer baseSprite;
    public bool damaged = false;
    private float damagedTimer = 0;
    private bool playedAlertSound = false;
    private bool playedDeathSound = false;
    public GameObject deathParticles;

    void Start()
    {
        turretAnim = GetComponent<Animator>();
        Dima = GameObject.Find("Dima");
        dimaScript = Dima.GetComponent<Dima_script>();
        overlord = GameObject.Find("OVERLORD").GetComponent<Overlord_script>();
        turretAudio = GetComponent<AudioSource>();
        shootAudio = barrelEnd.GetComponent<AudioSource>();
        color1 = new Color(255, 255, 255, 255);  // white
        color2 = new Color(255, 0, 0, 255); // red
        deathParticles.SetActive(false);
    }

    void Update()
    {
        if (health > 0) {
            bleepTimer += Time.deltaTime;

            if (dimaScript.health <= 0) {
                shoot = false;
            }
            
            turretAnim.SetBool("shootPlayer", shoot);
            turretAnim.SetBool("active", spottedPlayer);
            active = overlord.alarmOn;
            GunDamagedCheck();

            if (active) {
                ShootRays();

                if (!spottedPlayer) {
                    AimTurret();
                } else {
                    if (!playedAlertSound) {
                        turretAudio.clip = turretClips[3];
                        turretAudio.volume = 1;
                        turretAudio.Play();
                        playedAlertSound = true;
                    }

                    AimTurretTowardsPlayer();
                }
            }

            if (bleepTimer > 4) {
                PlayBleep();
                bleepTimer = 0;
            }
        } else {
            SentryGunDead();
        }
    }

    void AimTurret() {
        GameObject currentTargetObj = turretCurrentTarget == 1 ? target1 : target2;

        turretAimWaitTimer -= Time.deltaTime;

        Quaternion newRot = GenericFunctions.GetLookRotation(transform, currentTargetObj.transform);
        Vector3 adjustedRot = newRot.eulerAngles;
        adjustedRot = new Vector3(0, 0, adjustedRot.z + 180f);

        if (transform.rotation.eulerAngles.z >= adjustedRot.z - 10 && transform.rotation.eulerAngles.z <= adjustedRot.z + 10 ) {
            if (turretAimWaitTimer <= 0) {
                turretAimWaitTimer = turretWaitTimer;
            }
            turretCurrentTarget = turretCurrentTarget == 1 ? 2 : 1;
        }

        if (turretAimWaitTimer <= 0) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(adjustedRot), Time.deltaTime / 1f);
        }
    }

    void AimTurretTowardsPlayer() {
        Quaternion newRot = GenericFunctions.GetLookRotation(transform, Dima.transform);
        Vector3 adjustedRot = newRot.eulerAngles;
        adjustedRot = new Vector3(0, 0, adjustedRot.z + 180f);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(adjustedRot), Time.deltaTime / 0.5f);
    }

    void PlayBleep() {
        turretAudio.clip = turretClips[Random.Range(0, 2)];
        turretAudio.volume = AdjustedVolume();

        turretAudio.Play();
    }

    void ShootRays() {

        RaycastHit2D hit1 = Physics2D.Raycast(laser_A.transform.position, transform.right);
        RaycastHit2D hit2 = Physics2D.Raycast(laser_B.transform.position, transform.right);

        // TODO: better laser dot sprite
        Instantiate(OK, hit1.point, Quaternion.identity);
        Instantiate(OK, hit2.point, Quaternion.identity);

        if (hit1.collider.CompareTag("Player") || hit2.collider.CompareTag("Player")) {
            spottedPlayer = true;
            shoot = true;
        } else {
            shoot = false;
        }
    }

    void ShootTurret() {
        if (dimaScript.health > 0) {
            GameObject perkele = Instantiate(turretShot, barrelEnd.transform.position, Quaternion.identity);
            perkele.transform.localRotation = transform.localRotation;
            shootAudio.Play();
        }
    }

    void GunDamagedCheck() {
        damagedTimer -= Time.deltaTime;

        if (damaged) {
            damagedTimer = 0.2f;
            damaged = false;
        }

        ballSprite.color = damagedTimer > 0 ? color2 : color1;
        baseSprite.color = damagedTimer > 0 ? color2 : color1;
    }

    void SentryGunDead() {
        deathParticles.SetActive(true);
        if (!playedDeathSound) {
            turretAudio.clip = turretClips[4];
            turretAudio.volume = 1;
            turretAudio.Play();
            playedDeathSound = true;
        }
        turretAnim.SetBool("shootPlayer", false);
        turretAnim.SetBool("active", false);

        ballSprite.color = color2;
        baseSprite.color = color2;
        // TODO: add effects and sounds
    }

    float AdjustedVolume() {
        float distance = GenericFunctions.GetDistance(gameObject, Dima);
        distance = distance > 100 ? 100 : distance;
        return 0.5f - (distance / 50);
    }
}
