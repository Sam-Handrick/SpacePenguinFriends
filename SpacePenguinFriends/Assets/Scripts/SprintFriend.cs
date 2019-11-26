using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintFriend : FriendScript
{
    public float boostLength; // In seconds
    public float speedBoost; // Multiplier
    private float tBoosted;
    private bool isBoosted;

    // Start is called before the first frame update
    protected override void Start()
    {
        // Be sure to call this for all friends!
        base.Start();

        // Additional setup for this friend
        tBoosted = 0.0f;
        isBoosted = false;

        Debug.Assert(speedBoost != 0.0f, "ERROR: Speed boost must be nonzero!");
    }

    // Update is called once per frame
    protected override void Update()
    {
        // We need this block in each specific friend's update since each could have a different FollowPlayer() implementation
        if (wasAcquired)
        {
            FollowPlayer();
        }

        // Additional updates for this friend
        if (isBoosted)
        {
            tBoosted += Time.deltaTime;
            isBoosted = tBoosted < boostLength;
        }
        // Reduce speed to normal, turn boost off, and reset timer
        if (tBoosted >= boostLength)
        {
            pMove.speed /= speedBoost;
            isBoosted = false;
            tBoosted = 0.0f;
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        // Make sure this is called for all friends
        base.OnCollisionEnter(collision);
    }

    public override void FollowPlayer()
    {
        // Make sure this is called for all friends
        base.FollowPlayer();
    }

    // Example of a temporary speed-boosting friend as proof-of-concept
    public override void UsePower()
    {
        Debug.Assert(queuePos == 1, "ERROR: Trying to use power of a friend that's not at the front of the queue!");
        if (!isBoosted)
        {
            // Apply effect
            pMove.speed *= speedBoost;
            isBoosted = true;
        }

        // Make sure this is called *at the end of this function* for all friends
        base.UsePower();
    }
}
