using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float moveSpeed;
    public KeyCode destroyKey;
    public Transform destroyPos;

    public void Init(Transform dp)
    {
        moveSpeed = GameManager.Instance.beatDistance;
        destroyPos = dp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        if (Input.GetKeyDown(destroyKey)) { PlayNote(); }
    }

    void PlayNote()
    {
        if(Vector3.Distance(transform.position, destroyPos.position) < GameManager.Instance.maxDis)
        {
            GameManager.Instance.PlayNote(this);
        }
    }
}
