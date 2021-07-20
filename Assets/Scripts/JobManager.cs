using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobManager : MonoBehaviour
{
    private GameObject selectedCitizen;
    public GameObject player;
    public GameObject heart;
    public GameObject newFlag;
    public GameObject flagCitizen;
    public GameObject zoneDetection;
    //private bool flagplaced = false;

    private InputManager inputManager;
    private GatherResourceAI gatherResourceAI;
    private ObjectInfo objectinfo;
    public Button StoneGathererButton;
    public Button WoodGathererButton;
    public Button ClayGathererButton;
    public Button BuilderButton;
    public Button FarmerButton;
    // Start is called before the first frame update
    void Start()
    {
        StoneGathererButton.onClick.AddListener(() => SetStoneGatherer());
        WoodGathererButton.onClick.AddListener(() => SetWoodGatherer());
        ClayGathererButton.onClick.AddListener(() => SetClayGatherer());
        BuilderButton.onClick.AddListener(() => SetBuilder());
        FarmerButton.onClick.AddListener(() => SetFarmer());

        //Now that we have the Type we can use it to Add Component
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    void SetStoneGatherer()
    {
        Debug.Log("SetJob");
        selectedCitizen = player.GetComponent<InputManager>().selectedObject.gameObject;
        Debug.Log(selectedCitizen.name);
        selectedCitizen.GetComponent<CivilianJob>().stoneGatherer = true;
        selectedCitizen.GetComponent<CivilianJob>().woodGatherer = false;
        selectedCitizen.GetComponent<CivilianJob>().clayGatherer = false;
        selectedCitizen.GetComponent<CivilianJob>().builder = false;
        selectedCitizen.GetComponent<CivilianJob>().farmer = false;  
        System.Type WorkScript = System.Type.GetType("GatherResourceAI" + ",Assembly-CSharp");
        selectedCitizen.AddComponent(WorkScript);
        Debug.Log("Did");
        if (selectedCitizen.GetComponent<GatherResourceAI>().flag == null)
        {
            flagCitizen = Instantiate(newFlag, selectedCitizen.transform.position, newFlag.transform.rotation);
            selectedCitizen.GetComponent<GatherResourceAI>().flag = flagCitizen;
            flagCitizen.GetComponent<DetectResources>().civilian = selectedCitizen;
        }
        else
        {
            selectedCitizen.GetComponent<GatherResourceAI>().flag.transform.position = selectedCitizen.transform.position;
        }

    }
    void SetWoodGatherer()
    {
        Debug.Log("SetJob");
        selectedCitizen = player.GetComponent<InputManager>().selectedObject.gameObject;
        Debug.Log(selectedCitizen.name); 
        selectedCitizen.GetComponent<CivilianJob>().stoneGatherer = false;
        selectedCitizen.GetComponent<CivilianJob>().woodGatherer = true;
        selectedCitizen.GetComponent<CivilianJob>().clayGatherer = false;
        selectedCitizen.GetComponent<CivilianJob>().builder = false;
        selectedCitizen.GetComponent<CivilianJob>().farmer = false;
        System.Type WorkScript = System.Type.GetType("GatherResourceAI" + ",Assembly-CSharp");
        selectedCitizen.AddComponent(WorkScript);
        Debug.Log("Did");
        if (selectedCitizen.GetComponent<GatherResourceAI>().flag == null)
        {
            flagCitizen = Instantiate(newFlag, selectedCitizen.transform.position, newFlag.transform.rotation);
            selectedCitizen.GetComponent<GatherResourceAI>().flag = flagCitizen;

            flagCitizen.GetComponent<DetectResources>().civilian = selectedCitizen;
        }
        else
        {
            selectedCitizen.GetComponent<GatherResourceAI>().flag.transform.position = selectedCitizen.transform.position;
        }



    }
    void SetClayGatherer()
    {
        Debug.Log("SetJob");
        selectedCitizen = player.GetComponent<InputManager>().selectedObject.gameObject;
        Debug.Log(selectedCitizen.name);
        selectedCitizen.GetComponent<CivilianJob>().stoneGatherer = false;
        selectedCitizen.GetComponent<CivilianJob>().woodGatherer = false;
        selectedCitizen.GetComponent<CivilianJob>().clayGatherer = true;
        selectedCitizen.GetComponent<CivilianJob>().builder = false;
        selectedCitizen.GetComponent<CivilianJob>().farmer = false;
        System.Type WorkScript = System.Type.GetType("GatherResourceAI" + ",Assembly-CSharp");
        selectedCitizen.AddComponent(WorkScript);
        Debug.Log("Did");
        if(selectedCitizen.GetComponent<GatherResourceAI>().flag == null)
        {
            flagCitizen = Instantiate(newFlag, selectedCitizen.transform.position, newFlag.transform.rotation);
            selectedCitizen.GetComponent<GatherResourceAI>().flag = flagCitizen;

            flagCitizen.GetComponent<DetectResources>().civilian = selectedCitizen;
        } else
        {
            selectedCitizen.GetComponent<GatherResourceAI>().flag.transform.position = selectedCitizen.transform.position;
        }
        
        
        
    }

    void SetBuilder()
    {
        Debug.Log("SetJob Builder");
        selectedCitizen = player.GetComponent<InputManager>().selectedObject.gameObject;
        Debug.Log(selectedCitizen.name);
        selectedCitizen.GetComponent<CivilianJob>().stoneGatherer = false;
        selectedCitizen.GetComponent<CivilianJob>().woodGatherer = false;
        selectedCitizen.GetComponent<CivilianJob>().clayGatherer = false;
        selectedCitizen.GetComponent<CivilianJob>().builder = true;
        selectedCitizen.GetComponent<CivilianJob>().farmer = false;
        System.String ScriptName = "BuilderAI";
        System.Type builderAI = System.Type.GetType(ScriptName + ",Assembly-CSharp");
        selectedCitizen.AddComponent(builderAI);
        Debug.Log("Did");
        GameObject zone = Instantiate(zoneDetection, selectedCitizen.transform.position, zoneDetection.transform.rotation);
        zone.transform.parent = selectedCitizen.transform;
    }

    void SetFarmer()
    {
        Debug.Log("SetJob Builder");
        selectedCitizen = player.GetComponent<InputManager>().selectedObject.gameObject;
        Debug.Log(selectedCitizen.name);
        selectedCitizen.GetComponent<CivilianJob>().stoneGatherer = false;
        selectedCitizen.GetComponent<CivilianJob>().woodGatherer = false;
        selectedCitizen.GetComponent<CivilianJob>().clayGatherer = false;
        selectedCitizen.GetComponent<CivilianJob>().builder = false;
        selectedCitizen.GetComponent<CivilianJob>().farmer = true;
        System.String ScriptName = "FarmerAI";
        System.Type farmerAI = System.Type.GetType(ScriptName + ",Assembly-CSharp");
        selectedCitizen.AddComponent(farmerAI);
        Debug.Log("Did");
    }
}
