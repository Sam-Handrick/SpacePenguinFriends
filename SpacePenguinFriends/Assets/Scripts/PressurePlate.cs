using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : InteractableObject
{
    private bool isPressed;
    private int numOnSwitch;
    public bool requiresConstantWeight;

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
        numOnSwitch = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        numOnSwitch++;
        if (requiresConstantWeight)
        {
            isPressed = numOnSwitch > 0;
        }
        else
        {
            isPressed = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (requiresConstantWeight)
        {
            isPressed = numOnSwitch > 0;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        numOnSwitch--;
        if (requiresConstantWeight)
        {
            isPressed = numOnSwitch > 0;
        }
    }


    public bool IsPressed()
    {
        return isPressed;
    }
}
