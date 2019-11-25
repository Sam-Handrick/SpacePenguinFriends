using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSwitch : InteractableObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        // Just a proof of concept example - please do something cool with this
        transform.localScale += new Vector3(5.0f, 0.0f, 0.0f);
    }
}
