using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractScript : MonoBehaviour
{
    private Collider playerCollider;
    private GameObject objectInteractedWith;
    private LevelManager lm;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("LevelManager");
        lm = obj.GetComponent<LevelManager>();
        Debug.Assert(lm != null, "ERROR: Cannot find LevelManager! Make sure there is exactly one in the scene.");

        playerCollider = GetComponent<Collider>();
        objectInteractedWith = null;
    }

    // Update is called once per frame
    void Update()
    {
        // Interact with the nearest object on button press
        if (Input.GetButtonDown("Circle") && objectInteractedWith != null)
        {
            InteractableObject interObj = objectInteractedWith.GetComponent<InteractableObject>();
            Debug.Assert(interObj != null, "ERROR: Cannot find this object's InteractableObject script.");
            interObj.Interact();
        }

        // Use powers
        if (Input.GetButtonDown("Square") || Input.GetButtonDown("Submit") && lm.currFriends > 0)
        {
            // Use first friend's power
            FriendScript leader = lm.GetFrontFriend();
            leader.UsePower();

            // Shuffle that friend to the back and the rest forward
            int idx = lm.GetFrontIndex();
            for(int j = 0; j < lm.currFriends; ++j)
            {
                lm.friendArr[idx].ShuffleQueueForward();
                idx++;
                idx = idx % lm.currFriends;
            }

            lm.IncrementFrontIndex();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collidingObj = collision.gameObject;
        string collTag = collidingObj.tag;
        if (collTag == "Interaction")
        {
            objectInteractedWith = collidingObj;
        }
        else if (collTag == "Friend")
        {
            FriendScript fren = collidingObj.GetComponent<FriendScript>();
            Debug.Assert(lm.currFriends < LevelManager.maxFriends, "ERROR: Trying to add too many friends!");
            lm.friendArr[lm.currFriends] = fren;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Remove object when exiting collision volume
        objectInteractedWith = null;
    }


}
