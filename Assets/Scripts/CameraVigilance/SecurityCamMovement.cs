using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamMovement : MonoBehaviour
{
    public float defaultPitch;
    public float Angle;
    public float speed;

    float currentAngles;
    bool sweep = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SecurityCamSystem.isDetected)
        {
            
        }
        else
        {
            currentAngles += speed * Time.deltaTime * (sweep ? 1f : -1f);
            if (Mathf.Abs(currentAngles) >= (Angle * 0.5f))
            {
                sweep = !sweep;
            }

            transform.localEulerAngles = new Vector3(0f, currentAngles, defaultPitch);
        }
    }
}
