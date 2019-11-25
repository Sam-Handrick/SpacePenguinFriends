using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] interactObjects;
    private int numInteractObjects;

    // Start is called before the first frame update
    void Start()
    {
        if( interactObjects == null )
        {
            interactObjects = GameObject.FindGameObjectsWithTag("Interaction");
        }

        numInteractObjects = interactObjects.Length;
    }

    // Update is called once per frame
    void Update()
    {
        // Just in case
        Debug.Assert( interactObjects.Length == numInteractObjects, "ERROR: Number of interactable objects has changed since start." );
    }
}
