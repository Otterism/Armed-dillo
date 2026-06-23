using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_WASD_Movement : MonoBehaviour
{
    [SerializeField] private float speedCap = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform target = GetComponent<FollowTarget>().target;

        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        target.GetComponent<Rigidbody>().AddForce(transform.right * xMov + transform.forward * zMov);

        if (target.GetComponent<Rigidbody>().velocity.magnitude > speedCap)
            target.GetComponent<Rigidbody>().velocity = target.GetComponent<Rigidbody>().velocity.normalized * speedCap;
    }
}
