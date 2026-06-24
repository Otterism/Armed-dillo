using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAir : MonoBehaviour
{
    public bool inAir = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == 6) inAir = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer == 6) inAir = false;
    }
}
