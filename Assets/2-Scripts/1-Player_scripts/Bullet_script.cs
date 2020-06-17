﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_script : MonoBehaviour
{
    private GameObject crosshair;
    private Crosshair_script crosshairScript;
    private Vector3 direction;

    private float angle;
    private Quaternion rotation;

    void Start()
    {
        crosshair = GameObject.Find("Crosshair");
        crosshairScript = crosshair.GetComponent<Crosshair_script>();
        direction = crosshair.transform.position - transform.position;

        // calculate rotation
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;
    }

    void Update()
    {
        // go bullet go!
        transform.position += direction * Time.deltaTime * crosshairScript.bulletSpeed;
    }
}
