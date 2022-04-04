using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SecurityCamSystem : MonoBehaviour
{
    public static Transform target;
    [SerializeField] private Transform securityCamera;

    [SerializeField] private float timeToSpotPlayer;
    private float playerVisibleTimer;

    public Light spotlight;
    public float viewDistance;
    public LayerMask viewMask;
    Color originalSpotlightColour;

    public static bool isDetected;

    public event Action SpawningSpiders = delegate { };

    float viewAngle;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotlight.spotAngle;
        originalSpotlightColour = spotlight.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer())
        {
            isDetected = true;
            playerVisibleTimer += Time.deltaTime;
            spotlight.color = Color.red;
        }
        else
        {
            isDetected = false;
            playerVisibleTimer -= Time.deltaTime;
            spotlight.color = originalSpotlightColour;
        }
        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotlight.color = Color.Lerp(originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);


        if (playerVisibleTimer >= timeToSpotPlayer)
        {
            SpawningSpiders();
        }
    }

    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, target.position) < viewDistance)
        {
            Vector3 dirToPlayer = (target.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(securityCamera.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(securityCamera.position, target.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(securityCamera.position, securityCamera.forward * viewDistance);

    }
}
