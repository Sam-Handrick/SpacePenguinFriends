using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    // a reference to the camera in the scene
    GameObject myCamera;

    // the amount by which the player moves, is set using the gamepad's left analog stick vector
    private Vector3 movementVec;

    // float used to rotate the movement vector relative to camera orientation
    private float cameraAngleDiff;

    Rigidbody myRbody;

    // multiplied by the player's movement to alter movement speed
    public float speed;

    private CharacterController charControl;

    // these represent the strings used to leverage Unity's input system and reference inputs in unity's input manager
    string mySButton;
    string myXButton;
    string myTButton;
    string myOButton;
    string myShareButton;
    string myR2Trigger;
    string myL2Trigger;
    string myLeftStick;
    string myRightStick;

    // Start is called before the first frame update
    void Start()
    {
        myOButton = "Circle";
        mySButton = "Square";
        myTButton = "Cross";
        myXButton = "Triangle";
        myR2Trigger = "R2";
        myL2Trigger = "L2";
        myLeftStick = "LeftStick";
        myRightStick = "RightStick";
        myShareButton = "Share";

        myCamera = GameObject.FindWithTag("MainCamera");

        charControl = GetComponent<CharacterController>();

        myRbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movementVec = new Vector3(0, 0, 0);

        // calculate the angle difference between the camera orientation on the xz plane and what is essentially the direction character movement expects to move on
        cameraAngleDiff = Vector3.Angle(new Vector3(0, 0, 1), new Vector3(myCamera.transform.forward.x, 0, myCamera.transform.forward.z));

        // use cross product to get the sign of the angle at hand
        Vector3 cross = Vector3.Cross(new Vector3(0, 0, 1), new Vector3(myCamera.transform.forward.x, 0, myCamera.transform.forward.z));

        // if the cross component relative to the xz plane normal (y) is positive or zero then we do nothing, but if it is negative then we negate the angle
        if (cross.y < 0)
        {
            cameraAngleDiff = -cameraAngleDiff;
        }

        // we get the movement vector usign the left analog stick input
        movementVec.x = (Vector3.left * -(Input.GetAxis(myLeftStick + "X") * speed * Time.deltaTime)).x;
        movementVec.z = (Vector3.forward * -(Input.GetAxis(myLeftStick + "Y") * speed * Time.deltaTime)).z;

        if(movementVec.magnitude <.02f)
        {
            movementVec = new Vector3(0, 0, 0);
        }

        if (movementVec.magnitude <= 0.0f)
        {
            movementVec.x = (Vector3.left * -(Input.GetAxis("Horizontal") * speed * Time.deltaTime)).x;
            movementVec.z = (Vector3.forward * -(Input.GetAxis("Vertical") * speed * Time.deltaTime)).z;
        }

        // we then rotate the movement vector by the camera angle difference we've just calculated
        movementVec = Quaternion.AngleAxis(cameraAngleDiff, Vector3.up) * movementVec;

        Debug.Log(movementVec);

        // translate the player by the finalized movement vector
        myRbody.MovePosition(transform.position + movementVec);

    }
}
