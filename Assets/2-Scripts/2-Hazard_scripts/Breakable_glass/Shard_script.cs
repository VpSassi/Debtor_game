using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard_script : MonoBehaviour
{
    private Dima_script dimaScript;
    void Start()
    {
        dimaScript = GameObject.Find("Dima").GetComponent<Dima_script>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            dimaScript.health = 0;
        }
    }
}
