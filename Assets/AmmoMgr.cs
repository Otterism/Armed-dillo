using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMgr : MonoBehaviour
{
    public int ammo { get; private set; } = 1;

    bool isReloading;
    float reloadStartTime;
    [SerializeField] float reloadDuration = 2;


    void Update()
    {
        if (ammo <= 0 && !isReloading)
            StartReload();
        
        if (Time.time > reloadStartTime + reloadDuration)
            ammo = 1;
    }

    public void StartReload()
    {
        reloadStartTime = Time.deltaTime;
    }

    public void ReduceAmmo(int _ammo)
    {
        ammo -= _ammo;
    }
}
