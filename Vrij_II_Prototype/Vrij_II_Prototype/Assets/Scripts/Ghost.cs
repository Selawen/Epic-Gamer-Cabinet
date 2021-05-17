using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GhostColor { Yellow, Green, Blue, Red};

public class Ghost : MonoBehaviour
{
    public GhostColor myColor;
    public Material[] mats;
    Rigidbody rb;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myColor = (GhostColor)Random.Range((int)GhostColor.Yellow, (int)GhostColor.Red + 1); //Ugly enum conversion bullshit

        foreach(Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material = mats[(int)myColor];
        }
    }



    private void FixedUpdate()
    {
        transform.LookAt(Vector3.Scale(PlayerMovement.Instance.transform.position, new Vector3(1, 0, 1) + new Vector3(0, transform.position.y, 0)));
        rb.velocity = moveSpeed * transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Circle")) { Destroy(gameObject); }
        if (other.CompareTag("Bullet")) { if (other.GetComponent<Bullet>().myColor == myColor) { GameManager.Instance.ScoreGhost(); Destroy(gameObject); } }
    }
}
