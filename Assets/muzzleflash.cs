using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muzzleflash : MonoBehaviour
{
    [SerializeField] MeshRenderer mRenderer;
    float flashTime = 0f;
    float flashDuration = 0.25f;

    // Update is called once per frame
    void Update()
    {
        if (flashTime + flashDuration < Time.time)
            mRenderer.enabled = false;
    }

    public void Flash()
    {
        flashTime = Time.time;
        mRenderer.enabled = true;
    }
}
