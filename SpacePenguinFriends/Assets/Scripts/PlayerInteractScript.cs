using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractScript : MonoBehaviour
{
    private Collider playerCollider;
    private GameObject objectInteractedWith;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert( GameObject.FindGameObjectsWithTag( "LevelManager" ).Length == 1, "ERROR: There must be exactly one level manager per level.");
        playerCollider = GetComponent<Collider>();
        objectInteractedWith = null;
    }

    // Update is called once per frame
    void Update()
    {
        // Uncomment the GetButtonDown() bit to require pressing a button once we have those controls set up
        if( /*Input.GetButtonDown( "InteractButton" ) &&*/ objectInteractedWith != null )
        {
            InteractableObject interObj = (InteractableObject) objectInteractedWith.GetComponent(typeof(InteractableObject));
            Debug.Assert(interObj != null, "ERROR: Trying to interact with an object that doesn't have an InteractableObject script component.");
            interObj.Interact();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Upon entering an interactable object's collision volume, get the object itself
        GameObject collidingObj = collision.gameObject;
        if ( collidingObj.tag == "Interaction" )
        {
            objectInteractedWith = collidingObj;
        }
    }

    void OnCollisionExit( Collision collision )
    {
        // Remove object when exiting collision volume
        objectInteractedWith = null;
    }


}
