using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float smoothSpeed;  // the higher, the faster the camera will follow
    private float offset;


    void Start()
    {
        offset = transform.position.z;
    }

    void FixedUpdate()
    {

        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, offset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

    }
}
