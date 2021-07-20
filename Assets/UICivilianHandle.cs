using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICivilianHandle : MonoBehaviour
{
    public JobManager jobManager;
    private InputManager inputManager;
    private GameObject civilianUI;
    public Camera civilianCamera;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = jobManager.player.GetComponent<InputManager>();
        civilianUI = gameObject.transform.Find("CivilianUI").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.selectedObject != null)
        {
            if(inputManager.selectedObject.tag == "Selectable")
            {
                if (inputManager.selectedObject.GetComponent<ObjectInfo>().isSelected)
                {
                    civilianCamera.transform.LookAt(new Vector3(inputManager.selectedObject.transform.position.x, inputManager.selectedObject.transform.position.y + 1, inputManager.selectedObject.transform.position.z));
                    civilianCamera.transform.position = new Vector3(inputManager.selectedObject.transform.position.x, civilianCamera.transform.position.y, inputManager.selectedObject.transform.position.z -17);
                    civilianCamera.orthographicSize = 1.2f;

                    civilianUI.SetActive(true);
                    Vector2 heartpos = (Vector2)Camera.main.WorldToScreenPoint(new Vector3(inputManager.selectedObject.transform.position.x, inputManager.selectedObject.transform.position.y + 3f, inputManager.selectedObject.transform.position.z));
                    civilianUI.transform.Find("HeartCivilian").GetComponent<RectTransform>().position = new Vector2(heartpos.x, heartpos.y);
                    GameObject experienceWork = civilianUI.transform.Find("ExperiencePanel").gameObject;
                    Debug.Log(experienceWork);
                    experienceWork.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Wood Gathering Experience : {inputManager.selectedObject.GetComponent<JobExperience>().ExperienceWoodGatherer}";
                    experienceWork.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"Stone Gathering Experience : {inputManager.selectedObject.GetComponent<JobExperience>().ExperienceStoneGatherer}";
                    experienceWork.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = $"Clay Gathering Experience : {inputManager.selectedObject.GetComponent<JobExperience>().ExperienceClayGatherer}";

                }
                else
                {
                    civilianUI.SetActive(false);
                }

            }
            else
            {
                civilianUI.SetActive(false);
            }
        }

        else
        {
            civilianUI.SetActive(false);
        }
    }
}
