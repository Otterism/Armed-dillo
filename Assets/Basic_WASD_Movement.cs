using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basic_WASD_Movement : MonoBehaviour
{
    [SerializeField] private float speedCap = 15f;
    [SerializeField] private Image ballIndicator;
    bool ballMode = false;

    void SetMyBallMode(bool val)
    {
        ballMode = val;
        ballIndicator.enabled = val;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ballMode) BallMovement();
        if (Input.GetKeyDown(KeyCode.LeftShift)) SetMyBallMode(!ballMode);
    }

    void BallMovement()
    {
        Transform target = GetComponent<FollowTarget>().target;

        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        target.GetComponent<Rigidbody>().AddForce(transform.right * xMov + transform.forward * zMov);

        if (target.GetComponent<Rigidbody>().velocity.magnitude > speedCap)
            target.GetComponent<Rigidbody>().velocity = target.GetComponent<Rigidbody>().velocity.normalized * speedCap;
    }
}
