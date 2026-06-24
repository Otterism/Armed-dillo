using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardYRotator : MonoBehaviour
{
    [SerializeField] Rigidbody ballRb;

    // Update is called once per frame
    void Update()
    {

        Vector3 xzVelocity = new Vector3(ballRb.velocity.normalized.x, 0, ballRb.velocity.normalized.z);
        transform.forward = xzVelocity; 
    }
}
