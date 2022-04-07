using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFollowing : MonoBehaviour
{
    public Transform target;

    public float spiderJumpForce;
    public float spiderSpeed = 1f;
    public float radius;
    public LayerMask layerMask;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.CheckSphere(transform.position, radius, layerMask))
        {
            _rb.AddForce(Vector3.up * spiderJumpForce, ForceMode.VelocityChange);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, spiderSpeed * Time.deltaTime);
        }

           
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
