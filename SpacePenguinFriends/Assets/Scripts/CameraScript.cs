using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private GameObject player;

    private float currentRotationXZ;

    public float pitchAngle;

    public float cameraDistance;

    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        currentRotationXZ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position - ( cameraDistance * new Vector3(0, 0, 1));

        transform.LookAt(player.transform.position);

        if (Input.GetAxis("RightStickX") > .3f || Input.GetAxis("RightStickX") < -.3f)
            currentRotationXZ += rotationSpeed * Time.deltaTime * Input.GetAxis("RightStickX");
        else if (Input.GetAxis("Mouse X") > .3f || Input.GetAxis("Mouse X") < -.3f)
        {
            float mouseAxis = Input.GetAxis("Mouse X");
            if (mouseAxis > 2.0f)
                mouseAxis = 2.0f;

            if (mouseAxis < -2.0f)
                mouseAxis = -2.0f;

            currentRotationXZ += rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X");
        }

        transform.RotateAround(player.transform.position, new Vector3(0, 1, 0), currentRotationXZ);

        transform.RotateAround(player.transform.position, transform.right, pitchAngle);

        transform.LookAt(player.transform.position);
    }
}
