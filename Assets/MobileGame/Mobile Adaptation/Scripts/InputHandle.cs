using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandle : MonoBehaviour
{
    [SerializeField]
    public static Vector2 joystickAxis = new Vector2();
    [SerializeField]
    public static List<bool> buttonDown = new List<bool>();
    [SerializeField]
    public static List<bool> buttonPressed = new List<bool>();
    bool aDownAux = false;
    bool bDownAux = false;
    public Joystick joystick;
    public Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        buttonDown = new List<bool>();
        buttonPressed = new List<bool>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonDown.Add(new bool());
            buttonPressed.Add(new bool());
        }
        joystickAxis = new Vector2(joystick.Horizontal, joystick.Vertical);
        for(int i = 0; i<buttons.Length;i++)
        {
            buttonPressed[i] = buttons[i].gameObject.GetComponent<ButtonHandle>().pressed;

            buttonDown[i] = buttons[i].gameObject.GetComponent<ButtonHandle>().down;
        }
        


        //Debug.Log(joystickAxis);
    }
}
