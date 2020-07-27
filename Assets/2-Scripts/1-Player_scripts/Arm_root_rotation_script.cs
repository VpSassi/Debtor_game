using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Arm_root_rotation_script : MonoBehaviour
{
    private Vector3 rot;
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
            RotationAdjust();
        }
    }

    void RotationAdjust() {
        Quaternion newRot = GenericFunctions.GetLookRotation(transform, crosshair);
        Vector3 adjustedRot = newRot.eulerAngles;
        adjustedRot = dimaScript.spriteRotation == 1 ? new Vector3(0, 0, adjustedRot.z + 160f) : new Vector3(0, 0, adjustedRot.z + 20f);

        transform.rotation = Quaternion.Euler(adjustedRot);
    }
}
