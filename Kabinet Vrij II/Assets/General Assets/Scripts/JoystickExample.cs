using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JoystickExample : MonoBehaviour
{
    public TextMeshProUGUI textInput1;

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
    }
}
