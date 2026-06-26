using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoMgr : MonoBehaviour
{
    public int ammo { get; private set; } = 1;

    bool isReloading;
    float reloadStartTime;
    [SerializeField] float reloadDuration = 1.5f;
    [SerializeField] Image ammoDisplay;


    void Update()
    {
        if (ammo <= 0 && !isReloading)
            StartReload();
        
        if (Time.time > reloadStartTime + reloadDuration)
        {
            ammo = 1;
            isReloading = false;
        }

        ammoDisplay.enabled = ammo > 0;
    }

    public void StartReload()
    {
        reloadStartTime = Time.time;
        isReloading = true;
    }

    public void ReduceAmmo(int _ammo)
    {
        ammo -= _ammo;
    }
}
