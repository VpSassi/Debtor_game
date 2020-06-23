using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlord_script : MonoBehaviour
{
    public bool stop = false;

    public float ol_GetDistance(GameObject obj1, GameObject obj2) { // returns distance between 2 gameobjects
       return Vector2.Distance(obj1.transform.position, obj2.transform.position);
    }

    public Quaternion ol_GetLookRotation(Transform tr1, Transform tr2) { // returns looking rotation between 2 transforms
        Vector3 dir = tr1.position - tr2.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;

        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public Vector3 ol_Vector3ZeroZ(Vector3 vector) {
      return new Vector3(vector.x, vector.y, 0);
    }
    
    void Update() {
        
        if (Input.GetKeyDown(KeyCode.X)) { // sets universal stop bool, that can be used in other functions for testing
            stop = !stop;

            // Currently used in:
            // * Dima_script --> RotateSprite()
            // * Crosshair_script --> MoveCrosshair()
        }
    }
}
