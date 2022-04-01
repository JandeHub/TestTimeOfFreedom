using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSeePlayer : MonoBehaviour
{
    /*bool CanSeePlayer()
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
    }*/
}
