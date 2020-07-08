using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Animations_script : MonoBehaviour
{
    private Dima_script Dima;
    private Animator animator;

    void Awake()
    {
        Dima = GetComponent<Dima_script>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        jumpAnimation();
        moveAnimation();
    }

    void jumpAnimation() {
        animator.SetBool("jumping", !Dima.isGrounded);
    }

    void moveAnimation() {
        int animNumber;

        if (Dima.moveDir > 0) {
            animNumber = Dima.spriteRotation == 1 ? 1 : 2;
        } else if (Dima.moveDir < 0) {
            animNumber = Dima.spriteRotation == -1 ? 1 : 2;
        } else {
            animNumber = 0;
        }

        if (animator.GetBool("jumping") == false) {
            animator.SetInteger("moveDir", animNumber);
        }
    }
}
