using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_script : MonoBehaviour
{
    private GameObject crosshair;
    private GameObject Dima;
    private Crosshair_script crosshairScript;
    private Vector3 direction;
    private float angle;
    private Bullet_script parent;

    private Overlord_script overlord;

    void Start()
    {
        overlord = GameObject.Find("OVERLORD").GetComponent<Overlord_script>();
        crosshair = GameObject.Find("Crosshair");
        Dima = GameObject.Find("Dima");
        crosshairScript = crosshair.GetComponent<Crosshair_script>();
        
        SetStartDirection();
    }

    void Update()
    {
        MoveBullet();
    }

    void OnCollisionEnter2D(Collision2D col) {
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
        direction = crosshair.transform.position - transform.position;
        transform.rotation = overlord.ol_GetLookRotation(crosshair.transform, transform);
    }

    Vector3 NewReflectDir(Collision2D col) { // return new reflected direction
        return Vector3.Reflect(direction, col.contacts[0].normal);
    }
}