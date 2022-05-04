using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObjects : MonoBehaviour
{
    [SerializeField] private float pickUpRange;
    [SerializeField] private float moveForce;
    [SerializeField] private Transform holdObjectParent;
    public float throwSpeed;

    private GameObject heldObject;
    private Camera camera;

    private void OnEnable()
    {
        GameObject.FindWithTag("Player").GetComponent<InputSystemKeyboard>().OnPick += SetPick;
        GameObject.FindWithTag("Player").GetComponent<InputSystemKeyboard>().OnThrow += SetThrow;
    }

    private void OnDisable()
    {
        GameObject.FindWithTag("Player").GetComponent<InputSystemKeyboard>().OnPick -= SetPick;
        GameObject.FindWithTag("Player").GetComponent<InputSystemKeyboard>().OnThrow -= SetThrow;
    }
    private void Awake()
    {
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        GetComponent<PickupObjects>().enabled = false;
    }

    void FixedUpdate()
    {
        
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

    void ThrowObject()
    {

        Vector3 direction = camera.ScreenPointToRay(Input.mousePosition).direction;
        heldObject.GetComponent<Rigidbody>().AddForce(direction * throwSpeed);
        
        DropObject();

    }

    void SetPick(bool pick)
    {
        if (pick)
        {

            if (heldObject == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    PickupObject(hit.transform.gameObject);
                }

            }
            else
            {
                DropObject();
            }

        }
    }

    void SetThrow()
    {
        ThrowObject();
    }

   
}
