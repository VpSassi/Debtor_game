using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm_aim_script : MonoBehaviour
{

    private Transform crosshair;
    private Dima_script Dima;
    public bool rightArm = false;
    private GameObject handEmptyL;
    private GameObject handMakarovL;
    private GameObject handEmptyR;
    private GameObject handMakarovR;
    
    void Start()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Transform>();
        Dima = GameObject.Find("Dima").GetComponent<Dima_script>();

        handEmptyL = GameObject.Find("Hand_L");
        handEmptyR = GameObject.Find("Hand_R");
        handMakarovL = GameObject.Find("Hand_L_Makarov");
        handMakarovR = GameObject.Find("Hand_R_Makarov");
    }

    
    void Update()
    {
        if (rightArm && Dima.spriteRotation == 1) {
            GoToTarget();
            handEmptyL.SetActive(true);
            handEmptyR.SetActive(false);
            handMakarovL.SetActive(false);
            handMakarovR.SetActive(true);
        }

        if (!rightArm && Dima.spriteRotation == -1) {
            GoToTarget();
            handEmptyL.SetActive(false);
            handEmptyR.SetActive(true);
            handMakarovL.SetActive(true);
            handMakarovR.SetActive(false);
        }
        
    }

    void GoToTarget() {
        transform.position = crosshair.position;
    }
}
