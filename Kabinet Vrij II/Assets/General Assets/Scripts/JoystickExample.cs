using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JoystickExample : MonoBehaviour
{
    public TextMeshProUGUI textInput1;
    public TextMeshProUGUI textInput2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textInput1.text = "Joystick: " + ArcadeInputs.Stick().ToString() +
            "\n Red button: " + ArcadeInputs.Red() +
            "\n Green button: " + ArcadeInputs.Green() +
            "\n Blue button: " + ArcadeInputs.Blue() +
            "\n Yellow button: " + ArcadeInputs.Yellow() +
            "\n Select button: " + ArcadeInputs.Select();

        textInput2.text = "Joystick: " + ArcadeInputs.StickP2().ToString() +
            "\n Red button: " + ArcadeInputs.RedP2() +
            "\n Green button: " + ArcadeInputs.GreenP2() +
            "\n Blue button: " + ArcadeInputs.BlueP2() +
            "\n Yellow button: " + ArcadeInputs.YellowP2() +
            "\n Select button: " + ArcadeInputs.SelectP2();
    }
}
