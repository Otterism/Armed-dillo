using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackGunLogic : MonoBehaviour
{
    float shootStrength = 30;
    [SerializeField] private Rigidbody ballRb;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot(false);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Shoot(true);
        }
    }

    private void Shoot(bool reversed)
    {
        float adjustedShootStrength = (!reversed) ? -shootStrength : shootStrength;
        GetComponent<FollowTarget>().target.GetComponent<Rigidbody>().velocity = transform.forward * adjustedShootStrength;

        ballRb.rotation = (!reversed) ? transform.rotation : transform.rotation * Quaternion.AngleAxis(180, Vector3.up);
        //ballRb.rotation = (!reversed) ? transform.rotation : Quaternion.Inverse(transform.rotation);
        //if (reversed) ballRb.rotation = Quaternion.Inverse(ballRb.rotation);
        //ballRb.rotation.SetLookRotation(!reversed ? transform.forward : transform.forward * -1);
    }
}
