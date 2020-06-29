using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Muzzle_script : MonoBehaviour
{
    private Transform target_R;
    private Transform target_L;
    private Transform crosshair;
    public Transform parent;
    private Transform Dima;
    public bool isRightHand = false;
    
    private Vector3 leftPos;
    private Vector3 rightPos;

    void Start()
    {
        target_R = GameObject.Find("Arm_R_Effector").GetComponent<Transform>();
        target_L = GameObject.Find("Arm_L_Effector").GetComponent<Transform>();
        crosshair = GameObject.Find("Crosshair").GetComponent<Transform>();
        Dima = GameObject.Find("Dima").GetComponent<Transform>();

        leftPos = isRightHand ? new Vector3(-0.5f, -0.45f, 0f) : new Vector3(-0.4f, -0.39f, 0f);
        rightPos = isRightHand ?  new Vector3(0.5f, 0.45f, 0f) : new Vector3(0.4f, 0.39f, 0f);
    }

    void Update()
    {
        LookAtCrosshair();
        AdjustPos();
    }

    void AdjustPos() {
        transform.rotation = parent.rotation;
        transform.localPosition = Dima.localScale == new Vector3(1, 1, 1) ? rightPos : leftPos;
    }

    void LookAtCrosshair() {
        parent.position = isRightHand ? target_R.position : target_L.position;
        parent.rotation =  GenericFunctions.GetLookRotation(crosshair, transform);
    }
}
