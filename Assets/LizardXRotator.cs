using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardXRotator : MonoBehaviour
{
    [SerializeField] Rigidbody ballRb;
    [SerializeField] Basic_WASD_Movement mvmt;

    // Update is called once per frame
    void Update()
    {
        if (mvmt.ballMode && ballRb.velocity.magnitude > 0.75f) transform.Rotate(new Vector3(100 * Time.deltaTime * ballRb.velocity.magnitude, 0, 0), Space.Self);
        //if (mvmt.ballMode) transform.RotateAround(transform.position, transform.right, 100 * Time.deltaTime * ballRb.velocity.magnitude);
    }
}
