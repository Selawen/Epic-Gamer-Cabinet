using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class ArcadeInputs
{
    public static bool Red()
    {
        return Joystick.current["button5"].IsPressed() || Input.GetKeyDown(KeyCode.J);
    }

    public static bool Green()
    {
        return Joystick.current["button2"].IsPressed() || Input.GetKeyDown(KeyCode.I);
    }

    public static bool Blue()
    {
        return Joystick.current["button3"].IsPressed() || Input.GetKeyDown(KeyCode.K);
    }

    public static bool Yellow()
    {
        return Joystick.current["button4"].IsPressed() || Input.GetKeyDown(KeyCode.U);
    }

    public static bool Select()
    {
        return Joystick.current["button12"].IsPressed() || Input.GetKeyDown(KeyCode.Space);
    }

}
