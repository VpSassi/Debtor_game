using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Moving_platform_control_script : MonoBehaviour
{
    public GameObject platform;
    public GameObject connector;
    public GameObject rail_end_1;
    public GameObject rail_end_2;
    public GameObject lightOn;
    public GameObject lightOff;
    public Transform wire;
    public float platformSpeed = 5;
    public int railDirection = 0;
    public bool platformOn = false;

    void Start()
    {
        Vector3 middlePoint = (rail_end_1.transform.position + rail_end_2.transform.position) / 2;
        Quaternion lookRot = GenericFunctions.GetLookRotation(rail_end_1.transform, rail_end_2.transform);
        float distance = GenericFunctions.GetDistance(rail_end_1, rail_end_2);

        wire.position = middlePoint;
        wire.rotation = lookRot;
        wire.localScale = new Vector3(distance - (distance / 1.21f), wire.transform.localScale.y, wire.transform.localScale.z);
    }

    void Update()
    {
        if (platformOn) {
            MovePlatform();
            lightOn.SetActive(true);
            lightOff.SetActive(false);
        } else {
            lightOn.SetActive(false);
            lightOff.SetActive(true);
        }
    }

    void MovePlatform() { // moves platform between 2 rail ends and changes direction when hitting either one
        GameObject rail_end = railDirection == 0 ? rail_end_1 : rail_end_2;
        platform.transform.position = Vector2.MoveTowards(platform.transform.position, rail_end.transform.position, platformSpeed * Time.deltaTime);
    }
}
