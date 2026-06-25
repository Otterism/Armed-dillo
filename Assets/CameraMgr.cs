using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    [SerializeField] private Transform target;


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(target.position, transform.position - target.position, out hit, 10))
        {
            if (hit.transform == null) transform.position = (transform.position - target.position).normalized * 10;
            else transform.position = hit.point;
        }
    }
}
