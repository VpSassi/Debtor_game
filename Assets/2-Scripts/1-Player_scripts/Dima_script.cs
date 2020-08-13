using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Dima_script : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 400f;
    private Rigidbody2D rigidBody;
    public bool isGrounded = true;
    private bool aiming = false;
    private Camera_script MainCamera;
    private GameObject muzzle_R;
    private GameObject muzzle_L;
    public Transform bulletPrefab;
    public int health = 1;
    public float spriteRotation = 1;
    private bool shootFromRight = true;
    public GameObject face_A;
    public GameObject face_D;
    public float moveDir = 0; 
    public GameObject muzzleFlash;
    public bool pressedActionButton = false;
    private Animator Dima_Animator;
    private GameObject RightArmSolver;
    private GameObject LeftArmSolver;
    private GameObject Arm_R_Effector;
    private GameObject Arm_L_Effector;
    private Overlord_script overlord;
    public AudioSource walkAudio;
    public AudioClip[] walkSounds;
    public AudioSource jumpAudio;
    public AudioClip jumpSound;
    public AudioSource gunAudio;
    public AudioClip[] gunSounds;
    public AudioSource deathAudio;
    public AudioClip deathSound;
    private float shootTimer = 0;

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        MainCamera = GameObject.Find("Game_Camera").GetComponent<Camera_script>();
        muzzle_R = GameObject.Find("Muzzle_R");
        muzzle_L = GameObject.Find("Muzzle_L");
        Dima_Animator = GetComponent<Animator>();
        RightArmSolver = GameObject.Find("RightArmSolver");
        LeftArmSolver = GameObject.Find("LeftArmSolver");
        Arm_R_Effector = GameObject.Find("Arm_R_Effector");
        Arm_L_Effector = GameObject.Find("Arm_L_Effector");
        overlord = GameObject.Find("OVERLORD").GetComponent<Overlord_script>();
    }

    void Update()
    {
        DimaFunc();
    }

    /*
    void OnCollisionExit2D(Collision2D col) {
        isGrounded = false;
    }

    void OnCollisionStay2D(Collision2D col) {
        isGrounded = true;
    }
    */

    void OnTriggerStay2D(Collider2D col) {
        isGrounded = true;
    }

    void OnTriggerExit2D(Collider2D col) {
        isGrounded = false;
    }

    void DimaFunc() { // Dima movement, shooting and other basic functionalities
        if (health <= 0 && !overlord.playerDead) {
            PlayDeathSound();
            DisableAnimThings();
            DimaDeath();
        }
        if (!overlord.playerDead && !overlord.stageWin) {
            DimaMovement();
            ActionButtonFunc();
            ShootTimerFunc();

            spriteRotation = Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x ? 1 : -1;

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
                DimaJump();
            }

            if (transform.localScale != new Vector3(spriteRotation, 1, 1)) {
                RotateSprite(spriteRotation);
            }

            if (Input.GetMouseButtonDown(0) && shootTimer <= 0) {
                if (overlord.currentBullets > 0) {
                    DimaShoot(); 
                } else {
                    PlayEmptyGunSound();
                }
                shootTimer = 0.5f;
            }
        }
    }

    void DimaMovement() { // Sideways movement
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
        moveDir = movement.x;
    }

    void DimaJump() { // Classic addForce jumping
        rigidBody.AddForce(transform.up * jumpForce);
        PlayJumpSound();
    }

    void RotateSprite(float spriteRotation) { // Sprite rotation towards aiming direction
        if (!GlobalVariables.stop) { // TESTING
            transform.localScale = new Vector3(Mathf.Floor(spriteRotation) , 1, 1);
        }
    }

    void DimaShoot() { // Shoot a bullet towards crosshair
        GameObject muzzle = shootFromRight ? muzzle_R : muzzle_L;
        shootFromRight = !shootFromRight;

        GameObject muzzleFlashObj = Instantiate(muzzleFlash, muzzle.transform.position, Quaternion.identity);
        muzzleFlashObj.transform.parent = muzzle.transform;
        muzzleFlashObj.transform.rotation = muzzle.transform.rotation;

        Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.identity);
        overlord.currentBullets = overlord.currentBullets - 1;

        PlayGunSound();
    }

    void DisableAnimThings() { // Disables animator and arm movement effectors, so they don't interfere with the ragdoll

        Dima_Animator.enabled = false;
        RightArmSolver.SetActive(false);
        LeftArmSolver.SetActive(false);
        Arm_R_Effector.SetActive(false);
        Arm_L_Effector.SetActive(false);
    }

    void DimaDeath() { // Activate ragdoll

        for (int i = 1; i < 16; i++) {
            GameObject bone = GameObject.Find("bone_" + i);

            bone.GetComponent<Rigidbody2D>().simulated = true;
            bone.GetComponent<Collider2D>().enabled = true;
        }

        face_A.SetActive(false);
        face_D.SetActive(true);

        overlord.playerDead = true;
    }

    void ActionButtonFunc() { // sets action button bool
        if (Input.GetKeyDown(KeyCode.E)) {
            pressedActionButton = true;
        }

        if (Input.GetKeyUp(KeyCode.E)) {
            pressedActionButton = false;
        }
    }

    void PlayGunSound() { // plays one of 3 gun sounds
        int number = Random.Range(0, 2);
        gunAudio.clip = gunSounds[number];
        gunAudio.Play();
    }

    void PlayEmptyGunSound() { // plays one of 3 gun sounds
        int number = Random.Range(3, 5);
        gunAudio.clip = gunSounds[number];
        gunAudio.Play();
    }

    void PlayStepSound() { // plays one of 3 step sounds
        int number = Random.Range(0, 2);
        walkAudio.clip = walkSounds[number];
        walkAudio.Play();
    }

    void PlayJumpSound() { // plays jump sound
        jumpAudio.clip = jumpSound;
        jumpAudio.Play();
    }

    void PlayDeathSound() { // plays death sound
        deathAudio.clip = deathSound;
        deathAudio.Play();
    }

    void ShootTimerFunc() { // small delay between shots, so firing sounds don't get twisted
        if (shootTimer > 0) {
            shootTimer -= Time.deltaTime;
        }
    }
}
