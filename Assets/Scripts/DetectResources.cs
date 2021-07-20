using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectResources : MonoBehaviour
{
    [SerializeField] private Collider[] resources;
    public GameObject civilian;
    [SerializeField] private GameObject resourcepile;
    public List<Collider> resourcesList = new List<Collider>();
    public List<GameObject> resourcezone = new List<GameObject>();


    private void Awake()
    {
        foreach(Transform t in transform)
        {
            resourcezone.Add(t.gameObject);
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CivilianJob job = civilian.GetComponent<CivilianJob>();

         resources = Physics.OverlapSphere(this.transform.position, 20f);

        foreach(Collider collider in resources)
        {
            if(collider.tag == "Resource")
            {
                ResourceType resourceType = collider.GetComponent<ResourceType>();
                Debug.Log(resourceType);
                if (job.woodGatherer && resourceType.isWood)
                {
                    resourcesList.Add(collider);
                }
                if (job.stoneGatherer && resourceType.isStone)
                {
                    resourcesList.Add(collider);
                }
                if (job.clayGatherer && resourceType.isClay)
                {
                    Debug.Log("This is clay !!!");
                    resourcesList.Add(collider);
                }
            }
            

            else
            {
                continue;
            }

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector3 newrespos = Random.insideUnitSphere * 8;
            Instantiate(resourcepile, new Vector3(newrespos.x, newrespos.y, transform.position.z), Quaternion.identity);
        }
    }


}