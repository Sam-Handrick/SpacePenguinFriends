using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushPull : MonoBehaviour
{
    enum Direction { Forward, Backward, Right, Left};
    Direction myDirection;
    GameObject currentBlock;

    bool locked;

    bool blockMovingLock;
    Vector3 blockMoveVec;

    // Start is called before the first frame update
    void Start()
    {
        myDirection = Direction.Forward;
        locked = false;
        blockMovingLock = false;
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

        Debug.Log(dotAmount);

        if(dotAmount>.5f)
        {
            blockMovingLock = true;
            blockMoveVec = dotVec;
        }

        if(dotAmount < -.5f)
        {
            blockMovingLock = true;
            blockMoveVec = dotVec;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cross") || Input.GetButtonDown("Submit"))
        {
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

                //transform.position = currentBlock + offset;
                //transform.SetPare
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "PushPull")
        {
            currentBlock = col.gameObject;
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "PushPull" && currentBlock == col.gameObject)
        {
            currentBlock = null;
        }
    }
}
