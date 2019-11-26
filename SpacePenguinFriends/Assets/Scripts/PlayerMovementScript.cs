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

    public float jumpForce;

    bool airborne;

    private CharacterController charControl;

    public float jumpDelayTime = .5f;

    private float currentJumpWaitTime;

    // raycast hit used for ground detection
    private RaycastHit hit;

    // ray used for ground detection
    private Ray ray;

    PlayerPushPull pushPullComponent;

    Animator animator;

    // these represent the strings used to leverage Unity's input system and reference inputs in unity's input manager
    string circleButton;
    string squareButton;
    string crossButton;
    string triangleButton;
    string myShareButton;
    string myR2Trigger;
    string myL2Trigger;
    string myLeftStick;
    string myRightStick;

    // Start is called before the first frame update
    void Start()
    {
        circleButton = "Circle";
        squareButton = "Square";
        crossButton = "Cross";
        triangleButton = "Triangle";
        myR2Trigger = "R2";
        myL2Trigger = "L2";
        myLeftStick = "LeftStick";
        myRightStick = "RightStick";
        myShareButton = "Share";

        myCamera = GameObject.FindWithTag("MainCamera");

        charControl = GetComponent<CharacterController>();

        myRbody = GetComponent<Rigidbody>();

        airborne = false;

        animator = transform.GetChild(0).GetChild(0).GetComponent<Animator>();

        pushPullComponent = GetComponent<PlayerPushPull>();
        currentJumpWaitTime = 0.0f;
    }

    // the updates for physics-relative checks done on a fixed schedule
    void FixedUpdate()
    {

        ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out hit, 1.0f))
        {
            if (!(hit.collider.isTrigger))
            {
                if(airborne)
                    animator.SetBool("Landed", true);

                airborne = false;
                animator.SetBool("Jump", false);
            }

        }
        else
        {
            airborne = true;
            animator.SetBool("Jump", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentJumpWaitTime > 0.0f)
            currentJumpWaitTime -= Time.deltaTime;

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
        movementVec.x = (Vector3.left * -(Input.GetAxis(myLeftStick + "X"))).x;
        movementVec.z = (Vector3.forward * -(Input.GetAxis(myLeftStick + "Y"))).z;

        if(movementVec.magnitude <.1f)
        {
            movementVec = new Vector3(0, 0, 0);
        }

        if (movementVec.sqrMagnitude > 1.0f) movementVec.Normalize();

        movementVec = movementVec * speed * Time.deltaTime;

        if (movementVec.magnitude <= 0.0f)
        {
            movementVec.x = (Vector3.left * -(Input.GetAxis("Horizontal") * speed * Time.deltaTime)).x;
            movementVec.z = (Vector3.forward * -(Input.GetAxis("Vertical") * speed * Time.deltaTime)).z;
        }


        if(movementVec.magnitude > 0.0f)
            animator.SetBool("Walking", true);
        else
            animator.SetBool("Walking", false);


        // we then rotate the movement vector by the camera angle difference we've just calculated
        movementVec = Quaternion.AngleAxis(cameraAngleDiff, Vector3.up) * movementVec;


        if(pushPullComponent.IsLockedToBlock())
        {
            pushPullComponent.ApplyMotionToBlock(movementVec);
            return;
        }

        // translate the player by the finalized movement vector
        myRbody.MovePosition(transform.position + movementVec);

        if(movementVec.magnitude >.025f)
            transform.GetChild(0).forward = movementVec.normalized;



        bool jump = Input.GetButtonDown("Jump") || Input.GetButtonDown("Cross");

        if(jump && !airborne && currentJumpWaitTime <= 0.0f && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hop"))
        {
            myRbody.AddForce(jumpForce * new Vector3(0, 1, 0));

            animator.SetBool("Jump", true);
            animator.Play("Hop", 0, 0.25f);

            currentJumpWaitTime = jumpDelayTime;
        }

    }

    public Vector3 GetForwardVec()
    {
        if (movementVec.magnitude < 0.1f)
        {
            return new Vector3(0, 0, 0);
        }

        return Vector3.Normalize(movementVec);
    }
}
