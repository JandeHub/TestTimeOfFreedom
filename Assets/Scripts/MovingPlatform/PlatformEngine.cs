using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEngine : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform[] platPositions;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private float distance;

    private float startWaitTime;
    private int platformToMove;

    public bool palanca;

    // Start is called before the first frame update
    void Start()
    {
        startWaitTime = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, platPositions[platformToMove].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, platPositions[platformToMove].position) < distance)
        {
            if (waitTime <= 0)
            {
                waitTime = startWaitTime;

                if (platformToMove == 0)
                {
                    if (palanca)
                    {
                        platformToMove = 1;
                    }
                    else
                    {
                        platformToMove = 2;
                    }
                }
                else
                {
                    platformToMove = 0;
                }
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
