using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 Offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translationSpeed;
    [SerializeField] private float rotationSpeed;



    private void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }

    public void HandleTranslation()
    {
        Vector3 targetPosition = target.TransformPoint(Offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translationSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    IEnumerator returnCamera() {
        Offset.y = 2.87f;
        Offset.z = -5.66f;
        yield return new WaitForSeconds(.01f); 
        translationSpeed = 10;
        rotationSpeed = 5;
    }

    void Update()   
    {
        // Turn camera to back
        if (Input.GetKeyDown(KeyCode.C))
        {
            Offset.y = 4.26f;
            Offset.z = 5.66f;
            translationSpeed = 100;
            rotationSpeed = 100;
        }
        // return to front
        if (Input.GetKeyUp(KeyCode.C))
        {
            StartCoroutine(returnCamera());
        }
    }
}
