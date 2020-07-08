using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_destruct_script : MonoBehaviour
{
    public float killTimer = 0.5f;
    void Start()
    {
        Destroy(gameObject, killTimer);
    }
}
