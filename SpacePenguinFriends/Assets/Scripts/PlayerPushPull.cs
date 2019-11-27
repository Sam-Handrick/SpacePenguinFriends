using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushPull : MonoBehaviour
{
    enum Direction { Forward, Backward, Right, Left};
    Direction myDirection;
    GameObject currentBlock;
    private float currentMoveDistance;

    bool locked;

    bool blockMovingLock;
    Vector3 blockMoveVec;

    Animator animator;

    Vector3 currentBlockOffset;

    // Start is called before the first frame update
    void Start()
    {
        myDirection = Direction.Forward;
        locked = false;
        blockMovingLock = false;
        currentMoveDistance = 0;
        currentBlockOffset = new Vector3(0, 0, 0);

        animator = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    public bool IsLockedToBlock()
    {
        return locked;
    }

    public void ApplyMotionToBlock(Vector3 movementVec)
    {
        if (blockMovingLock)
            return;

        Vector3 dotVec;

        switch (myDirection)
        {
            case Direction.Forward:
                dotVec = currentBlock.transform.forward;
                break;
            case Direction.Backward:
                dotVec = currentBlock.transform.forward;
                break;
            case Direction.Right:
                dotVec = currentBlock.transform.right;
                break;
            case Direction.Left:
                dotVec = currentBlock.transform.right;
                break;
            default:
                dotVec = new Vector3(0, 0, 0);
                break;
        }

        float dotAmount = Vector3.Dot(dotVec, movementVec.normalized);

        blockMoveVec = new Vector3(0, 0, 0);

        if (dotAmount>.5f)
        {
            blockMovingLock = true;
            blockMoveVec = dotVec;
        }

        if(dotAmount < -.5f)
        {
            blockMovingLock = true;
            blockMoveVec = -dotVec;
        }

        Vector3 toPlayer = transform.position - currentBlock.transform.position;
        float dot = Vector3.Dot(toPlayer, blockMoveVec);

        if(dot>=0)
        {
            animator.SetBool("Pulling", true);
            animator.SetBool("Pushing", false);
        }
        else
        {
            animator.SetBool("Pushing", true);
            animator.SetBool("Pulling", false);
        }

        currentMoveDistance = 0.0f;

        //currentBlock.GetComponent<Rigidbody>().MovePosition(currentBlock.transform.position + (blockMoveVec * Time.deltaTime * 3));
        //GetComponent<Rigidbody>().MovePosition(transform.position + (blockMoveVec * Time.deltaTime * 3));
    }

    // Update is called once per frame
    void Update()
    {
        if (blockMovingLock)
        {
            Vector3 nextDist = blockMoveVec * Time.deltaTime * currentBlock.GetComponent<PushPullScript>().GetMoveSpeed();

            currentMoveDistance += nextDist.magnitude;

            if (currentMoveDistance > currentBlock.GetComponent<PushPullScript>().GetPushDistance())
            {
                float extraDistance = currentMoveDistance - currentBlock.GetComponent<PushPullScript>().GetPushDistance();
                float newMagnitude = nextDist.magnitude - extraDistance;
                nextDist = nextDist.normalized * newMagnitude;
            }

            currentBlock.GetComponent<Rigidbody>().MovePosition(currentBlock.transform.position + (nextDist));
            transform.position += (nextDist);

            if (currentMoveDistance > currentBlock.GetComponent<PushPullScript>().GetPushDistance())
            {
                blockMovingLock = false;
            }
        }

        if(Input.GetButtonDown("Square") || Input.GetButtonDown("Submit"))
        {
            if (locked)
            {
                if (!blockMovingLock)
                { 
                    transform.SetParent(null);
                    Vector3 direction = transform.position - currentBlock.transform.position;
                    direction = direction.normalized * .5f;
                    GetComponent<Rigidbody>().MovePosition(transform.position + (direction));
                    locked = false;
                    animator.SetBool("OnBlock", false);
                }

                return;
            }

            if(currentBlock)
            {

                Vector3 blockToPlayer = transform.position - currentBlock.transform.position;

                float forwardDot = Vector3.Dot(blockToPlayer, currentBlock.transform.forward);

                float backwardDot = Vector3.Dot(blockToPlayer, -currentBlock.transform.forward);

                float rightDot = Vector3.Dot(blockToPlayer, currentBlock.transform.right);

                float leftDot = Vector3.Dot(blockToPlayer, -currentBlock.transform.right);

                Vector3 offset = new Vector3(0,0,0);

                if(forwardDot>= backwardDot && forwardDot>= rightDot && forwardDot>=leftDot)
                {
                    myDirection = Direction.Forward;
                    offset = currentBlock.transform.forward;
                }
                else if (backwardDot >= forwardDot && backwardDot >= rightDot && backwardDot >= leftDot)
                {
                    myDirection = Direction.Backward;
                    offset = -currentBlock.transform.forward;
                }
                else if (rightDot >= forwardDot && rightDot >= backwardDot && rightDot >= leftDot)
                {
                    myDirection = Direction.Right;
                    offset = currentBlock.transform.right;
                }
                else if (leftDot >= forwardDot && leftDot >= backwardDot && leftDot >= rightDot)
                {
                    myDirection = Direction.Left;
                    offset = -currentBlock.transform.right;
                }

                locked = true;
                animator.SetBool("OnBlock", true);

                currentBlockOffset = offset;

                offset *= currentBlock.GetComponent<PushPullScript>().GetPlayerOffsetDistance();
                transform.position = currentBlock.transform.position + offset;
                transform.GetChild(0).forward = (currentBlock.transform.position - transform.position).normalized;
            }
        }
    }

    public void NotifyBlockCollision(GameObject sender)
    {
        if(sender == currentBlock)
        {
            blockMovingLock = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "PushPull")
        {
            currentBlock = col.gameObject;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (!currentBlock && col.gameObject.tag == "PushPull")
        {
            currentBlock = col.gameObject;
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "PushPull" && currentBlock == col.gameObject)
        {
            if(!locked)
                currentBlock = null;
        }
    }
}
