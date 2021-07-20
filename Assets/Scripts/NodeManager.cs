using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public enum ResourceTypes { Stone}
    public ResourceTypes resourceType;

    public float harvestTime;
    public float avaibleResource;

    public int gatherers;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ResourceTick());
    }

    // Update is called once per frame
    void Update()
    {
        if(avaibleResource <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ResourceGather()
    {
        if(gatherers != 0)
        {
            avaibleResource -= gatherers;
        }
    }

    IEnumerator ResourceTick()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            ResourceGather();

        }
    }
}
