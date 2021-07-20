using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public float panSpeed;
    private float panDetect = 15.0f;
    public float rotateSpeed;
    public float rotateAmount;
    private Quaternion rotation;
   // private float minHeight = 10f;
   // private float maxHeight = 100f;
    public GameObject selectedObject;
    public GameObject Civilian;
    public GameObject CivilianMenu;
    public GameObject Canvas;
    private ObjectInfo selectedInfo;
    private BuildingInfo selectedbInfo;
    // Start is called before the first frame update
    void Start()
    {
        rotation = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        RotateCamera();

        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            LeftClick();
        }

        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            RightClick();
        }

       
    }

    public void LeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider.tag == "Ground")
            {
                if (selectedObject != null)
                {
                    selectedInfo.isSelected = false;
                    selectedObject = null;
                }

                Debug.Log("Deselect");
            } else if(hit.collider.tag == "Selectable" )
            {
                if(selectedObject != null)
                {
                    if(selectedbInfo != null)
                    {
                        selectedbInfo.isSelected = false;
                        selectedObject = null;
                        selectedbInfo = null;
                    }
                    else
                    {

                        selectedInfo.isSelected = false;
                        selectedObject = null;
                        selectedInfo = null;
                    }
                    
                }
                
                selectedObject = hit.collider.gameObject;
                selectedInfo = selectedObject.GetComponent<ObjectInfo>();

                selectedInfo.isSelected = true;

                Debug.Log("Selected" + selectedInfo.objectName);
            }
            else if(hit.collider.tag == "Building")
            {
                if (selectedObject != null)
                {
                    if (selectedbInfo != null)
                    {
                        selectedbInfo.isSelected = false;
                        selectedObject = null;
                    }
                    else
                    {

                        selectedInfo.isSelected = false;
                        selectedObject = null;
                    }

                }

                selectedObject = hit.collider.gameObject;
                selectedbInfo = selectedObject.GetComponent<BuildingInfo>();

                selectedbInfo.isSelected = true;
                Debug.Log("Building");
                
                //buildingPlacement.BuildingUI(selectedBuilding);
            }else if(hit.collider.tag == "UI")
            {

            }
        }
    }

    public void RightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "Selectable")
            {

                selectedObject = hit.collider.gameObject;
                selectedInfo = selectedObject.GetComponent<ObjectInfo>();
                selectedInfo.isSelected = true;
                Debug.Log("Right cliked" + selectedInfo.objectName);
                Debug.Log(Input.mousePosition);


                CivilianMenu.transform.parent.position = Input.mousePosition;
                Debug.Log(CivilianMenu.transform.parent.position);
                CivilianMenu.transform.parent.gameObject.SetActive(true);
                CivilianMenu.SetActive(true);

                
                Debug.Log("Right cliked" + selectedInfo.objectName);
            }
            
        }
    }

    void MoveCamera()
    {
        float moveX = Camera.main.transform.position.x;
        float moveY = Camera.main.transform.position.y;
        float moveZ = Camera.main.transform.position.z;

        float xPos = Input.mousePosition.x;
        float yPos = Input.mousePosition.y;

        if(Input.GetKey(KeyCode.Q) || xPos > 0 && xPos < panDetect)
        {
            moveX -= panSpeed;
        }

        if(Input.GetKey(KeyCode.D) || xPos < Screen.width && xPos > Screen.width - panDetect)
        {
            moveX += panSpeed;
        }

        if (Input.GetKey(KeyCode.S) || yPos > 0 && yPos < panDetect)
        {
            moveZ -= panSpeed;
        }

        if (Input.GetKey(KeyCode.Z) || yPos < Screen.height && yPos > Screen.height - panDetect)
        {
            moveZ += panSpeed;
        }

        Vector3 pos = new Vector3(moveX, moveY, moveZ);

        Camera.main.transform.position = pos;
    }

    void RotateCamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;

        if(Input.GetMouseButtonDown(2))
        {
            destination.x -= Input.GetAxis("Mouse Y") * rotateAmount;
            destination.y += Input.GetAxis("Mouse X") * rotateAmount;
        }

        if(destination != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * rotateSpeed);
        }
    }
}

