using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter_Start_script : MonoBehaviour
{
    private GameObject Dima;
    private Dima_script dimaScript;
    private Transform spawn;
    private Overlord_script overlord;
    private float effetcTimer = 5;

    void Awake()
    {
        Dima = GameObject.Find("Dima");
        dimaScript = Dima.GetComponent<Dima_script>();
        spawn = transform.Find("Spawn").gameObject.transform;
        overlord = GameObject.Find("OVERLORD").GetComponent<Overlord_script>();

        Spawn();
    }

    void Spawn() {
        Dima.transform.position = spawn.position;
    }
}
