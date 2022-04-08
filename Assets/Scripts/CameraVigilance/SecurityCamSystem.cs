using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SecurityCamSystem : MonoBehaviour
{
    public static Transform target;
    [SerializeField] private Transform securityCamera;
    public Transform explosionParticle;

    [SerializeField] private float timeToSpotPlayer;
    private float playerVisibleTimer;
    private float timer = 0f;
    public float maxTimer = 3f;

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
        timer = maxTimer;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotlight.spotAngle;
        originalSpotlightColour = spotlight.color;

        Instantiate(explosionParticle, securityCamera.position, Quaternion.Euler(0, 0, -110), securityCamera);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            explosionParticle.gameObject.SetActive(false);
            if (CanSeePlayer())
            {
                isDetected = true;
                playerVisibleTimer += Time.deltaTime;
                spotlight.color = Color.red;
            }
            else
            {
                isDetected = false;
                timer = maxTimer;
                playerVisibleTimer -= Time.deltaTime;
                spotlight.color = originalSpotlightColour;
            }
            playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
            spotlight.color = Color.Lerp(originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);

        
            if (playerVisibleTimer >= timeToSpotPlayer)
            {
                StartCoroutine("DestroyCountdown");
            }
        }

        else
        {
            spotlight.enabled = false;
            explosionParticle.gameObject.SetActive(true);
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

    IEnumerator DestroyCountdown()
    {

        if(timer > 0)
        { 
            timer -= Time.deltaTime;
            Debug.Log(timer);
        }
        SpawningSpiders();


        yield return new WaitForSeconds(1f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(securityCamera.position, securityCamera.forward * viewDistance);

    }
}
