using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm_aim_script : MonoBehaviour
{
    private Transform crosshair;
    private Dima_script dimaScript;
    
    void Start()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Transform>();
        dimaScript = GameObject.Find("Dima").GetComponent<Dima_script>();
    }

    
    void Update()
    {
        if (dimaScript.health > 0) {
            GoToTarget();
        }
    }

    void GoToTarget() {
        transform.position = crosshair.position;
    }
}
