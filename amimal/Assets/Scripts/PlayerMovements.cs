using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public bool grounded = true;
    public float jumpPower, moveSpeed;
    public Rigidbody2D rb;
    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float yVel = rb.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space)&&grounded)
        {
            yVel = jumpPower;
            grounded = false;
        }
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, yVel);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "map")
        {
            grounded = true;
        }
    }
}
