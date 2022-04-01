using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform[] destination;

    [SerializeField] private float followRange;
    private bool inRange;

    [SerializeField] private float timeToSpotPlayer;
    private float playerVisibleTimer;
    
    public NavMeshAgent navMeshEnemy;
    private int currentPoint;

    public Light spotlight;
    public float viewDistance;
    public LayerMask viewMask;
    Color originalSpotlightColour;

    float viewAngle;

    public Slider alertImage;
    Slider imageInstance;
    GameObject enemyAlertManager;

    private void Awake()
    {
        enemyAlertManager = GameObject.Find("EnemyAlertCanvas");
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotlight.spotAngle;
        originalSpotlightColour = spotlight.color;

        imageInstance = Instantiate(alertImage, gameObject.transform.position, Quaternion.identity);
        imageInstance.gameObject.transform.SetParent(enemyAlertManager.transform, false);
        imageInstance.gameObject.SetActive(false);
        imageInstance.maxValue = timeToSpotPlayer;
    }

    void Update()
    {
        navMeshEnemy.speed = 3.5f;
        imageInstance.gameObject.SetActive(false);
        BackToPatrol();
        if (CanSeePlayer())
        {
            playerVisibleTimer += Time.deltaTime;
            navMeshEnemy.speed = 0f;
            imageInstance.gameObject.SetActive(true);
            imageInstance.value = playerVisibleTimer;
            spotlight.color = Color.red;
        }
        else
        {
            playerVisibleTimer -= Time.deltaTime;
            spotlight.color = originalSpotlightColour;
        }
        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotlight.color = Color.Lerp(originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer)
		{
            Following();
        }
        
    }

    void BackToPatrol()
    {
        if (navMeshEnemy.remainingDistance < 2.5f)
        {
            navMeshEnemy.destination = destination[currentPoint].position;
            updateCurrentPoint();
        }
    }

    void updateCurrentPoint()
    {
        if(currentPoint == destination.Length - 1)
        {
            currentPoint = 0;
        }
        else
        {
            currentPoint++;
        }
    }

    void Following()
    {
        navMeshEnemy.speed = 3.5f;
        Vector3 moveTo = Vector3.MoveTowards(transform.position, target.position, 100f);
        navMeshEnemy.destination = moveTo;
    }

    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, target.position) < viewDistance)
        {
            Vector3 dirToPlayer = (target.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, target.position, viewMask))
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
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);

    }
}
