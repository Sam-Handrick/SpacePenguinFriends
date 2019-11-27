using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Note this is an abstract class! Do not create an instance of FriendScript directly or attach this to any object
// Inherit from this class and implement UsePower() as appropriate
public abstract class FriendScript : MonoBehaviour
{
    private LevelManager lm;
    protected int queuePos; // Starting from 1

    protected GameObject player;
    protected PlayerMovementScript pMove;

    public string friendName;
    protected bool wasAcquired;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // DEBUG
        Debug.Log("Called FriendScript.Start()");

        // Find player and movement script
        player = GameObject.Find("Player");
        Debug.Assert(player != null, "ERROR: Player could not be found!");
        pMove = player.GetComponent<PlayerMovementScript>();
        Debug.Assert(pMove != null, "ERROR: Player's PlayerMovementScript could not be found!");

        // Find level manager
        GameObject obj = GameObject.Find("LevelManager");
        lm = obj.GetComponent<LevelManager>();
        Debug.Assert(lm != null, "ERROR: Cannot find LevelManager! Make sure there is exactly one in the scene.");

        wasAcquired = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // DEBUG
        Debug.Log("Called FriendScript.Update()");
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        GameObject collidingObj = collision.gameObject;
        if (collidingObj.tag == "Player")
        {
            // Teleport the friend to the last spot in the queue
            queuePos = ++lm.currFriends;
            Debug.Assert(queuePos < LevelManager.maxFriends, "ERROR: Currently have more friends than allowed!");

            Vector3 friendToPlayer = Vector3.Normalize(player.transform.position - transform.position);
            transform.position = player.transform.position + (0.75f * (queuePos + 1)) * friendToPlayer;

            wasAcquired = true;
        }
    }

    public virtual void FollowPlayer()
    {
        // Adjust each friend's forward vector to achieve a snake-like effect as the queue gets longer
        Vector3 friendToPlayer = Vector3.Normalize(player.transform.position - transform.position);
        Vector3 fFwd = (friendToPlayer.magnitude < 0.01f) ? (new Vector3(0, 0, 0)) : (Vector3.Normalize(friendToPlayer * (1.0f - 0.02f * queuePos)));

        transform.LookAt(player.transform);

        // Ensure the friends are slower than the player
        float fSpeed = (friendToPlayer.magnitude < 0.01f) ? (0.0f) : (0.7f * pMove.speed);

        transform.position = player.transform.position - (transform.localScale.x * (queuePos + 1)) * friendToPlayer + fSpeed * Time.deltaTime * fFwd;
    }

    public virtual void UsePower()
    {

    }

    public void ShuffleQueueForward()
    {
        queuePos--;
        if (queuePos < 1)
        {
            queuePos += lm.currFriends;
        }
    }
}
