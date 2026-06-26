using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basic_WASD_Movement : MonoBehaviour
{
    [SerializeField] private float speedCap = 20f;
    [SerializeField] private Image ballIndicator;
    [SerializeField] private Image standIndicator;
    [SerializeField] private Rigidbody ballRb;
    [SerializeField] private GameObject ballVisual;
    [SerializeField] private GameObject standVisual;
    [SerializeField] private GameObject gunVisual;
    [SerializeField] private InAir inAir;   
    [SerializeField] private Image crosshair1;   
    [SerializeField] private Image crosshair2;   

    public bool ballMode = false;
    float ballMinV = 0.5f;
    float acceleration = 5f;
    float slerpStrength = 0.01f;
    public Vector3 WorldMove = Vector3.zero;
    public Vector3 LocalMove = Vector3.zero;

    public void SetBallMode(bool val)
    {
        string a = this.gameObject.name;
        ballMode = val;
        ballIndicator.enabled = val;
        standIndicator.enabled = !val;
        ballRb.constraints = (val) ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeRotation;
        if (!val) ballRb.rotation = Quaternion.identity;
        ballVisual.SetActive(val);
        standVisual.SetActive(!val);
        gunVisual.SetActive(!val);
        crosshair1.enabled = !val;
        crosshair2.enabled = !val;
    }

    // Update is called once per frame
    void Update()
    {
        if (ballMode) BallMovement();
        else if (!inAir.inAir) ApplyFriction();

        if (Input.GetKeyDown(KeyCode.LeftShift)) SetBallMode(!ballMode);
        if (Input.GetKeyDown(KeyCode.Space) && !inAir.inAir) ballRb.velocity = new Vector3(ballRb.velocity.x, 10, ballRb.velocity.y);

        if (ballRb.velocity.y < 0 && ballRb.velocity.y > -2) ballRb.AddForce(12f * Vector3.down);
    }

    void BallMovement()
    {
        //if (ballRb.velocity.magnitude < ballMinV && (Time.time - timeOfBallModeActivate > minTimeOfBallMode))
        //{
        //    SetBallMode(!ballMode);
        //    return;
        //}


        Transform target = GetComponent<FollowTarget>().target;

        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");
        WorldMove = transform.right * xMov + transform.forward * zMov;
        LocalMove = new Vector3(xMov, 0, zMov);
        Vector3 xzVelocity = new Vector3(ballRb.velocity.x, 0, ballRb.velocity.z);


        ///////////////////////////////////// REDUCE SPEED IF OVER MAXIMUM VELOCITY ///////////////////////////
        // if the user is trying to move and current velocity > maximum velocity:
        // prevent them from speeding up, but allow them to direct and counteract their currently high velocity
        // This is only run whilst on the ground
        if (WorldMove != Vector3.zero && xzVelocity.magnitude > speedCap)
        {
            float strength = .75f;

            float A = Vector3.SignedAngle(xzVelocity * -1, WorldMove, new Vector3(1, 0, 1));
            float aRadian = Mathf.Abs(A / 180);

            // reduce velocity in current direction according to how much it counteracts the current velocity, AND according to how far over maxV you are moving
            ballRb.AddForce(
                xzVelocity.normalized
                * (-1 * WorldMove.magnitude * acceleration * aRadian)
                * (1 + (xzVelocity.magnitude - speedCap) * Time.fixedDeltaTime)
                * strength);
        }




        // add movement
        var dot = Vector3.Dot(xzVelocity.normalized, WorldMove.normalized);
        float multiplier = (dot < 0) ? 2 : 1;  // accelerate more if opposing current velocity
        target.GetComponent<Rigidbody>().AddForce(WorldMove * multiplier * acceleration);

        // slerp to redirect
        if (WorldMove.magnitude == 0) return; 

        float adjustedSlerpStrength = (xzVelocity.magnitude > speedCap) ? 0.03f : slerpStrength;
        ballRb.velocity = Vector3.Slerp(xzVelocity.normalized, WorldMove.normalized, adjustedSlerpStrength * (1 - Mathf.Abs(dot)))
            * xzVelocity.magnitude
            + new Vector3(0, ballRb.velocity.y, 0); 
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
        ballRb.AddForce(-0.5f * y * frictionDir);
    }
}
