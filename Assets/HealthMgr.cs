using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMgr : MonoBehaviour
{
    [SerializeField] private float health = 100;



    public void DoDmg(float dmg)
    {
        health -= dmg;

        if (health > 0) return;

        Destroy(gameObject);

        Object.FindAnyObjectByType<PointsMgr>().AddPoints(12 + (int)Object.FindAnyObjectByType<InAir>().gameObject.GetComponent<Rigidbody>().velocity.magnitude + (int)Object.FindAnyObjectByType<InAir>().transform.position.y);
    }
}
