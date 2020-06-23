using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Trigger_script : MonoBehaviour
{
    private float lifeTime = 0;
    private GameObject parent;
    private GameObject Dima;
    private Dima_script dimaScript;

    void Start() {
        parent = transform.parent.gameObject;
        Dima = GameObject.Find("Dima");
        dimaScript = Dima.GetComponent<Dima_script>();
    }

    void Update()
    {
        lifeTime += 1 * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && lifeTime > 1) {
            DamagePlayer();
            Destroy(parent);
        }
    }

    void DamagePlayer() {
        dimaScript.health = dimaScript.health - 1;
    }
}