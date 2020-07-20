using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_script : MonoBehaviour
{
    public Laser_system_script laser_System;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            laser_System.laserHit = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            laser_System.laserHit = false;
        }
    }
}
