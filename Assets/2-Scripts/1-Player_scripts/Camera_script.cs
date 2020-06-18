using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_script : MonoBehaviour
{
    public GameObject Dima;

    private GameObject crosshair;

    public Camera mainCamera;

    private Vector3 cameraPos;

    private float cameraSizeMin = 7f;
    private float cameraSizeMax = 14f;

    void Start()
    {
        crosshair = GameObject.Find("Crosshair");
    }

    void Update()
    {
        float heightOffset = 6f;
        float distance = Vector2.Distance(Dima.transform.position, crosshair.transform.position);

        transform.position =  new Vector3(Dima.transform.position.x, Dima.transform.position.y + heightOffset, -10f);

        // TODO: smoother camera size scaling?
        if (distance > cameraSizeMin && distance < cameraSizeMax) {
            mainCamera.orthographicSize = distance;
        }
    }
}
