using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_BG_script : MonoBehaviour
{
    private int currentImage = 0;
    private GameObject img1;
    // private Image img2;
    // private Image img3;
    private float timer = 0;

    void Start()
    {
        AdjustImageSizes();
        img1 = transform.Find("Image_1").gameObject;
        // img2 = transform.Find("Image_2").GetComponent<Image>();
        // img3 = transform.Find("Image_3").GetComponent<Image>();
    }

    /*
    void Update()
    {
        timer++;
        SwitchImage();
    }
    */

    void AdjustImageSizes() {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        img1.GetComponent<RectTransform>().sizeDelta = screenSize;
        // img2.GetComponent<RectTransform>().sizeDelta = screenSize;
        // img3.GetComponent<RectTransform>().sizeDelta = screenSize;
    }

    /*
    void SwitchImage() {
        if (timer >= 2) {
            int index = 0;

            Image img1Copy = img1;
            img1Copy.color.a = currentImage == 0 ? 1f : 0f;
            img1.color = img1Copy.color;

            Image img2Copy = img2;
            img2Copy.color.a = currentImage == 1 ? 1f : 0f;
            img2.color = img2Copy.color;

            Image img3Copy = img3;
            img3Copy.color.a = currentImage == 2 ? 1f : 0f;
            img3.color = img3Copy.color;

            currentImage = Random.Range(0, 3);
        }
    }
    */
}
