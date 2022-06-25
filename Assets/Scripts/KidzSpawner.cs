using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KidzSpawner : MonoBehaviour
{

    public int totalKidz = 0; 
    private int maxKidz = 24;

    public GameObject[] spawners;
    public GameObject kidzPrefab;

    private int respawntime = 10;
    private NavMeshAgent agent;

    void Start() {

        // spawn 10 kidz at the start of the game
        for (int i = 0; i < 20; i++)
        {
        spawnKid();
        }

        // start spawner
        StartCoroutine(spawner());
    }

    // select random position in navmesh
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {

        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;

    }

    void spawnKid()
    {
        // select random spawner
        int numberOfSpawners = spawners.Length;
        int randomSpawnerID = Random.Range(0, numberOfSpawners);
        GameObject spawner = spawners[randomSpawnerID];

        // move agent to navmesh and spawn kidprefab
        agent = kidzPrefab.GetComponent<NavMeshAgent>();
        Vector3 spawnPosition = RandomNavSphere(spawner.transform.position, 2f, -1);
        agent.Warp(spawnPosition);
        GameObject kid = Instantiate(kidzPrefab) as GameObject;
        kid.transform.position = spawnPosition;
        totalKidz += 1;
    }

    IEnumerator spawner()
    {
        while(totalKidz < maxKidz) {
            yield return new WaitForSeconds(respawntime);
            spawnKid();
        }
    }
}