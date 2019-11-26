using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullScript : MonoBehaviour
{
    public float moveDistance = 1;
    public float playerOffsetDistance = 2.5f;
    public float moveSpeed = 3.0f;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetPushDistance()
    {
        return moveDistance;
    }

    public float GetMoveSpeed()
    {
        return moveDistance;
    }

    public float GetPlayerOffsetDistance()
    {
        return playerOffsetDistance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != player)
        {
            player.GetComponent<PlayerPushPull>().NotifyBlockCollision(this.gameObject);
        }
    }
}
