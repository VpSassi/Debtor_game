using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydraulic_platform_script : MonoBehaviour
{
    public GameObject platform;
    public Transform target_UP;
    public Transform target_DOWN;
    public bool startDown = true;
    private bool directionUp = true;
    public float platformSpeed = 5f;
    public float waitTimer = 3f;
    private float timer = 0f;
    public bool upDownMovement = true;

    void Start()
    {
        target_UP.position = upDownMovement 
        ? new Vector3(transform.position.x, target_UP.position.y, 10)
        : new Vector3(target_UP.position.x, transform.position.y, 10);

        target_DOWN.position = upDownMovement
        ? new Vector3(transform.position.x, target_DOWN.position.y, 10)
        : new Vector3(target_DOWN.position.x, transform.position.y, 10);

        platform.transform.position = startDown ? target_DOWN.position : target_UP.position;
        directionUp = startDown;
    }

    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
        }
        MovePlatform();
    }

    void MovePlatform() {
        if (timer <= 0) {
            Vector3 targetPos = directionUp ? target_UP.position : target_DOWN.position;
            platform.transform.position = Vector2.MoveTowards(platform.transform.position, targetPos, Time.deltaTime * platformSpeed);
            
            if (upDownMovement && platform.transform.position.y == targetPos.y || 
                !upDownMovement && platform.transform.position.x == targetPos.x) {
                timer = waitTimer;
                directionUp = !directionUp;
            };
        }
    }
}
