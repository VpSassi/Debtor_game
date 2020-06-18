using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Trigger_script : MonoBehaviour
{
    private float lifeTime = 0;
    private GameObject parent;
    void Update()
    {
        lifeTime += 1 * Time.deltaTime;
        parent = transform.parent.gameObject;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && lifeTime > 1) {
            Destroy(parent);
        }
    }
}