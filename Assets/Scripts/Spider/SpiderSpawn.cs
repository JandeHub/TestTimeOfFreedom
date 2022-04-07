using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpiderSpawn : MonoBehaviour
{
    public GameObject spiderPrefab;
    public Transform camerasecurity;

    public Transform spiderParents;
    public int numSpiders;
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
        
    }

    void Spawn()
    {

        SpawnS();

    }

    void SpawnS()
    {
        for (int i = 0; i < numSpiders; i++)
        {
            if (numSpiders > i)
            {
                Instantiate(spiderPrefab, camerasecurity.position, transform.rotation, spiderParents);
            }

        }
    }
}
