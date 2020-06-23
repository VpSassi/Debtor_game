using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair_script : MonoBehaviour
{
    public Camera mainCamera;
    public float bulletSpeed = 0.5f;
    private Vector3 latestPos;

    private Overlord_script overlord;

    void Start() {
        overlord = GameObject.Find("OVERLORD").GetComponent<Overlord_script>();
    }
    
    void Update()
    {
        MoveCrosshair();
    }

    Vector3 GetMousePos() {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return mouseWorldPos;
    }

    void MoveCrosshair() { // move crosshair to mouse position
        latestPos = new Vector3(GetMousePos().x, GetMousePos().y, 10f);

        if (!overlord.stop) { // TESTING
            transform.position = latestPos;
        }
    }
}
