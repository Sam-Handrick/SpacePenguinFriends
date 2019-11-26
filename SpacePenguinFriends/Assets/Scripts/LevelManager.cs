using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public const int maxFriends = 5;
    public int currFriends;
    public FriendScript[] friendArr;

    private int frontIdx;

    // Start is called before the first frame update
    void Start()
    {
        friendArr = new FriendScript[maxFriends];
        currFriends = 0;
        frontIdx = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public FriendScript GetFrontFriend()
    {
        return friendArr[frontIdx];
    }

    public int GetFrontIndex()
    {
        return frontIdx;
    }

    public void IncrementFrontIndex()
    {
        frontIdx++;
        frontIdx = frontIdx % currFriends;
    }
}
