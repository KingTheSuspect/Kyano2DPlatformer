using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwipe : MonoBehaviour
{
    private Vector3 touchStart;
    public Camera cam;
    public float ValueX = 0;


    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = GetWorldPosition(ValueX);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - GetWorldPosition(ValueX);
            direction.x = Mathf.Clamp(direction.x, -1, 1);
            direction.y = 0;
            direction.z = 0;
            cam.transform.position += direction;
        }
    }
    private Vector3 GetWorldPosition(float x)
    {
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.forward, new Vector3(x, 0));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }
}