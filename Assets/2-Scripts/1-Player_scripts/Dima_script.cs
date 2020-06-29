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
    public int bullets = 30;
    private GameObject muzzle_R;
    private GameObject muzzle_L;
    public Transform bulletPrefab;
    public int health = 1;
    public float spriteRotation = 1;
    private bool shootFromRight = true;
    private GameObject face_A;
    private GameObject face_D;
    public float moveDir = 0; 

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        MainCamera = GameObject.Find("Camera").GetComponent<Camera_script>();
        muzzle_R = GameObject.Find("Muzzle_R");
        muzzle_L = GameObject.Find("Muzzle_L");

        face_A = GameObject.Find("Face_A");
        face_D = GameObject.Find("Face_D");

        face_D.SetActive(false);
    }

    void Update()
    {
        if (health > 0) {
            DimaMovement();

            spriteRotation = Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x ? 1 : -1;

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
                DimaJump();
            }
            if (transform.localScale != new Vector3(spriteRotation, 1, 1)) {
                RotateSprite(spriteRotation);
            }
            if(Input.GetMouseButtonDown(0) && bullets != 0) {
                DimaShoot();
            }
        }

        if (health <= 0) {
            // TODO: proper death stuff
            print("DED");

            face_A.SetActive(false);
            face_D.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        isGrounded = false;
    }

    void OnCollisionStay2D(Collision2D col) {
        isGrounded = true;
    }

    
    void DimaMovement() { // Sideways movement
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
        moveDir = movement.x;
    }

    void DimaJump() { // Classic addForce jumping
        rigidBody.AddForce(transform.up * jumpForce);
    }

    void RotateSprite(float spriteRotation) { // Sprite rotation towards aiming direction
        if (!GlobalVariables.stop) { // TESTING
            transform.localScale = new Vector3(Mathf.Floor(spriteRotation) , 1, 1);
        }
    }

    void DimaShoot() { // Shoot a bullet towards crosshair
        GameObject muzzle = shootFromRight ? muzzle_R : muzzle_L;
        shootFromRight = !shootFromRight;

        Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.identity);
        bullets = bullets - 1;
    }
}
