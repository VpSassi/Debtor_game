using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_platform_button_controller_script : MonoBehaviour
{
    public Button_script button;
    public Moving_platform_control_script platform;

    void Update()
    {
        platform.platformOn = button.pressed;
    }
}
