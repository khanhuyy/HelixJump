using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.04f;

    // Start is called before the first frame update
    private void Start()
    {
        offset = transform.position - target.position;    
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed);
        transform.position = newPos;
    }
}
