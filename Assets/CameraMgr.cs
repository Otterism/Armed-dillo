using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CameraMgr : MonoBehaviour
{
    [SerializeField] private Transform target;
    Vector3 defaultPos;
    bool myBool = false;

    private void Start()
    {
        defaultPos = transform.localPosition;
    }


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(target.position, transform.position - target.position, out hit, 10, LayersToLayerMask(new int[] { 0, 6 })) || myBool)
        {
            if (hit.transform == null) transform.localPosition = defaultPos;
            else
            {
                transform.position = hit.point;
                print($"{Time.time} ADJUSTED");
                myBool = true;
            }
        }
        else transform.localPosition = defaultPos;
    }


    public int LayersToLayerMask(int[] layers)
    {
        int layerMask = 0;
        foreach (int layer in layers)
            layerMask = layerMask | 1 << (int)layer;

        return layerMask;
    }
}
