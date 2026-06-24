using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardYRotator : MonoBehaviour
{
    [SerializeField] Rigidbody ballRb;
    [SerializeField] Basic_WASD_Movement mvmt;
    [SerializeField] PlayerRotator cameraRot;

    // Update is called once per frame
    void Update()
    {
        if (mvmt.ballMode)
        {
            Vector3 xzVelocity = new Vector3(ballRb.velocity.normalized.x, 0, ballRb.velocity.normalized.z);
            transform.forward = xzVelocity;
        }
        else
        {
            transform.forward = cameraRot.transform.forward;
        }
    }
}
