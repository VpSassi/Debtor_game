using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Head_rotation_script : MonoBehaviour
{
    private Transform crosshair;
    
    void Start()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Transform>();
    }

    
    void Update()
    {
        LookAtTarget();
    }

    void LookAtTarget() { // rotate head to look at crosshair
        Quaternion lookRot = GenericFunctions.GetLookRotation(crosshair, transform);
        transform.rotation = lookRot;
        
        Vector3 adjustedRot = transform.rotation.eulerAngles;
        adjustedRot = new Vector3(0, 0, adjustedRot.z + 90);
        transform.rotation = Quaternion.Euler(adjustedRot);
    }
}
