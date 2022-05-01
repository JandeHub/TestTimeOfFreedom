using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObjects : MonoBehaviour
{
    [SerializeField] private float pickUpRange;
    [SerializeField] private float moveForce;
    [SerializeField] private Transform holdObjectParent;

    private GameObject heldObject;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
          
            if(heldObject == null)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    PickupObject(hit.transform.gameObject);
                }
   
            }
            else
            {
                DropObject();
            }

            
        }

        if (heldObject != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if(Vector3.Distance(heldObject.transform.position, holdObjectParent.position) > 0.1f)
        {
            Vector3 moveDirection = (holdObjectParent.position - heldObject.transform.position);
            heldObject.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }

    void PickupObject(GameObject pickObject)
    {
        if(pickObject.GetComponent<Rigidbody>())
        {
            Rigidbody objectRigidbody = pickObject.GetComponent<Rigidbody>();
            objectRigidbody.useGravity = false;
            objectRigidbody.drag = 10;

            objectRigidbody.transform.parent = holdObjectParent;
            heldObject = pickObject;
        }
    }

    void DropObject()
    {
        Rigidbody objectRigidbody = heldObject.GetComponent<Rigidbody>();
        objectRigidbody.useGravity = true;
        objectRigidbody.drag = 1;

        heldObject.transform.parent = null;
        heldObject = null;
    }

   
}
