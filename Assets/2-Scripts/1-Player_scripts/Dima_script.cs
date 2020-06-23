using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dima_script : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 400f;
    private Rigidbody2D rigidBody;
    private bool isGrounded = true;
    private bool aiming = false;
    private Camera_script MainCamera; 
    public int bullets = 30;
    private GameObject muzzle;
    public Transform bulletPrefab;
    public int health = 1;

    private Overlord_script overlord;

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        MainCamera = GameObject.Find("Camera").GetComponent<Camera_script>();
        muzzle = GameObject.Find("Muzzle");

        overlord = GameObject.Find("OVERLORD").GetComponent<Overlord_script>();
    }

    void Update()
    {
        // if (health > 0) {
            DimaMovement();
        // }
        
        float spriteRotation = Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x ? 1 : -1;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            DimaJump();
        }
        if (transform.localScale != new Vector3(spriteRotation, 1, 1)) {
            RotateSprite(spriteRotation);
        }
        if(Input.GetMouseButtonDown(0) && bullets != 0) {
            DimaShoot();
        }

        if (health <= 0) {
            // TODO: proper death stuff
            print("DED");
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col) {
        isGrounded = false;
    }

    
    void DimaMovement() { // Sideways movement
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
    }

    void DimaJump() { // Classic addForce jumping
        rigidBody.AddForce(transform.up * jumpForce);
    }

    void RotateSprite(float spriteRotation) { // Sprite rotation towards aiming direction
        if (!overlord.stop) { // TESTING
            transform.localScale = new Vector3(Mathf.Floor(spriteRotation) , 1, 1);
        }
    }

    void DimaShoot() { // Shoot a bullet towards crosshair
        Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.identity);
        bullets = bullets - 1;
    }
}
