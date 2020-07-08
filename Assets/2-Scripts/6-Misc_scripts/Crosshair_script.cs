using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Crosshair_script : MonoBehaviour
{
    public Camera mainCamera;
    public float bulletSpeed = 0.5f;
    private Vector3 latestPos;
    private Dima_script dimaScript;

    void Start() {
        dimaScript = GameObject.Find("Dima").GetComponent<Dima_script>();
    }
    
    void Update()
    {
        if (dimaScript.health > 0) {
            MoveCrosshair();
        }
    }

    Vector3 GetMousePos() {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return mouseWorldPos;
    }

    void MoveCrosshair() { // move crosshair to mouse position
        latestPos = new Vector3(GetMousePos().x, GetMousePos().y, 10f);

        if (!GlobalVariables.stop) { // TESTING
            transform.position = latestPos;
        }
    }
}
