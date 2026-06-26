using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muzzleflash : MonoBehaviour
{
    [SerializeField] MeshRenderer muzzleFlashRenderer;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject stand;
    [SerializeField] GameObject roll;
    float flashTime = 0f;
    float flashDuration = 0.25f;

    // Update is called once per frame
    void Update()
    {
        if (flashTime + flashDuration < Time.time)
            muzzleFlashRenderer.enabled = false;
    }

    public void Flash()
    {
        flashTime = Time.time;
        muzzleFlashRenderer.enabled = true;

        gun.SetActive(true);
        stand.SetActive(true);
        roll.SetActive(false);
    }
}
