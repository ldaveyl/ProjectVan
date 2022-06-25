using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRollBar: MonoBehaviour

{
    Rigidbody Truck;

    // Start is called before the first frame update
    void Start()
    {
        Truck = GetComponent<Rigidbody>();
    }

    public WheelCollider WheelL;
    public WheelCollider WheelR;
    public float AntiRoll = 5000;

    void LateUpdate()
    {
        WheelHit hit;
        float travelL = 1;
        float travelR = 1;

        bool groundedL = WheelL.GetGroundHit(out hit);
        if (groundedL)
            travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance;

        bool groundedR = WheelR.GetGroundHit(out hit);
        if (groundedR)
            travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) / WheelR.suspensionDistance;

        float antiRollForce = (travelL - travelR) * AntiRoll;

        if (groundedL)
            Truck.AddForceAtPosition(WheelL.transform.up * -antiRollForce,
                   WheelL.transform.position);
        if (groundedR)
            Truck.AddForceAtPosition(WheelR.transform.up * antiRollForce,
                   WheelR.transform.position);
    }
    // Update is called once per frame
    //void LateUpdate()
    //{
    //    Truck.MoveRotation(Quaternion.LookRotation(transform.forward, Vector3.up));
    //    //transform.LookAt(transform.forward + transform.position, Vector3.up);
    //}
}
