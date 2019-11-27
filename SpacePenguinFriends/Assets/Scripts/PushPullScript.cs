using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullScript : MonoBehaviour
{
    public float moveDistance = 1;
    public float playerOffsetDistance = 2.5f;
    public float moveSpeed = 3.0f;

    private GameObject player;

    // raycast hit used for ground detection
    private RaycastHit hit;

    // ray used for ground detection
    private Ray ray;

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
            bool valid = false;

            ray = new Ray(transform.position, -transform.right);
            if (Physics.Raycast(ray, out hit, 1.0f))
            {
                if (!(hit.collider.isTrigger))
                {
                    if(hit.collider.gameObject == collision.gameObject)
                    {
                        valid = true;
                    }
                }

            }

            ray = new Ray(transform.position, transform.right);
            if (Physics.Raycast(ray, out hit, 1.0f))
            {
                if (!(hit.collider.isTrigger))
                {
                    if (hit.collider.gameObject == collision.gameObject)
                    {
                        valid = true;
                    }
                }

            }

            ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit, 1.0f))
            {
                if (!(hit.collider.isTrigger))
                {
                    if (hit.collider.gameObject == collision.gameObject)
                    {
                        valid = true;
                    }
                }

            }

            ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit, 1.0f))
            {
                if (!(hit.collider.isTrigger))
                {
                    if (hit.collider.gameObject == collision.gameObject)
                    {
                        valid = true;
                    }
                }

            }

            if(valid)
                player.GetComponent<PlayerPushPull>().NotifyBlockCollision(this.gameObject);
        }
    }
}
