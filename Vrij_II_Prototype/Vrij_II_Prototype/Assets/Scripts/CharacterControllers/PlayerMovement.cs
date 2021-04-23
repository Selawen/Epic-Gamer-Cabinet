using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    public KeyCode forward = KeyCode.W;
    public KeyCode back = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    public KeyCode yellow = KeyCode.I, green = KeyCode.L, blue = KeyCode.K, red = KeyCode.J;
    public bool freeRange;
    public float moveSpeed;
    public float fireRate;

    bool canShoot;

    public GameObject bullet;
    public Transform spawnPoint;
    Rigidbody rb;
    Vector3 startPos;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        freeRange = false;
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(HandleShootInput());
    }

    private void Update()
    {
        if (freeRange) { HandleMoveInput(); }
    }

    IEnumerator HandleShootInput()
    {
        while (true)
        {
            canShoot = true;

            while (!freeRange)
            {
                yield return null;
            }
            while (!Input.GetKeyDown(yellow) && !Input.GetKeyDown(red) && !Input.GetKeyDown(blue) && !Input.GetKeyDown(green))
            {
                yield return null;
            }
            
            var gamepad = Gamepad.current;
            if ((Input.GetKeyDown(yellow) || gamepad.yButton.wasPressedThisFrame) && canShoot)
            {
                Bullet b = Instantiate(bullet, spawnPoint).GetComponent<Bullet>();
                b.Init(GhostColor.Yellow);
                canShoot = false;
            }
            if ((Input.GetKeyDown(red) || gamepad.xButton.wasPressedThisFrame) && canShoot)
            {
                Bullet b = Instantiate(bullet, spawnPoint).GetComponent<Bullet>();
                b.Init(GhostColor.Red);
                canShoot = false;
            }
            if ((Input.GetKeyDown(green) || gamepad.bButton.wasPressedThisFrame) && canShoot)
            {
                Bullet b = Instantiate(bullet, spawnPoint).GetComponent<Bullet>();
                b.Init(GhostColor.Green);
                canShoot = false;
            }
            if ((Input.GetKeyDown(blue) || gamepad.aButton.wasPressedThisFrame) && canShoot)
            {
                Bullet b = Instantiate(bullet, spawnPoint).GetComponent<Bullet>();
                b.Init(GhostColor.Blue);
                canShoot = false;
            }
            yield return new WaitForSeconds(1 / fireRate);
        }
    }

    void HandleMoveInput()
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

        if (Input.GetKey(left) || Input.GetKey(right) || Input.GetKey(forward) || Input.GetKey(back))
        {
            rb.velocity = transform.forward * moveSpeed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        
        transform.rotation = rot;
    }

    public void SetMove(bool canMove)
    {
        freeRange = canMove;
        if (!freeRange) { transform.position = startPos; transform.rotation = Quaternion.identity; rb.velocity = Vector3.zero; }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Ghost"))
        {
            GameManager.Instance.BoostValue -= 0.1f;
            Destroy(collision.collider.GetComponentInParent<Ghost>().gameObject);
        }
                
    }
}
