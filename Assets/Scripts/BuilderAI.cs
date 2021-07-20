using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using System;

public class BuilderAI : MonoBehaviour
{
    [SerializeField] private GameObject building;
    [SerializeField] private Transform buildingpos;
    private List<String> resourceNecessary = new List<String>();
    private GameObject portableObject;
    //private bool hasResource = false;
    private bool hasWorkplace;
    private int i;
    private int z;
    private GameObject cible;

    private ObjectInfo objectInfo;
    private AnimatorState state;
    private enum AnimatorState
    {
        Idle,
        MovingToResource,
        GetResource,
        MovingToBuilding,
        Build,
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



        if (gameObject.GetComponent<CivilianJob>().builder) //Si le personnage est constructeur
        {
            
            
           if (hasWorkplace)
            {
                resourceNecessary = building.GetComponent<BuildingSystem>().resourceNecessary; //Récupère la lsite des ressources pour la construction
                switch (state)
                {
                    case AnimatorState.Idle:  //Ne fait rien
                        objectInfo.isWalking = false; //Reste sur place
                        i = UnityEngine.Random.Range(0, resourceNecessary.Count - 1); //Récupère une ressource random
                        String objectN = resourceNecessary[i]; //Récupère le nom de la ressource
                        if (objectN == "Wood") // Si c'est du bois
                        {
                            z = UnityEngine.Random.Range(0, gameObject.GetComponentInChildren<DetectBuildingResources>().resourcesListBuildingWood.Count - 1); //Trouve une ressource de bois alentour aléatoire
                            
                            cible = gameObject.GetComponentInChildren<DetectBuildingResources>().resourcesListBuildingWood[z].gameObject; //Détermine la ressource cible
                            Debug.Log(cible.name); // Donne nom cible
                            if (cible.GetComponent<ResourceType>().isWoodPile && cible.GetComponent<NbResourcePile>().NbResource == 0) // Si la ressource est une pile de bois sans ressource
                            {
                                state = AnimatorState.Idle; //Retourne état initial
                            }
                            
                            else 
                            {
                                objectInfo.isWalking = true; //Se met en route
                                agent.destination = new Vector3(cible.transform.position.x + 1, gameObject.transform.position.y, cible.transform.position.z + 1); //Détermine la destination avec la ressource cible
                                Debug.Log(agent.destination);

                                if (gameObject.transform.position.x == agent.destination.x && gameObject.transform.position.z == agent.destination.z) // Si arrivé
                                {
                                    objectInfo.isWalking = false; // Arrête de marcher
                                    state = AnimatorState.GetResource; // Change d'état
                                }
                            }
                           
                        }

                        if (objectN == "Clay")
                        {
                            z = UnityEngine.Random.Range(0, gameObject.GetComponentInChildren<DetectBuildingResources>().resourcesListBuildingClay.Count - 1);

                            cible = gameObject.GetComponentInChildren<DetectBuildingResources>().resourcesListBuildingClay[z].gameObject;
                            Debug.Log(cible.name);
                            if (cible.GetComponent<ResourceType>().isClayPile && cible.GetComponent<NbResourcePile>().NbResource == 0)
                            {
                                state = AnimatorState.Idle;
                            }

                            else
                            {
                                objectInfo.isWalking = true;
                                agent.destination = new Vector3(cible.transform.position.x + 1, gameObject.transform.position.y, cible.transform.position.z + 1);
                                Debug.Log(agent.destination);

                                if (gameObject.transform.position.x == agent.destination.x && gameObject.transform.position.z == agent.destination.z)
                                {
                                    objectInfo.isWalking = false;
                                    state = AnimatorState.GetResource;
                                }
                            }

                        }
                        break;

                    case AnimatorState.GetResource:
                        if (cible.GetComponent<ResourceType>().isWoodPile)
                        {
                            GetFromWoodPile();
                        }

                        if (cible.GetComponent<ResourceType>().isClayPile)
                        {
                            GetFromClayPile();
                        }
                        break;

                    case AnimatorState.MovingToBuilding:
                        agent.destination = building.transform.position;
                        objectInfo.isWalking = true;

                        if (gameObject.transform.position.x == agent.destination.x && gameObject.transform.position.z == agent.destination.z)
                        {
                            state = AnimatorState.Build;
                        }
                        break;

                    case AnimatorState.Build:
                        Building();
                        break;
                }

            }
        }
    }

    void GetFromWoodPile()
    {
            GameObject resourceTransp = cible.GetComponent<ResourceType>().Wood;
            Vector3 posgathres = new Vector3(cible.transform.position.x - 0.043f, cible.transform.position.y + 1.27f, cible.transform.position.z);
            cible.GetComponent<NbResourcePile>().NbResource -= 1;
            portableObject = Instantiate(resourceTransp, posgathres, resourceTransp.transform.rotation);
            portableObject.transform.parent = gameObject.transform;
            state = AnimatorState.MovingToBuilding;
        
    }

    void GetFromClayPile()
    {
        GameObject resourceTransp = cible.GetComponent<ResourceType>().Clay; //Définit la ressource temporaire
        Vector3 posgathres = new Vector3(cible.transform.position.x - 0.043f, cible.transform.position.y + 1.27f, cible.transform.position.z); //Définit la position de la ressource
        cible.GetComponent<NbResourcePile>().NbResource -= 1; //Retire une ressource de la pile
        portableObject = Instantiate(resourceTransp, posgathres, resourceTransp.transform.rotation); //Instantie la représentation temporaire de la ressource
        portableObject.transform.parent = gameObject.transform; //Définit la ressource en "child" du joueur
        state = AnimatorState.MovingToBuilding;//Définit que le civil va au bâtiment

    }

    void Building()
    {
        objectInfo.isWalking = false;
        Debug.Log("Arrived !");
        Destroy(portableObject);
        resourceNecessary.RemoveAt(i);
        if(resourceNecessary.Count == 0)
        {
            hasWorkplace = false;
            agent.destination = new Vector3(0, gameObject.transform.position.y, -20);
        } 
        else
        {
           state = AnimatorState.Idle;
        }
        
    }

    public void DefineWorkspace()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "Building")
            {
                building = hit.collider.gameObject;
                buildingpos = building.transform;
                Debug.Log("Builder Workplace");
                hasWorkplace = true;
            }

            if (hit.collider.tag == "Ground")
            {
                gameObject.GetComponent<BuilderAI>().agent.destination = hit.point;
                Debug.Log("Moving");
            }
            else if (hit.collider.tag == "Resource")
            {
                gameObject.GetComponent<BuilderAI>().agent.destination = hit.collider.gameObject.transform.position;
                Debug.Log("Harvesting");
            }

        }
    }
}
