using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementTool : MonoBehaviour
{
    public bool isActive = true;
    public GameObject PlaneTerrain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {


            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(new Vector3(0, 5, 0), new Vector3(0, 0, 0));
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                //Debug line for look direction of the mouse
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
                transform.position = pointToLook;
            }

            mouseActionPlacement();
        }
    }

    private void mouseActionPlacement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isActive = false;
        }
    }
}
