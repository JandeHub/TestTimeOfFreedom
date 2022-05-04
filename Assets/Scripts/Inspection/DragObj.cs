using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObj : MonoBehaviour
{
    private Vector3 lastPosition;
    private Vector3 currentPosition;
    private float rotationSpeed = -0.2f;

    void Start()
    {
        lastPosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            currentPosition = Input.mousePosition;
            Vector3 offset = currentPosition - lastPosition;
            transform.RotateAround(transform.position, Vector3.up, offset.x * rotationSpeed);
            lastPosition = currentPosition;
        }
        lastPosition = Input.mousePosition;
    }
}
