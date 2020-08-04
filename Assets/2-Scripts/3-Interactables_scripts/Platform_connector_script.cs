using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Platform_connector_script : MonoBehaviour
{
    public Moving_platform_control_script controlScript;

    void Start()
    {
        RotateConnector();
    }

    void RotateConnector() {
        Quaternion lookRot = GenericFunctions.GetLookRotation(controlScript.rail_end_1.transform, controlScript.rail_end_2.transform);
        transform.rotation = lookRot;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("moving_platform_rail_end")) {
            int newRailDir = controlScript.railDirection == 0 ? 1 : 0;
            controlScript.railDirection = newRailDir;
        }
    }
}
