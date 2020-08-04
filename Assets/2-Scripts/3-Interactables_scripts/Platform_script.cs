using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_script : MonoBehaviour
{
    private GameObject Dima;

    void Start()
    {
        Dima = GameObject.Find("Dima");
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            Dima.transform.parent = gameObject.transform;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            Dima.transform.parent = null;
        }
    }
}
