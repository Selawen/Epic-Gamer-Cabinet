using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public Type noteColour;
    public float moveSpeed;

    public enum Type{
        RED,
        YELLOW,
        GREEN,
        BLUE
        }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move note
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
