using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 10;
    public Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;
    }

    public void ChangeCameraTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ChangeCameraTargetNoSmooth(Transform newTarget)
    {
        float savedSmootherSpeed = smoothSpeed;
        smoothSpeed = 100;
        target = newTarget;
        smoothSpeed = savedSmootherSpeed;
    }
}
