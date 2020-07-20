using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire_connector_script : MonoBehaviour
{
    public Button_script button;
    private GameObject on;
    private GameObject off;
    void Start()
    {
        on = gameObject.transform.Find("On").gameObject;
        off = gameObject.transform.Find("Off").gameObject;
    }

    void Update()
    {
        ChangeValue();
    }

    void ChangeValue() { // show corresponding state depending whether or not button has been pressed
        on.SetActive(button.pressed);
        off.SetActive(!button.pressed);
    }
}
