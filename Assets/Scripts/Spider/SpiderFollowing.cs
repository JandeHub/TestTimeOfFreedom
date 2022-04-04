using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFollowing : MonoBehaviour
{
    public Transform target;

    public bool apearing;

    public float spiderSpeed = 1f;
    public float radius;
    public LayerMask layerMask;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        GetComponent<SpiderSpawn>().FollowingSpiders += FollowTarget;
    }
    private void OnDisable()
    {
        GetComponent<SpiderSpawn>().FollowingSpiders -= FollowTarget;
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
            Debug.Log("Collisionando");
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, spiderSpeed * Time.deltaTime);
        }

           
        
    }

    void FollowTarget()
    {
        apearing = true;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}