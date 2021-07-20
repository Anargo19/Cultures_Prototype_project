using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public float stone;
    public float maxStone;
    public GameObject Stone;
    private ObjectInfo objectInfo;
    private float resourcePosX;
    private float resourcePosZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void StoneGathering(Collider other)
    {
        resourcePosX = other.transform.position.x;
        resourcePosZ = other.transform.position.z;
        Vector3 ResourcespawnPos = new Vector3(resourcePosX, -0.5f, resourcePosZ);
        Destroy(other.gameObject);
        Instantiate(Stone, ResourcespawnPos, Stone.transform.rotation);
    }*/

    /*public IEnumerator GatherTick(Collider other)
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            if (this.objectInfo.stoneGatherer)
            {
                StoneGathering(other);
            }
        }
    }*/
}
