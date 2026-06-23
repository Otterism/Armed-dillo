using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackGunLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GetComponent<FollowTarget>().target.GetComponent<Rigidbody>().velocity = transform.forward * -20;
    }
}
