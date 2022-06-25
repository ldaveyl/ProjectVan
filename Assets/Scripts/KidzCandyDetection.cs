using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidzCandyDetection : MonoBehaviour
{
    public KidzNavigation Navigation;
    public Animator animator;

    private void OnTriggerStay(Collider col)
    {

        // If candy is detected run to it, and when you touch it, destroy it
        if (col.gameObject.tag == "candyDetect")
        {
            Navigation.detectCandy = true;
            Navigation.candyPosition = col.transform.position;
        }
        else if (col.gameObject.tag == "candyTouch")
        {
            Destroy(col.transform.parent.gameObject);
            Navigation.detectCandy = false; // start wandering again
        }
    }
}
