using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float moveSpeed;
    public KeyCode destroyKey;
    public Transform destroyPos;
    bool canBePlayed;

    public void Init(Transform dp)
    {
        canBePlayed = false;
        moveSpeed = GameManager.Instance.beatDistance;
        destroyPos = dp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, destroyPos.position) < GameManager.Instance.maxDis)
        {
            canBePlayed = true;
        }
        else
        {
            if (canBePlayed) { GameManager.Instance.MissNote(this); return; }
        }

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        if (Input.GetKeyDown(destroyKey) && canBePlayed) { GameManager.Instance.PlayNote(this); }
    }
}
