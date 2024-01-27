using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera billboardCamera;

    void Start()
    {
        billboardCamera = Camera.main;
    }

    void Update()
    {
        if (billboardCamera == null)
            return;

        /*Vector3 flatDirection = (billboardCamera.transform.position - transform.position).normalized;
        flatDirection.y = 0f;

        transform.rotation = Quaternion.LookRotation(flatDirection, transform.up);*/

        //Project to plane
        float distanceToPlane = Vector3.Dot(transform.up, billboardCamera.transform.position - transform.position);
        Vector3 pointOnPlane = billboardCamera.transform.position - (transform.up * distanceToPlane);

        transform.LookAt(pointOnPlane, transform.up);

        //transform.LookAt(lookAtCamera.transform.position);
    }
}
