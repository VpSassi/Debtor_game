using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Laser_system_script : MonoBehaviour
{
    public GameObject laser_A;
    public GameObject laser_B;
    public GameObject beam_D;
    public GameObject beam_A1;
    public GameObject beam_A2;
    public bool isDeathLaser = false;
    public bool isContinous = true;
    private float timer = 0;
    public float timerDuration = 0;
    public float initialTimerDelay = 0;
    private bool laserOn = true;
    private GameObject laser_Ball_A;
    private GameObject laser_Ball_D_A;
    private GameObject laser_Ball_A1_A;
    private GameObject laser_Ball_A2_A;
    private GameObject laser_Ball_B;
    private GameObject laser_Ball_D_B;
    private GameObject laser_Ball_A1_B;
    private GameObject laser_Ball_A2_B;
    public bool laserHit = false;
    private Overlord_script overlord;
    private GameObject Dima;
    private Dima_script dimaScript;
    public AudioClip[] laserFireSounds;
    public AudioSource laserFireAudio;
    public AudioClip[] laserHitSounds;
    public AudioSource laserHitAudio;
    private bool playingFireAudio = false;
    private bool playingHitAudio = false;
    public bool laserOnline = true;
    private bool laserBallOn = true;
    private float laserBallTimer = 0;
    private GameObject middlePointObject;

    void Start()
    {
        overlord = GameObject.Find("OVERLORD").GetComponent<Overlord_script>();
        Dima = GameObject.Find("Dima");
        dimaScript = Dima.GetComponent<Dima_script>();

        Vector3 middlePoint = (laser_A.transform.Find("Laser_Target_A").position + laser_B.transform.Find("Laser_Target_B").position) / 2;

        middlePointObject = Instantiate(new GameObject(), middlePoint, Quaternion.identity);

        Quaternion lookRot = GenericFunctions.GetLookRotation(laser_A.transform, laser_B.transform);

        float distance = GenericFunctions.GetDistance(laser_A.transform.Find("Laser_Target_A").gameObject, laser_B.transform.Find("Laser_Target_B").gameObject);

        if (isDeathLaser) {
            beam_A1.SetActive(false);
            beam_A2.SetActive(false);

            beam_D.transform.position = middlePoint;
            beam_D.transform.rotation = lookRot;
            beam_D.transform.localScale = new Vector3(distance - (distance / 7), beam_D.transform.localScale.y, beam_D.transform.localScale.z);
        } else {
            beam_D.SetActive(false);

            beam_A1.transform.position = middlePoint;
            beam_A1.transform.rotation = lookRot;
            beam_A1.transform.localScale = new Vector3(distance - (distance / 7), beam_A1.transform.localScale.y, beam_A1.transform.localScale.z);

            beam_A2.transform.position = middlePoint;
            beam_A2.transform.rotation = lookRot;
            beam_A2.transform.localScale = new Vector3(distance - (distance / 7), beam_A2.transform.localScale.y, beam_A2.transform.localScale.z);
        }

        laser_Ball_A = laser_A.transform.Find("Laser_ball").gameObject;
        laser_Ball_D_A = laser_A.transform.Find("Laser_ball_D").gameObject;
        laser_Ball_A1_A = laser_A.transform.Find("Laser_ball_A1").gameObject;
        laser_Ball_A2_A = laser_A.transform.Find("Laser_ball_A2").gameObject;

        laser_Ball_B = laser_B.transform.Find("Laser_ball").gameObject;
        laser_Ball_D_B = laser_B.transform.Find("Laser_ball_D").gameObject;
        laser_Ball_A1_B = laser_B.transform.Find("Laser_ball_A1").gameObject;
        laser_Ball_A2_B = laser_B.transform.Find("Laser_ball_A2").gameObject;
    }

    void Update()
    {
        if (laserOnline) {
            beam_A1.SetActive(!isDeathLaser && !overlord.alarmOn);
            beam_A2.SetActive(!isDeathLaser && overlord.alarmOn);
            beam_D.SetActive(isDeathLaser);

            if (!isContinous) {
            TimerCounter();
            }

            LaserFireController();
            LaserHitFunc();
        } else {
            beam_A1.SetActive(false);
            beam_A2.SetActive(false);
            beam_D.SetActive(false);
        }
        
    }

    void TimerCounter() {

        if (initialTimerDelay > 0) {
            initialTimerDelay -= Time.deltaTime;
        }

        if (initialTimerDelay < 1) {
            timer += Time.deltaTime;
            laserBallTimer += Time.deltaTime;
        }

        if (timer >= timerDuration) {
            timer = 0;
            laserBallTimer = 0;

            laserOn = !laserOn;
        }

        if (laserBallTimer >= timerDuration - 0.2f) { // TODO: fix this
            laserBallOn = !laserBallOn;
            laserBallTimer = 0;
        }
    }

    void LaserFireController() {
        float baseVolume = isDeathLaser ? 0.4f : 0.4f;
        float distance = GenericFunctions.GetDistance(middlePointObject, Dima);
        distance = distance > 100 ? 100 : distance;
        laserFireAudio.volume = baseVolume - (distance / 60);

        if (isDeathLaser) {
            beam_D.SetActive(laserOn);
            laserFireAudio.clip = laserFireSounds[0];
        } else {
            if (overlord.alarmOn) {
                beam_A2.SetActive(laserOn);
                beam_A1.SetActive(false);
            } else {
                beam_A1.SetActive(laserOn);
                beam_A2.SetActive(false);
            }
            laserFireAudio.clip = laserFireSounds[1];
        }
        if (laserOn && !playingFireAudio) {
            laserFireAudio.Play();
            playingFireAudio = true;
        } else if (!laserOn) {
            laserFireAudio.Stop();
            playingFireAudio = false;
        }
        LaserBalls();
    }

    void LaserBalls() {
        if (isDeathLaser) {
            laser_Ball_A.SetActive(!laserBallOn);
            laser_Ball_D_A.SetActive(laserBallOn);
            laser_Ball_A1_A.SetActive(false);
            laser_Ball_A2_A.SetActive(false);

            laser_Ball_B.SetActive(!laserBallOn);
            laser_Ball_D_B.SetActive(laserBallOn);
            laser_Ball_A1_B.SetActive(false);
            laser_Ball_A2_B.SetActive(false);
        } else {
            laser_Ball_A.SetActive(!laserBallOn);
            laser_Ball_D_A.SetActive(false);
            if (overlord.alarmOn) {
                laser_Ball_A2_A.SetActive(laserBallOn);
                laser_Ball_A1_A.SetActive(false);
            } else {
                laser_Ball_A1_A.SetActive(laserBallOn);
                laser_Ball_A2_A.SetActive(false);
            }

            laser_Ball_B.SetActive(!laserBallOn);
            laser_Ball_D_B.SetActive(false);
            if (overlord.alarmOn) {
                laser_Ball_A2_B.SetActive(laserBallOn);
                laser_Ball_A1_B.SetActive(false);
            } else {
                laser_Ball_A1_B.SetActive(laserBallOn);
                laser_Ball_A2_B.SetActive(false);
            }
        }
    }

    void LaserHitFunc() {
        if (laserHit) {
            if (isDeathLaser) {
                dimaScript.health = 0;
                laserHitAudio.clip = laserHitSounds[0];
            } else {
                overlord.alarmOn = true;
                laserHitAudio.clip = laserHitSounds[1];
            }
            if (!playingHitAudio) {
                laserHitAudio.Play();
                playingHitAudio = true;
            }
        }
    }
}
