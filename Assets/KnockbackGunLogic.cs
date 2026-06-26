using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnockbackGunLogic : MonoBehaviour
{
    float shootStrength = 50;
    [SerializeField] private Rigidbody ballRb;
    [SerializeField] private muzzleflash muzzleFlash;

    [Header("References")]
    [SerializeField] private GameObject shootSFX;
    [SerializeField] private AmmoMgr ammoMgr;
    [SerializeField] private FollowTarget followTarget;
    [SerializeField] private Basic_WASD_Movement mvmt;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && ammoMgr.ammo > 0)
        {
            Shoot();
            //if (!transform.root.GetComponent<Basic_WASD_Movement>().ballMode)
            //    Shoot(false);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Camera.main.fieldOfView = 40;
            transform.root.GetComponent<Basic_WASD_Movement>().SetBallMode(false);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Camera.main.fieldOfView = 60;
            transform.root.GetComponent<Basic_WASD_Movement>().SetBallMode(true);
        }
    }

    private void Shoot()
    {
        ammoMgr.ReduceAmmo(1);
        Instantiate(shootSFX, transform.position, Quaternion.identity);

        Vector3 moveDir = (!mvmt.ballMode) ? transform.forward * -1 : mvmt.Move.normalized;
        float adjustedShootStrength = (!mvmt.ballMode) ? shootStrength : shootStrength * 0.75f;

        //Vector3 flashDir = (!mvmt.ballMode) ? Vector3.forward : mvmt.Move.normalized;

        Vector3 rayOrigin = (!mvmt.ballMode) ?  Camera.main.transform.position  : transform.position;
        Vector3 rayDir = (!mvmt.ballMode) ?     Camera.main.transform.forward   : mvmt.Move.normalized;

        GunRayCast(rayOrigin, rayDir);
        followTarget.target.GetComponent<Rigidbody>().velocity = moveDir * adjustedShootStrength;

        ballRb.rotation = transform.rotation;
        muzzleFlash.Flash();

        transform.root.GetComponent<Basic_WASD_Movement>().SetBallMode(true);
    }

    private void GunRayCast(Vector3 origin, Vector3 direction)
    {
        //Vector3 roughtMuzzleLocation = transform.position + transform.right * 0.5f + transform.up;
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, Mathf.Infinity))
        {
            if (hit.transform.GetComponent<HealthMgr>())
                hit.transform.GetComponent<HealthMgr>().DoDmg(100);
        }
    }
}
