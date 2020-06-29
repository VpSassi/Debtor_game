using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Camera_script : MonoBehaviour
{
    private GameObject Dima;
    private GameObject crosshair;
    private Vector3 cameraPos;
    public float cameraSizeMin = 7f;
    public float cameraSizeMax = 14f;

    void Start()
    {
        crosshair = GameObject.Find("Crosshair");
        Dima = GameObject.Find("Dima");
    }

    void Update()
    {
        float heightOffset = 6f;
        float distance = GenericFunctions.GetDistance(Dima, crosshair);

        transform.position = NewCameraPos(heightOffset);
        
        if (distance > cameraSizeMin && distance < cameraSizeMax) {
            ZoomCamera(distance);
        }
    }

    void ZoomCamera(float distance) { // zooms camera based on crosshair placement
        // TODO: smoother camera size scaling?
        GetComponent<Camera>().orthographicSize = distance;
    }

    Vector3 NewCameraPos(float heightOffset) { // returns new camera position
        // TODO: smoother player follow (now it jitters a little)
        return new Vector3(Dima.transform.position.x, Dima.transform.position.y + heightOffset, -10f);
    }
}
