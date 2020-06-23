using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle_script : MonoBehaviour
{
    private Transform target;
    private Transform crosshair;
    private Transform parent;

    private Transform Dima;
    
    private Vector3 leftPos = new Vector3(-0.73f, -0.35f, 0f);
    private Vector3 rightPos = new Vector3(0.673f, 0.282f, 0f);

    private Overlord_script overlord;

    void Start()
    {
        target = GameObject.Find("Arm_R_Target").GetComponent<Transform>();
        crosshair = GameObject.Find("Crosshair").GetComponent<Transform>();
        parent = GameObject.Find("Muzzle_parent").GetComponent<Transform>();
        Dima = GameObject.Find("Dima").GetComponent<Transform>();

        overlord = GameObject.Find("OVERLORD").GetComponent<Overlord_script>();
    }

    void Update()
    {
        LookAtCrosshair();
        AdjustPos();
    }

    void AdjustPos() {
        transform.localPosition = Dima.localScale == new Vector3(1, 1, 1) ? rightPos : leftPos;
    }

    void LookAtCrosshair() {
        parent.position = target.position;
        parent.rotation =  overlord.ol_GetLookRotation(crosshair, transform);
    }
}
