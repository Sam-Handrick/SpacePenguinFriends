using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate_Elevator : MonoBehaviour
{
    //public GameObject elevator;
    public Animator anim;
    //Old Array for elevatorTag (new List below) -> //public string[] elevatorTag = new string[10];
    public List<string> elevatorTag = new List<string>(new string[] {"Player"});
    


//Used along with OnTriggerEnter to disable/pause the elevator animation at the top
    void pauseAnimationEvent()
    {
        anim.enabled = false;
    }

//checks if the Collider's tag is in the elevatorTag array when a GameObject (w/Collider) enters the collider trigger
    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(col.tag.GetType());
        foreach(string eTag in elevatorTag)
        {
            if (col.tag == eTag || col.tag == "Player")
            {
                //elevator.transform.position += new Vector3(0, 4, 0);
                anim.SetTrigger("Elevator_Lift");      
            }
        }
    }

//Re-enables/resumes the elevator animation which drops back down when the GameObject exits the collider
    void OnTriggerExit(Collider col)
    {
        //elevator.transform.position += new Vector3(0, -4, 0);
        anim.enabled = true;
    }


}
