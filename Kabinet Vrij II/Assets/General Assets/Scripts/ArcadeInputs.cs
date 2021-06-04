using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class ArcadeInputs
{
    public static bool Red()
    {
        if (Joystick.current == null) { return Input.GetKey(KeyCode.J); }
        return Joystick.current["button5"].IsPressed() || Input.GetKey(KeyCode.J);
    }

    public static bool Green()
    {
        if (Joystick.current == null) { return Input.GetKey(KeyCode.I); }

        return Joystick.current["button2"].IsPressed() || Input.GetKey(KeyCode.I);
    }

    public static bool Blue()
    {
        if (Joystick.current == null) { return Input.GetKey(KeyCode.K); }

        return Joystick.current["button3"].IsPressed() || Input.GetKey(KeyCode.K);
    }

    public static bool Yellow()
    {
        if (Joystick.current == null) { return Input.GetKey(KeyCode.U); }

        return Joystick.current["button4"].IsPressed() || Input.GetKey(KeyCode.U);
    }

    public static bool Select()
    {
        if (Joystick.current == null) { return Input.GetKey(KeyCode.Space); }

        return Joystick.current["button12"].IsPressed() || Input.GetKey(KeyCode.Space);
    }

    public static Vector2 Stick()
    {
        if (Joystick.current == null)
        {
            return new Vector2(0, 0);
        }
        return Joystick.current.stick.ReadValue();
    }

    public static bool StickUp()
    {
        if (Joystick.current == null)
        {
            return false;
        }
        return (Joystick.current.stick.ReadValue().y == 1);
    }

    public static bool StickDown()
    {
        if (Joystick.current == null)
        {
            return false;
        }
        return (Joystick.current.stick.ReadValue().y == -1);
    }

    public static bool StickLeft()
    {
        if (Joystick.current == null)
        {
            return false;
        }
        return (Joystick.current.stick.ReadValue().x == -1);
    }

    public static bool StickRight()
    {
        if (Joystick.current == null)
        {
            return false;
        }
        return (Joystick.current.stick.ReadValue().x == 1);
    }
}
