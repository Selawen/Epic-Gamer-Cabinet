using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO.Ports;

public static class ArcadeInputs
{
    private static SerialPort arduinoSerial = new SerialPort("COM4", 9600, Parity.None);

    /// <summary>
    /// returns true if red button player 1 is pressed
    /// </summary>
    /// <returns></returns>
    public static bool Red()
    {
        if (Joystick.current == null) { return Input.GetKey(KeyCode.J); }
        return Joystick.current["button5"].IsPressed() || Input.GetKey(KeyCode.J);
    }
    /// <summary>
    /// returns true if green button player 1 is pressed
    /// </summary>
    /// <returns></returns>
    public static bool Green()
    {
        if (Joystick.current == null) { return Input.GetKey(KeyCode.I); }

        return Joystick.current["button2"].IsPressed() || Input.GetKey(KeyCode.I);
    }

    /// <summary>
    /// returns true if blue button player 1 is pressed
    /// </summary>
    /// <returns></returns>
    public static bool Blue()
    {
        if (Joystick.current == null) { return Input.GetKey(KeyCode.K); }

        return Joystick.current["button3"].IsPressed() || Input.GetKey(KeyCode.K);
    }

    /// <summary>
    /// returns true if yellow button player 1 is pressed
    /// </summary>
    /// <returns></returns>
    public static bool Yellow()
    {
        if (Joystick.current == null) { return Input.GetKey(KeyCode.U); }

        return Joystick.current["button4"].IsPressed() || Input.GetKey(KeyCode.U);
    }

    /// <summary>
    /// returns true if select button player 1 is pressed
    /// </summary>
    /// <returns></returns>
    public static bool Select()
    {
        if (Joystick.current == null) { return Input.GetKey(KeyCode.Space); }

        return Joystick.current["button12"].IsPressed() || Input.GetKey(KeyCode.Space);
    }

    /// <summary>
    /// returns vector2 for joystick player 1, (0.0,-1.0) is straight down, (0.7,0.7) is upper right
    /// </summary>
    /// <returns></returns>
    public static Vector2 Stick()
    {
        if (Joystick.current == null)
        {
            return new Vector2(0, 0);
        }
        return Joystick.current.stick.ReadValue();
    }

    /// <summary>
    /// returns true if joystick player 1 is up
    /// </summary>
    /// <returns></returns>
    public static bool StickUp()
    {
        if (Joystick.current == null)
        {
            return false;
        }
        return (Joystick.current.stick.ReadValue().y == 1);
    }

    /// <summary>
    /// returns true if joystick player 1 is down
    /// </summary>
    /// <returns></returns>
    public static bool StickDown()
    {
        if (Joystick.current == null)
        {
            return false;
        }
        return (Joystick.current.stick.ReadValue().y == -1);
    }

    /// <summary>
    /// returns true if joystick player 1 is left
    /// </summary>
    /// <returns></returns>
    public static bool StickLeft()
    {
        if (Joystick.current == null)
        {
            return false;
        }
        return (Joystick.current.stick.ReadValue().x == -1);
    }

    /// <summary>
    /// returns true if joystick player 1 is right
    /// </summary>
    /// <returns></returns>
    public static bool StickRight()
    {
        if (Joystick.current == null)
        {
            return false;
        }
        return (Joystick.current.stick.ReadValue().x == 1);
    }

    /// <summary>
    /// returns vector2 for joystick player 2, (0.0,-1.0) is straight down, (0.7,0.7) is upper right
    /// </summary>
    /// <returns></returns>
    public static Vector2 StickP2()
    {
        return SerialVector2();
    }

    /// <summary>
    /// returns true if joystick player 2 is up
    /// </summary>
    /// <returns></returns>
    public static bool StickUpP2()
    {
        return SerialVector2().y == 1;
    }

    /// <summary>
    /// returns true if joystick player 2 is down
    /// </summary>
    /// <returns></returns>
    public static bool StickDownP2()
    {
        return SerialVector2().y == -1;
    }

    /// <summary>
    /// returns true if joystick player 2 is left
    /// </summary>
    /// <returns></returns>
    public static bool StickLeftP2()
    {
            return SerialVector2().x == -1;
    }

    /// <summary>
    /// returns true if joystick player 2 is right
    /// </summary>
    /// <returns></returns>
    public static bool StickRightP2()
    {
        return SerialVector2().x == 1;
    }

    private static Vector2 SerialVector2()
    {
        if (!arduinoSerial.IsOpen)
        {
            arduinoSerial.Open();
            arduinoSerial.ReadTimeout = 100;
            arduinoSerial.Handshake = Handshake.None;
        }
        float x; float y;
        string vectorString = arduinoSerial.ReadLine().Split('~')[0];
        switch (vectorString)
        {
            case "0,0":
                x = 0; y = 0;
                break;
            case "1,0":
                x = 1; y = 0;
                break;
            case "-1,0":
                x = -1; y = 0;
                break;
            case "0,1":
                x = 0; y = 1;
                break;
            case "0,-1":
                x = 0; y = -1;
                break;
            case "1,1":
                x = 0.7f; y = 0.7f;
                break;
            case "1,-1":
                x = 0.7f; y = -0.7f;
                break;
            case "-1,-1":
                x = -0.7f; y = -0.7f;
                break;
            case "-1,1":
                x = -0.7f; y = 0.7f;
                break;
            default:
                x = 0; y = 0;
                break;
        }
        //Debug.Log(vectorString);
        return new Vector2(x, y);
    }

    private static int SerialButton(Note.Type colour)
    {
        if (!arduinoSerial.IsOpen)
        {
            arduinoSerial.Open();
            arduinoSerial.ReadTimeout = 100;
            arduinoSerial.Handshake = Handshake.None;
        }

        string buttonString = arduinoSerial.ReadLine().Split('~')[1];
        buttonString = buttonString.Split(',')[(int)colour];

        return 0;
    }
}
