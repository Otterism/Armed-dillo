using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMgr : MonoBehaviour
{
    [SerializeField] private float health = 100;


    public void DoDmg(float dmg)
    {
        health -= dmg;

        if (health <= 0)
            Destroy(gameObject);
    }
}
