#pragma warning disable CS0108
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public bool grounded = true, canMove = true, jumpRising = false;
    public float jumpPower, moveSpeed;
    public float gravity = 1.5f, fallGravity = 3.0f, jumpHoldDuration = 2.0f;
    public LayerMask whatToScan;
    public BoxCollider2D collider;
    public Rigidbody2D rb;
    public float sideScanDist = 0.2f;
    [SerializeField] float airCounter = 0.0f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        grounded = GroundCheck();
        if (jumpRising)
        {
            if (rb.velocity.y <= 0.0f) jumpRising = false;
            else if (airCounter <= jumpHoldDuration && Input.GetKey(KeyCode.Space)) rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            else
            {
                jumpRising = false;
            }
            airCounter += Time.deltaTime;
        }
        if (canMove && Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            airCounter = 0.0f;
            jumpRising = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = gravity;
        }
        else rb.gravityScale = fallGravity;
        
    }
    private bool GroundCheck()
    {
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0.0f, Vector2.down, 0.1f, whatToScan);
        if (hit.collider != null) return true;
        else return false;
    }
    public void MoveLeft()
    {
        if (!canMove)
        {
            return;
        }
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0.0f, Vector2.left, 0.1f, whatToScan);
        if(hit.collider==null) rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }
    public void MoveRight()
    {
        if (!canMove) return;
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0.0f, Vector2.right, 0.1f, whatToScan);
        if(hit.collider==null) rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }
    /*public void FixPositionX(float x)
    {
        rb.velocity = new Vector2(0.0f, rb.velocity.y);
        transform.position = new Vector2(x, transform.position.y);
    }
    public void FixPositionY(float y)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        transform.position = new Vector2(transform.position.x, y);
    }*/

}
