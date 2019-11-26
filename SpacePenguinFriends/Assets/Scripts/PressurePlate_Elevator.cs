using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate_Elevator : MonoBehaviour
{
    public GameObject elevator;
    public float maxHeight = 5;
    public float minHeight = 1;
    public float rateUp = 0.1f;
    public float rateDown = 0.2f;
    private bool tagEntered = false;
    public List<string> elevatorTag = new List<string>(new string[] {"Player"});
    
    //public Animator anim;
    //Old Array for elevatorTag (new List below) -> //public string[] elevatorTag = new string[10];
    
    void goingUp()
    {
        elevator.transform.position += new Vector3(0, rateUp, 0);
    }
    void goingDown()
    {
        elevator.transform.position -= new Vector3(0, rateDown, 0);
    }

    /*
    void Start()
    {
        minHeight = elevator.transform.position.y;
    }
    */

//checks if the Collider's tag is in the elevatorTag array when a GameObject (w/Collider) enters the collider trigger
    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(col.tag.GetType());
        foreach(string eTag in elevatorTag)
        {
            if (col.tag == eTag)
            {
                tagEntered = true;
                //anim.SetTrigger("Elevator_Lift");      
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        Debug.Log(elevator.transform.position.y);
        if (tagEntered == true && elevator.transform.position.y < maxHeight)
        {
            goingUp();
        }
    }

    void OnTriggerExit(Collider col)
    {
        tagEntered = false;
        
        //anim.enabled = true;   
            //Re-enables/resumes the elevator animation which drops back down when the GameObject exits the collider
    }
    void Update()
    {
        if (tagEntered == false && elevator.transform.position.y > minHeight)
        {
            goingDown();
        }
    }

    /*//Used along with OnTriggerEnter to disable/pause the elevator animation at the top
    void pauseAnimationEvent()
    {
        anim.enabled = false;
    }
    */

}
