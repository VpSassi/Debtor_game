using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidden_platform_script : MonoBehaviour
{
    private SpriteRenderer platformSprite;
    private Color color1 = new Color(110, 50, 50, 255);
    private Color color2 = new Color(110, 50, 50, 0);
    private bool hit = false;

    void Start()
    {
        platformSprite = GetComponent<SpriteRenderer>();
        platformSprite.color = color2;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player") && !hit || col.CompareTag("bullet") && !hit) {
            PlatformHit();
        }
    }

    void PlatformHit() {
        platformSprite.color = color1;
        hit = true;
    }
}
