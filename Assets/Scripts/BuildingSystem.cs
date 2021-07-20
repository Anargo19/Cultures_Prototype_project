using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    private Transform BaseTransform;
    [SerializeField] private GameObject building;
    private GameObject wood;
    private GameObject stone;
    public List<String> resourceNecessary = new List<String>();
    // Start is called before the first frame update
    void Start()
    {
        BaseTransform = gameObject.transform;

        if (gameObject.GetComponent<BuildingType>().isHeadquarter)
        {
            resourceNecessary.Add("Wood");
            resourceNecessary.Add("Wood");
        }
        if (gameObject.GetComponent<BuildingType>().isFarm)
        {
            resourceNecessary.Add("Clay");
            resourceNecessary.Add("Wood");
            resourceNecessary.Add("Wood");
        }
        if (gameObject.GetComponent<BuildingType>().isHouse)
        {
            resourceNecessary.Add("Clay");
            resourceNecessary.Add("Wood");
            resourceNecessary.Add("Wood");
            resourceNecessary.Add("Wheat");
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<PlacementTool>().isActive == false)
        {
            if (resourceNecessary.Count == 0)
            {
                StartCoroutine(BuildingTick());
            }
        }

        //
    }

    IEnumerator BuildingTick()
    {

        while (gameObject.GetComponent<PlacementTool>().isActive == false)
        {
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
            Instantiate(building, new Vector3(BaseTransform.position.x, building.transform.position.y, BaseTransform.position.z), BaseTransform.rotation);
            
        }
    }
}
