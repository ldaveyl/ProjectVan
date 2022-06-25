using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class GameOverCollision : MonoBehaviour
{
    public UnityEvent onHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
        onHit.Invoke();
        }
    }
}
