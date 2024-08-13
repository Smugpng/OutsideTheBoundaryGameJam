using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ThreatSpawn : MonoBehaviour
{
    public bool check = false;
    public GameObject[] enemyPrefabs;
    public Transform[] spawnLocations;
    private int spawnChance = 0;

    private void Start()
    {
        SpawnCheck();
    }

    void SpawnCheck()
    {
        spawnChance += PlayerPrefs.GetInt("ThreatMultiplier");
        int randomNum = Random.Range(1, 10);
        if (randomNum <= spawnChance)
        {
            Invoke("Spawn", 30f);
        }
        else
        {
        }
    }
    public void Spawn()
    {
        GameObject Enemies = new GameObject("enemies");
        for(int i = 0; i < spawnLocations.Length; i++) 
        {
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);
            enemy.transform.parent = Enemies.transform;
            enemy.transform.position = spawnLocations[i].position;
        }
    }
    private void Update()
    {
        if (check)
        {
            check = false;
            Spawn();
        }
    }


}
