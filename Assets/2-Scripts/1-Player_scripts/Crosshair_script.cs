using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair_script : MonoBehaviour
{
    public Camera mainCamera;

    public float bulletSpeed = 0.1f;
    void Update()
    {
        // crosshairs position is the same as mouse location in world
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 10f);
    }
}
