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
        Quaternion rot = transform.rotation;
        Quaternion roth = Quaternion.identity;
        Quaternion rotv = Quaternion.identity;

        if (Input.GetKey(forward))
        {
            rotv = Quaternion.Euler(0, 0, 0);
            rot = Quaternion.Euler(0, 0, 0);

        }
        if (Input.GetKey(back))
        {
            rotv = Quaternion.Euler(0, 180, 0);
            rot = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKey(right))
        {
            roth = Quaternion.Euler(0, 90, 0);
            rot = Quaternion.Euler(0, 90, 0);
        }
        if (Input.GetKey(left))
        {
            roth = Quaternion.Euler(0, 270, 0);
            rot = Quaternion.Euler(0, 270, 0);
        }

        if ((Input.GetKey(left) || Input.GetKey(right)) && (Input.GetKey(forward) || Input.GetKey(back)))
        {
            rot = Quaternion.Lerp(roth, rotv, 0.5f);
        }
        
        transform.rotation = rot;
    }
}
