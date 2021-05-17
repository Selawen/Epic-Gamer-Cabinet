using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Note : MonoBehaviour
{
    public float moveSpeed;
    public KeyCode destroyKey;
    public string destroyKey2;
    public Transform destroyPos;
    bool canBePlayed;

    bool pressed = false;

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
        if ((Input.GetKeyDown(destroyKey) ||  (Gamepad.current[destroyKey2].IsPressed() && !pressed)) && canBePlayed)
        {
            GameManager.Instance.PlayNote(this);
            pressed = true;
        }
        if (!Gamepad.current[destroyKey2].IsPressed() && pressed)
        {
            pressed = false;
        }
    }
}
