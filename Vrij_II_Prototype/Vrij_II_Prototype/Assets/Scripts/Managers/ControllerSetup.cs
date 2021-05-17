using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSetup : MonoBehaviour
{
    public static ControllerSetup Instance;
    public string upButton, downButton, leftButton, rightButton;


    private void Awake()
    {
        if (Instance) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
