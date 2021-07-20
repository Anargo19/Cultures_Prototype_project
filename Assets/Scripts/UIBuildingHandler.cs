using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildingHandler : MonoBehaviour
{
    public BuildingInfo buildinginfo;
    private InputManager inputManager;
    private GameObject buildingUI;
    public Camera civilianCamera;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GameObject.Find("Player").GetComponent<InputManager>();   
        buildingUI = gameObject.transform.Find("BuildingUI").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.selectedObject != null)
        {
            if (inputManager.selectedObject.tag == "Building")
            {
                if (inputManager.selectedObject.GetComponent<BuildingInfo>().isSelected)
                {
                    civilianCamera.transform.LookAt(new Vector3(inputManager.selectedObject.transform.position.x, inputManager.selectedObject.transform.position.y + 1, inputManager.selectedObject.transform.position.z));
                    civilianCamera.transform.position = new Vector3(inputManager.selectedObject.transform.position.x, civilianCamera.transform.position.y, inputManager.selectedObject.transform.position.z - 12);
                    civilianCamera.orthographicSize = 5;

                    buildingUI.SetActive(true);
                    buildingUI.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = inputManager.selectedObject.GetComponent<BuildingInfo>().Name;
                    buildingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite = inputManager.selectedObject.GetComponent<BuildingInfo>().storageResource;
                    buildingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = inputManager.selectedObject.GetComponent<BuildingInfo>().NbResources.ToString();
                }
                else
                {
                    buildingUI.SetActive(false);
                }
            }
            else
            {
                buildingUI.SetActive(false);
            }


        }

        else
        {
            buildingUI.SetActive(false);
        }
    }
}
