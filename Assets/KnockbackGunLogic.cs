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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && ammoMgr.ammo > 0)
        {
            if (!transform.root.GetComponent<Basic_WASD_Movement>().ballMode)
                Shoot(false);
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

    private void Shoot(bool reversed)
    {
        ammoMgr.ReduceAmmo(1);
        Instantiate(shootSFX, transform.position, Quaternion.identity);

        float adjustedShootStrength = (!reversed) ? -shootStrength : shootStrength;
        followTarget.target.GetComponent<Rigidbody>().velocity = transform.forward * adjustedShootStrength;

        ballRb.rotation = (!reversed) ? transform.rotation : transform.rotation * Quaternion.AngleAxis(180, Vector3.up);
        muzzleFlash.Flash();
        //ballRb.rotation = (!reversed) ? transform.rotation : Quaternion.Inverse(transform.rotation);
        //if (reversed) ballRb.rotation = Quaternion.Inverse(ballRb.rotation);
        //ballRb.rotation.SetLookRotation(!reversed ? transform.forward : transform.forward * -1);

        GunRayCast();

        transform.root.GetComponent<Basic_WASD_Movement>().SetBallMode(true);
    }

    private void GunRayCast()
    {
        //Vector3 roughtMuzzleLocation = transform.position + transform.right * 0.5f + transform.up;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.GetComponent<HealthMgr>())
                hit.transform.GetComponent<HealthMgr>().DoDmg(100);
        }
    }
}
