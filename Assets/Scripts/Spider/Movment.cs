using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    public float speed = 1f;
    

    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rb.velocity.magnitude < speed)
        {
            float value = Input.GetAxis("Vertical");
            if (value != 0)
            {
                _rb.AddForce(0, 0, value * Time.deltaTime * speed);
            }
        }
    }
}
