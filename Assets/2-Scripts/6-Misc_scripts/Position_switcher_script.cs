using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position_switcher_script : MonoBehaviour
{
    public Transform[] positions;
    public Transform switchableObject;

    void Start()
    {
        switchableObject.position = positions[Random.Range(0, positions.Length)].position;
    }
}
