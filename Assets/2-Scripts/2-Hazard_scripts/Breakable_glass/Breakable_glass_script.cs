using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_glass_script : MonoBehaviour
{
    public bool broken = false;
    private GameObject glass;
    private GameObject glassBroken_T;
    private GameObject glassBroken_B;
    void Start()
    {
        glass = transform.Find("Glass").gameObject;
        glassBroken_T = transform.Find("Broken_glass_T").gameObject;
        glassBroken_B = transform.Find("Broken_glass_B").gameObject;

        glassBroken_T.SetActive(false);
        glassBroken_B.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("bullet")) {
            BreakGlass();
        }
    }

    void BreakGlass() {
        broken = true;

        glass.SetActive(false);
        glassBroken_T.SetActive(true);
        glassBroken_B.SetActive(true);

        // TODO: play shattering sound
    }
}
