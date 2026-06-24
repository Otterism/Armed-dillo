using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basic_WASD_Movement : MonoBehaviour
{
    [SerializeField] private float speedCap = 15f;
    [SerializeField] private Image ballIndicator;
    [SerializeField] private Rigidbody ballRb;
    bool ballMode = false;
    float ballMinV = 0.5f;
    float acceleration = 3f;

    float timeOfBallModeActivate = 0f;
    float minTimeOfBallMode = 1f;

    void SetBallMode(bool val)
    {
        string a = this.gameObject.name;
        ballMode = val;
        ballIndicator.enabled = val;
        ballRb.constraints = (val) ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeRotation;
        if (!val) ballRb.rotation = Quaternion.identity;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ballMode) BallMovement();
        else ApplyFriction();

        if (Input.GetKeyDown(KeyCode.LeftShift)) SetBallMode(!ballMode);
    }

    void BallMovement()
    {
        if (ballRb.velocity.magnitude < ballMinV && (Time.time - timeOfBallModeActivate > minTimeOfBallMode))
        {
            SetBallMode(!ballMode);
            return;
        }

        Transform target = GetComponent<FollowTarget>().target;

        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * xMov + transform.forward * zMov;
        Vector3 xzVelocity = new Vector3(ballRb.velocity.x, 0, ballRb.velocity.z);


        ///////////////////////////////////// REDUCE SPEED IF OVER MAXIMUM VELOCITY ///////////////////////////
        // if the user is trying to move and current velocity > maximum velocity:
        // prevent them from speeding up, but allow them to direct and counteract their currently high velocity
        // This is only run whilst on the ground
        if (move != Vector3.zero && xzVelocity.magnitude > speedCap)
        {
            float strength = 1f;

            float A = Vector3.SignedAngle(xzVelocity * -1, move, new Vector3(1, 0, 1));
            float aRadian = Mathf.Abs(A / 180);

            // reduce velocity in current direction according to how much it counteracts the current velocity, AND according to how far over maxV you are moving
            ballRb.AddForce(
                xzVelocity.normalized
                * (-1 * move.magnitude * acceleration * aRadian)
                * (1 + (xzVelocity.magnitude - speedCap) * Time.fixedDeltaTime)
                * strength);
        }




        // add movement
        var dot = Vector3.Dot(xzVelocity.normalized, move.normalized);
        float multiplier = (dot < 0) ? 2 : 1;  // accelerate more if opposing current velocity
        target.GetComponent<Rigidbody>().AddForce(move * multiplier * acceleration);
    }

    void ApplyFriction()
    {
        if (ballRb.velocity.magnitude < 10) return;

        bool grounded = true;
        float m = (grounded) ? 0.2f : 0.2f;
        float c = (grounded) ? 1f : 0.2f;
        float y = m * Mathf.Pow(ballRb.velocity.magnitude, 2) + c;  // y= mx^2 + C


        // remove downward component from friction (otherwise falling feels floaty)
        Vector3 frictionDir = new Vector3(ballRb.velocity.normalized.x, 0, ballRb.velocity.normalized.z);
        ballRb.AddForce(-1 * y * frictionDir);
    }
}
