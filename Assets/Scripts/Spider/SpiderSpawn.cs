using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpiderSpawn : MonoBehaviour
{
    public GameObject spiderPrefab;
    public Transform camerasecurity;

    public Transform spiderParents;
    private bool Spoted;

    public event Action FollowingSpiders = delegate { };
    private void OnEnable()
    {
        GetComponent<SecurityCamSystem>().SpawningSpiders += Spawn;
    }
    private void OnDisable()
    {
        GetComponent<SecurityCamSystem>().SpawningSpiders -= Spawn;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Spoted)
        {
            Instantiate(spiderPrefab, camerasecurity.position, transform.rotation, spiderParents);
            FollowingSpiders();
        }
    }

    void Spawn()
    {
        Spoted = !Spoted;
    }
}
