using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardXRotator : MonoBehaviour
{

    [SerializeField] Rigidbody ballRb;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.right, 100 * Time.deltaTime * ballRb.velocity.magnitude);
    }
}
