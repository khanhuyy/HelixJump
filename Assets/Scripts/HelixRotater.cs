using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixRotater : MonoBehaviour
{
    public float rotationSpeed = 1000f;
    public float rotationSpeedPhone = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // #if INITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                float mouseX = Input.GetAxisRaw("Mouse X");
                transform.Rotate(transform.position.x, -mouseX * rotationSpeed * Time.deltaTime, transform.position.z);
            }
        
        // #elif UNITY_ANDROID
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
                float xDeltaPos = Input.GetTouch(0).deltaPosition.x;
                transform.Rotate(transform.position.x, -xDeltaPos * rotationSpeedPhone * Time.deltaTime, transform.position.z);
            }
        // #endif
    }
}
