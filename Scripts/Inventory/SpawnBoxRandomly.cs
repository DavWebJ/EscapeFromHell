using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoxRandomly : MonoBehaviour
{
    public Transform[] spawnPosition;
    public GameObject[] lootPref;

    void Awake()
    {
        
        PositionateLoot();
    }

    public void PositionateLoot()
    {
        for (int i = 0; i < lootPref.Length; i++)
        {
            GameObject go = Instantiate(lootPref[Random.Range(0,lootPref.Length)],spawnPosition[Random.Range(0,spawnPosition.Length)]);
        }
        

    }
    void Update()
    {
        
    }
}
