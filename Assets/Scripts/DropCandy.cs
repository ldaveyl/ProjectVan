using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCandy : MonoBehaviour
{
    public GameObject[] candyPrefabs;
    public GameObject kidzTrigger;
    public AudioSource sourceDropCandy;

    private int _candyid;

    // select random position in navmesh
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {

        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;

    }

    void spawnCandy()
    {
        _candyid = Random.Range(0, 3);
        GameObject candy = Instantiate(candyPrefabs[_candyid]);
        candy.transform.position = this.transform.position;
    }

    void Update()   
    {
        if (Input.GetKeyDown(KeyCode.X) && kidzTrigger.GetComponent<VanCollisions>().candyAmount > 0)
        {
            spawnCandy();
            sourceDropCandy.Play();
            kidzTrigger.GetComponent<VanCollisions>().candyAmount -= 1;
        }
    }
}
