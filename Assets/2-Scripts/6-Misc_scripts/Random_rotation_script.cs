using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_rotation_script : MonoBehaviour
{
    void Start()
    {
        Vector3 euler = transform.eulerAngles;
        euler.z = Random.Range(0, 360);
        transform.eulerAngles = euler;
    }
}
