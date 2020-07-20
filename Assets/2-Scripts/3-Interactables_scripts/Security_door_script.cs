using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security_door_script : MonoBehaviour
{
    public Button_script button;
    private Animator doorAnim;
    private GameObject open_T;
    private GameObject open_B;
    private GameObject closed_T;
    private GameObject closed_B;
    private bool pressed = false;
    private AudioSource doorAudio;
    public AudioClip[] doorClips;

    void Start()
    {
        doorAnim = transform.Find("Door").GetComponent<Animator>();
        open_T = transform.Find("Open_T").gameObject;
        open_B = transform.Find("Open_B").gameObject;
        closed_T = transform.Find("Closed_T").gameObject;
        closed_B = transform.Find("Closed_B").gameObject;
        doorAudio = GetComponent<AudioSource>();

        open_T.SetActive(false);
        open_B.SetActive(false);
        closed_T.SetActive(true);
        closed_B.SetActive(true);
    }

    void Update()
    {
        if (pressed != button.pressed) {
            if (button.pressed) {
                DoorOpen();
            } else {
                DoorClose();
            }
        }
    }

    void DoorOpen() {
        doorAudio.clip = doorClips[0];
        doorAudio.Play();

        open_T.SetActive(true);
        open_B.SetActive(true);
        closed_T.SetActive(false);
        closed_B.SetActive(false);

        doorAnim.SetBool("open", true);
        pressed = true;
    }

    void DoorClose() {
        doorAudio.clip = doorClips[1];
        doorAudio.Play();

        open_T.SetActive(false);
        open_B.SetActive(false);
        closed_T.SetActive(true);
        closed_B.SetActive(true);

        doorAnim.SetBool("open", false);
        pressed = false;
    }
}
