using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_button_controller_script : MonoBehaviour
{
    public Laser_system_script laser;
    public Button_script button;

    void Update()
    {
        laser.laserOnline = !button.pressed;
    }
}
