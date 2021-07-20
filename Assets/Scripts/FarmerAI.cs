using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmerAI : MonoBehaviour
{
    private bool hasWorkplace;
    private GameObject Workplace;
    private ObjectInfo objectInfo;
    private GameObject farm;
    private GameObject wheat;
    [SerializeField] private GameObject gatheredWheat;
    private GameObject portGathered;
    private AnimatorState state;
    private enum AnimatorState
    {
        Idle,
        MovingToplace,
        GrowResource,
        GetResource,
        ReturnToPlace,
        Store,
    }

    public NavMeshAgent agent;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        objectInfo = GetComponent<ObjectInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && gameObject.GetComponent<ObjectInfo>().isSelected)
        {
            DefineWorkspace();
        }

        if (hasWorkplace)
        {
            switch (state)
            {
                case AnimatorState.Idle:  //Ne fait rien
                    int i;
                    objectInfo.isWalking = false; //Reste sur place
                    i = UnityEngine.Random.Range(0, farm.transform.childCount); //Récupère le nom de la ressource
                    Debug.Log(i);
                    wheat = farm.transform.GetChild(i).gameObject;
                    if (wheat.GetComponent<GrowWheatController>().isgrownup) // Si c'est du bois
                    {
                        state = AnimatorState.MovingToplace;
                    }
                    else
                    {
                        state = AnimatorState.Idle;
                    }
                    
                    break;

                case AnimatorState.MovingToplace:

                    objectInfo.isWalking = true;
                    agent.destination = wheat.transform.position;
                    bool v = agent.destination.x == gameObject.transform.position.x;
                    bool u = agent.destination.z == gameObject.transform.position.z;
                    if (v && u && wheat.GetComponent<GrowWheatController>().grownupstate < 3)
                    {
                        Debug.Log("ArrivedGrow");
                        state = AnimatorState.GrowResource;
                    }
                    else if(v && wheat.GetComponent<GrowWheatController>().grownupstate >= 3)
                    {
                        state = AnimatorState.GetResource;
                    }
                    break;

                case AnimatorState.GrowResource:
                    Debug.Log("GettingGrow");
                    wheat.GetComponent<GrowWheatController>().grownupstate++;
                    wheat.GetComponent<GrowWheatController>().isgrownup = false;
                    Debug.Log("GrownUp");
                    state = AnimatorState.Idle;
                    break;

                case AnimatorState.GetResource:
                    wheat.GetComponent<GrowWheatController>().grownupstate = 0;
                   // portGathered = Instantiate(gatheredWheat, transform);
                    state = AnimatorState.Store;
                    break;
                case AnimatorState.Store:
                    agent.destination = Workplace.transform.position;
                    farm.GetComponent<BuildingInfo>().NbResources++;
                   //Destroy(portGathered);
                    state = AnimatorState.Idle;
                    break;

                case AnimatorState.ReturnToPlace:
                    agent.destination = Workplace.transform.position;
                    if(agent.destination.x == gameObject.transform.position.x && agent.destination.z == gameObject.transform.position.z)
                    {
                        state = AnimatorState.Idle;
                    }
                    break;
            }
        }
    }


    public void DefineWorkspace()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            //BuildingType buildingtype = hit.collider.gameObject.GetComponent<BuildingType>();
            farm = hit.collider.gameObject;
            Debug.Log(farm.GetComponent<BuildingType>().isFarm);
            bool v = farm.GetComponent<BuildingType>().isFarm;
            if (hit.collider.tag == "Building" && v)
            {
                Workplace = hit.collider.gameObject;
                Debug.Log("Farmer Workplace");
                hasWorkplace = true;
            }


        }
    }
}
