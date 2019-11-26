using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject door;
    public Animator anim;

    void OnTriggerEnter(Collider col)
    {
        //door.transform.position += new Vector3(0, 4, 0);
        anim.SetTrigger("DoorOpen");      
    }

    void OnTriggerExit(Collider col)
    {
        //door.transform.position += new Vector3(0, -4, 0);
        anim.enabled = true;
    }

    void pauseAnimationEvent()
    {
        anim.enabled = false;
    }
}
