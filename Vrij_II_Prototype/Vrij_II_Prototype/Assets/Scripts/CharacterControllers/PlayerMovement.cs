using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    public KeyCode forward = KeyCode.W;
    public KeyCode back = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    public KeyCode jump = KeyCode.Space;

    public float moveSpeed;
    public float jumpForce;
    public bool grounded;

    Rigidbody rb;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        Vector3 vel = Vector3.zero;
        if(Input.GetKey(forward))
        {
            vel += transform.forward * moveSpeed;
        }   
        if (Input.GetKey(back))
        {
            vel -= transform.forward * moveSpeed;
        }
        if (Input.GetKey(left))
        {
            vel -= transform.right * moveSpeed;

        }
        if (Input.GetKey(right))
        {
            vel += transform.right * moveSpeed;
        }
        rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);

        if (Input.GetKeyDown(jump) && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground")) { grounded = true; }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground")) { grounded = false; }
    }
}
