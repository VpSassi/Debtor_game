using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Overlord_script : MonoBehaviour
{
    void Update() {
        
        // TODO: move this to Class1.cs
        if (Input.GetKeyDown(KeyCode.X)) { // sets universal stop bool, that can be used in other functions for testing
            GlobalVariables.stop = !GlobalVariables.stop;

            // Currently used in:
            // * Dima_script --> RotateSprite()
            // * Crosshair_script --> MoveCrosshair()
        }
    }
}
