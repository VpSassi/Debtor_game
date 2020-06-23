using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_rotation_script : MonoBehaviour
{
    private Transform crosshair;
    private Overlord_script overlord;
    
    void Start()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Transform>();
        overlord = GameObject.Find("OVERLORD").GetComponent<Overlord_script>();
    }

    
    void Update()
    {
        LookAtTarget();
    }

    void LookAtTarget() { // rotate head to look at crosshair
        Quaternion lookRot = overlord.ol_GetLookRotation(crosshair, transform);
        transform.rotation = lookRot;
        
        Vector3 adjustedRot = transform.rotation.eulerAngles;
        adjustedRot = new Vector3(0, 0, adjustedRot.z + 90);
        transform.rotation = Quaternion.Euler(adjustedRot);
    }
}
