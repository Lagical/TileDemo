using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinionSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawners;
    [SerializeField] GameObject[] minions;
    void Start()
    {
    }

    public void spawnHealers()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            Instantiate(minions[0], spawners[i].position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
