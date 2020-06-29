using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm_aim_script : MonoBehaviour
{
    private Transform crosshair;
    
    void Start()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Transform>();
    }

    
    void Update()
    {
        GoToTarget();
    }

    void GoToTarget() {
        transform.position = crosshair.position;
    }
}
