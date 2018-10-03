using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Vector3 mouseOriginPoint;
    private Vector3 offset;
    private bool dragging;
    int MIDDLE_MOUSE_BTN = 2;

    private void LateUpdate()
    {
        // Debug.Log(Input.GetAxis("Mouse ScrollWheel"));

        // Make scroll scroll less when you are zoomed in really close, and more when you are zoomed out far
        float sizeFactor = Camera.main.orthographicSize * 0.1f;

        // Make scroll go faster
        float speed = 10f;

        float newSize = Camera.main.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * speed * sizeFactor;
        // Need to tag camera with tag MainCamera to get Camera.main to work
        // Clamp restrains newSize to be within [min, max] values
        Camera.main.orthographicSize = Mathf.Clamp(newSize, 2.5f, 50f); 

        
        if (Input.GetMouseButton(MIDDLE_MOUSE_BTN))
        {
            Vector3 mousePosWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = mousePosWorldPoint - transform.position;
            if (!dragging)
            {
                dragging = true;
                mouseOriginPoint = mousePosWorldPoint;
            }
        } else
        {
            dragging = false;
        }
        
        if (dragging)
        {
            transform.position = mouseOriginPoint - offset;
        }
    }
}
