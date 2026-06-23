using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    private float xRotation;

    [Tooltip("Sensitivity of the rotation, relative to cursor movement input")]
    [SerializeField]
    private float sensitivity = 0.3f;


    [Header("References")]
    [Tooltip("The parent object, that rotates around the y-axis")]
    [SerializeField]
    private Transform horizontalRotator;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        CalculateInputsAndRotate();
    }

    private void CalculateInputsAndRotate()
    {
        float xRot = Input.GetAxisRaw("Mouse Y");
        float yRot = Input.GetAxisRaw("Mouse X");

        xRotation -= xRot;

        horizontalRotator.Rotate(Vector3.up * yRot * sensitivity);

        transform.localRotation = Quaternion.Euler(xRotation * sensitivity, 0, 0);
    }
}
