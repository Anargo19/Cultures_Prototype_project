using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GatherResourceAI : MonoBehaviour
{
    private enum AnimatorState
    {
        Idle,
        MovingToResource,
        DestroyResource,
        GoingGatheringResource,
        GatheringResource,
        MovingToStore,
        Store,
    }

    public GameObject flag;
    [SerializeField] public List<GameObject> resourceZone = new List<GameObject>();
    public GameObject gatheredObject;
    public GameObject portableObject;
    public GameObject woodPile;
    public List<Collider> resourcesList = new List<Collider>();

    private AnimatorState state;

    [SerializeField] public Mesh[] clayPileMesh;
    public Mesh[] stonePileMesh;
    public Mesh[] woodPileMesh;

    public bool isWalking = false;

    private int randresource;
    public int NbofResourcePile;
    private NbResourcePile nbResources;
    private int indexzone;

    private GameObject cibleresource;
    public NavMeshAgent agent;
    public ObjectInfo objectInfo;
    private Animator civilianAnimator;
    [SerializeField] private GameObject manager;
    [SerializeField] private Material clayMaterial;




    // Start is called before the first frame update
    void Start()
    {
        indexzone = 0;
        resourceZone = flag.GetComponent<DetectResources>().resourcezone;
        clayPileMesh = GameObject.Find("Managers").GetComponent<MeshManager>().clayPileMesh;
        stonePileMesh = GameObject.Find("Managers").GetComponent<MeshManager>().stonePileMesh;
        woodPileMesh = GameObject.Find("Managers").GetComponent<MeshManager>().woodPileMesh;
        objectInfo = gameObject.GetComponent<ObjectInfo>();
        resourceZone[indexzone] = flag.transform.GetChild(0).gameObject;
        clayMaterial = Resources.Load<Material>("Materials/Clay");
        InitiatePiles();

        agent = GetComponent<NavMeshAgent>();
        civilianAnimator = GetComponent<Animator>();
        state = AnimatorState.Idle;

    }

    // Update is called once per frame
    void Update()
    {

        nbResources = resourceZone[indexzone].GetComponent<NbResourcePile>();
        GatherAI();
        NbRessources();



    }

    private void InitiatePiles()
    {
        if (gameObject.GetComponent<CivilianJob>().clayGatherer)
        {
            foreach (GameObject g in resourceZone)
            {
                g.transform.localScale = new Vector3(32, 36, 46);
                g.GetComponent<MeshRenderer>().material = clayMaterial;
                g.GetComponent<ResourceType>().isClayPile = true;
            }

        } else if (gameObject.GetComponent<CivilianJob>().woodGatherer)
        {
            foreach (GameObject g in resourceZone)
            {
                g.GetComponent<ResourceType>().isWoodPile = true;
            }
        }
    }


    public void MoveGatherResource()
    {

        NavMeshAgent agent = gameObject.GetComponent<ObjectInfo>().agent;
        Debug.Log(gameObject.name);
        agent.destination = resourcesList[0].transform.position;
    }

    void GatherRawResource(GameObject rawresource)
    {
        GameObject resource;
        if (gameObject.GetComponent<CivilianJob>().stoneGatherer)
        {
            resource = rawresource.GetComponent<ResourceType>().Stone;
            float resourcePosX = rawresource.transform.position.x;
            float resourcePosY = rawresource.transform.position.y;
            float resourcePosZ = rawresource.transform.position.z;
            Vector3 ResourcespawnPos = new Vector3(resourcePosX, resourcePosY + 0.40f, resourcePosZ);
            resourcesList.RemoveAt(randresource);
            Destroy(rawresource);
            gatheredObject = Instantiate(resource, ResourcespawnPos, resource.transform.rotation);
            gameObject.GetComponent<JobExperience>().ExperienceStoneGatherer++;
        }

        else if (gameObject.GetComponent<CivilianJob>().woodGatherer)
        {
            resource = rawresource.GetComponent<ResourceType>().Wood;
            float resourcePosX = rawresource.transform.position.x;
            float resourcePosY = rawresource.transform.position.y;
            float resourcePosZ = rawresource.transform.position.z;
            Vector3 ResourcespawnPos = new Vector3(resourcePosX, resourcePosY, resourcePosZ);
            resourcesList.RemoveAt(randresource);
            Destroy(rawresource);
            gatheredObject = Instantiate(resource, ResourcespawnPos, resource.transform.rotation);
            gameObject.GetComponent<JobExperience>().ExperienceWoodGatherer++;
        }

        else if (gameObject.GetComponent<CivilianJob>().clayGatherer)
        {
            resource = rawresource.GetComponent<ResourceType>().Clay;
            float resourcePosX = rawresource.transform.position.x;
            float resourcePosY = rawresource.transform.position.y;
            float resourcePosZ = rawresource.transform.position.z;
            Vector3 ResourcespawnPos = new Vector3(resourcePosX, resourcePosY, resourcePosZ);
            resourcesList.RemoveAt(randresource);
            Destroy(rawresource);
            gatheredObject = Instantiate(resource, ResourcespawnPos, resource.transform.rotation);
            gameObject.GetComponent<JobExperience>().ExperienceClayGatherer++;
        }


    }


    void GetResource()
    {
        if (resourcesList.Count == 0)
        {
            state = AnimatorState.Idle; 
        }
        else
        {
            randresource = Random.Range(0, resourcesList.Count - 1);
            cibleresource = resourcesList[randresource].gameObject;

            if (cibleresource == null)
            {
                state = AnimatorState.Idle;
            }
            else
            {

                state = AnimatorState.MovingToResource;
            }
        }


    }

    void MovetoResource()
    {
        if (gameObject.GetComponent<CivilianJob>().woodGatherer)
        {
            agent.destination = new Vector3(resourcesList[randresource].transform.position.x, gameObject.transform.position.y, resourcesList[randresource].transform.position.z + 1.5f); //resourcesList[randresource].transform.position;
            objectInfo.isWalking = true;
            objectInfo.isIdle = false;
        }
        else
        {
            agent.destination = new Vector3(resourcesList[randresource].transform.position.x, gameObject.transform.position.y, resourcesList[randresource].transform.position.z); //resourcesList[randresource].transform.position;
            objectInfo.isWalking = true;
            objectInfo.isIdle = false;
        }

    }

    void MovetoGather()
    {
        agent.destination = new Vector3(gatheredObject.transform.position.x, gameObject.transform.position.y, gatheredObject.transform.position.z);
        objectInfo.isWalking = true;
    }

    void GatherResource()
    {
        float gatheredresourcePosX = gatheredObject.transform.position.x;
        float gatheredresourcePosY = gatheredObject.transform.position.y;
        float gatheredresourcePosZ = gatheredObject.transform.position.z;
        GameObject resourcegath = gatheredObject;
        Vector3 posgathres = new Vector3(gatheredresourcePosX - 0.043f, gatheredresourcePosY + 1.27f, gatheredresourcePosZ);

        Destroy(gatheredObject);
        portableObject = Instantiate(resourcegath, posgathres, resourcegath.transform.rotation);
        portableObject.transform.parent = gameObject.transform;
        portableObject.transform.localScale = new Vector3(3, 1.5f, 3);
    }

    void MovingtoFlag()
    {
        agent.destination = flag.transform.position;
        objectInfo.isWalking = true;
    }

    void Store()
    {
        Destroy(portableObject);
        nbResources.NbResource++;
    }

    void GatherAI()
    {
        resourcesList = flag.GetComponent<DetectResources>().resourcesList;
        NavMeshAgent agent = gameObject.GetComponent<ObjectInfo>().agent;

        switch (state)
        {
            case AnimatorState.Idle:
                GetResource();
                break;

            case AnimatorState.MovingToResource:
                MovetoResource();
                if (gameObject.transform.position.x == agent.destination.x && gameObject.transform.position.z == agent.destination.z && cibleresource != null)
                {
                    state = AnimatorState.DestroyResource;
                }
                break;

            case AnimatorState.DestroyResource:
                GatherRawResource(cibleresource);
                state = AnimatorState.GoingGatheringResource;
                break;

            case AnimatorState.GoingGatheringResource:
                MovetoGather();
                if (gameObject.transform.position.x == agent.destination.x && gameObject.transform.position.z == agent.destination.z)
                {
                    state = AnimatorState.GatheringResource;
                }

                break;

            case AnimatorState.GatheringResource:
                GatherResource();
                state = AnimatorState.MovingToStore;
                break;

            case AnimatorState.MovingToStore:
                MovingtoFlag();
                if (gameObject.transform.position.x == agent.destination.x && gameObject.transform.position.z == agent.destination.z)
                {
                    state = AnimatorState.Store;
                }
                break;

            case AnimatorState.Store:
                Store();
                state = AnimatorState.Idle;
                break;
        }


    }


    private IEnumerator DigClay()
    {
        civilianAnimator.Play("Digging");
        yield return new WaitForSeconds(5);
        GatherRawResource(cibleresource);
    }
    
    private void NbRessources()
    {
        if(nbResources.NbResource <= 5)
        {
            if (gameObject.GetComponent<CivilianJob>().stoneGatherer)
            {
                resourceZone[indexzone].GetComponent<MeshFilter>().mesh = stonePileMesh[nbResources.NbResource];
            }
            else if (gameObject.GetComponent<CivilianJob>().woodGatherer)
            {
                resourceZone[indexzone].GetComponent<MeshFilter>().mesh = woodPileMesh[nbResources.NbResource];
            }
            else if (gameObject.GetComponent<CivilianJob>().clayGatherer)
            {
                resourceZone[indexzone].GetComponent<MeshFilter>().mesh = clayPileMesh[nbResources.NbResource];
            }
        }
        

        if(nbResources.NbResource >= 5)
        {
            Debug.Log("BESOIN D4UNE NOUVELLE RESOURCE PILE LBLBLBLB");
            indexzone++;
            Debug.Log(indexzone);
        }

    }
    /*
    private void NewResourcePile()
    {
        Debug.Log("NewresourcePile");
        GameObject newrespile = flag.transform.GetChild(0).gameObject;
        Vector3 newrespos = NewPosition();
        resourceZone = Instantiate(newrespile, newrespos, Quaternion.identity);
        NbofResourcePile = 0;
    }

    private Vector3 NewPosition()
    {
        Debug.Log("Newposition");
        
        Collider[] resources = Physics.OverlapSphere(this.transform.position, 5);
        List<Bounds> colliders = new List<Bounds>();
        Vector3 rand = Random.insideUnitSphere * 5;
        Vector3 newrespos = new Vector3(rand.x, rand.y, transform.position.z);
        foreach(Collider collider in resources)
        {
                colliders.Add(collider.bounds);
        }
        if (colliders.Contains(newrespos))
        {
            NewPosition();
            return Vector3.zero;
        }
        else
        {

            return newrespos;
        }
    }*/
    

    /* private void OnTriggerEnter(Collider other)
     {
         ResourceType resourcetype = other.GetComponent<ResourceType>();
         if (resourcetype.isClay)
         {
             civilianAnimator.Play("Digging");
             StartCoroutine(DigClay());
         }
     }*/
}
