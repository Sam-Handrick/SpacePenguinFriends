using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Note that this is an abstract class! You can never create an InteractableObject directly
// Always inherit from this and override the Interact() function appropriately
public abstract class InteractableObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Interact()
    {
        // I don't think this is possible, but let's error just in case
        Debug.LogError( "ERROR: Calling Interact() on a base InteractableObject!" );
    }
}
