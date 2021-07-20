using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBuildingResources : MonoBehaviour
{
    [SerializeField] private Collider[] resources;
    public List<GameObject> resourcesListBuildingStone = new List<GameObject>();
    public List<GameObject> resourcesListBuildingWood = new List<GameObject>();
    public List<GameObject> resourcesListBuildingClay = new List<GameObject>();
    public List<GameObject> testlist = new List<GameObject>();
    public ObjectType objectType;
    public ResourceType typeResource;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DetectBuildingRes", 0, 2); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DetectBuildingRes()
    {
        resourcesListBuildingWood.Clear();
        resourcesListBuildingClay.Clear();
        resourcesListBuildingStone.Clear();
        resources = Physics.OverlapSphere(this.transform.position, 30f);

        foreach (Collider collider in resources)
        {
            if (collider.tag == "Resource")
            {
                try
                {
                    ResourceType resourceType = collider.GetComponent<ResourceType>();
                    Debug.Log(resourceType);
                    if (resourceType.isWoodPile)
                    {
                        resourcesListBuildingWood.Add(collider.gameObject);
                    }
                    if (resourceType.isStonePile)
                    {
                        resourcesListBuildingStone.Add(collider.gameObject);
                    }
                    if (resourceType.isClayPile)
                    {
                        Debug.Log("This is clay !!!");
                        resourcesListBuildingClay.Add(collider.gameObject);
                    }
                }
                catch
                {
                    continue;
                }
                
            }


            else
            {
                continue;
            }

        }
    }
}