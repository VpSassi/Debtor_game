using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_script : MonoBehaviour
{    
    private Transform RT;
    private Transform RB;
    private Transform LT;
    private Transform LB;
    public int boxNumber;
    public UnityEngine.Object box;
    private float invisibilyTimer = 5;
    public GameObject breakEffect;

    void Start()
    {
        RT = transform.Find("RT");
        RB = transform.Find("RB");
        LT = transform.Find("LT");
        LB = transform.Find("LB");
    }

    void Update() {
        if (invisibilyTimer > 0) {
            invisibilyTimer =- Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("bullet") && invisibilyTimer < 1 ||
            col.gameObject.CompareTag("blast") && invisibilyTimer < 1 ||
            col.gameObject.CompareTag("burn") && invisibilyTimer < 1) {
            BreakBox();
        }
    }

    void BreakBox() {
        invisibilyTimer = 10;

        GameObject newBreakeffect = Instantiate(breakEffect, transform.position, Quaternion.identity);
        Vector3 newScale = new Vector3(transform.localScale.x * 2, transform.localScale.y * 2, transform.localScale.z * 2);
        newBreakeffect.transform.localScale = newScale;

        if (boxNumber != 3) {
            GameObject box1 = Instantiate(box, RT.position, Quaternion.identity) as GameObject;
            GameObject box2 = Instantiate(box, RB.position, Quaternion.identity) as GameObject;
            GameObject box3 = Instantiate(box, LT.position, Quaternion.identity) as GameObject;
            GameObject box4 = Instantiate(box, LB.position, Quaternion.identity) as GameObject;

            Vector3 halfSize = new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, transform.localScale.z / 2);
            box1.transform.localScale = halfSize;
            box2.transform.localScale = halfSize;
            box3.transform.localScale = halfSize;
            box4.transform.localScale = halfSize;

            box1.GetComponent<Box_script>().boxNumber = boxNumber + 1;
            box2.GetComponent<Box_script>().boxNumber = boxNumber + 1;
            box3.GetComponent<Box_script>().boxNumber = boxNumber + 1;
            box4.GetComponent<Box_script>().boxNumber = boxNumber + 1;

            float force = 5000;
            box1.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1000, 1000), Random.Range(-1000, 1000) * force));
            box2.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1000, 1000), Random.Range(-1000, 1000) * force));
            box3.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1000, 1000), Random.Range(-1000, 1000) * force));
            box4.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1000, 1000), Random.Range(-1000, 1000) * force));
        }

        Destroy(gameObject);
    }
}
