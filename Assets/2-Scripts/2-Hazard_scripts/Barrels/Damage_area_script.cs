using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_area_script : MonoBehaviour
{
    public bool enableDamage = true;
    private Dima_script dimaScript;

    void Start()
    {
        dimaScript = GameObject.Find("Dima").GetComponent<Dima_script>();
    }

    void OnTriggerStay2D(Collider2D col) {
        if (enableDamage && col.CompareTag("Player")) {
            dimaScript.health = 0;
        }
    }
}
