using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_anim_script : MonoBehaviour
{
    private float timer = 2;
    void Start()
    {

    }

    void Update()
    {
        if (timer > 0.9f) {
            timer -= Time.deltaTime;
            
            // 
        }
    }
}
