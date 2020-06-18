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

    public Camera_script MainCamera; 

    public int bullets = 30;
    public GameObject makarov;

    public Transform bulletPrefab;

    public int health = 1;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Sideways movement
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;

        float spriteRotation = 1;

        // Classic addForce jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rigidBody.AddForce(transform.up * jumpForce);
        }

        // rotate sprite based on crosshair position
        spriteRotation = Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x ? 1 : -1;
        if (transform.localScale != new Vector3(spriteRotation, 1, 1)) {
            transform.localScale = new Vector3(spriteRotation, 1, 1);
        }
        
        // Pew pew
        if(Input.GetMouseButtonDown(0) && bullets != 0) {
            Instantiate(bulletPrefab, makarov.transform.position, Quaternion.identity);
            bullets = bullets - 1;
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

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("bullet") ||
            other.gameObject.CompareTag("hazard")) {
            health = health - 1;
        }
    }
}
