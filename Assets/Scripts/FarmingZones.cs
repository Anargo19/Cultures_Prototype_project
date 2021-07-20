using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingZones : MonoBehaviour
{
    private Transform FarmZoneoneTransform;
    public Terrain t;

    public int posX;
    public int posZ;
    public float[] textureValues;

    void Start()
    {/*
        t = Terrain.activeTerrain;
        FarmZoneoneTransform = gameObject.transform;

        GetTerrainTexture(FarmZoneoneTransform);
        if(textureValues[0] > 0)
        {
            Debug.Log("Grass");
        }
        else
        {
            GameObject.Destroy(gameObject);
        }*/


        /*int z = gameObject.transform.childCount;
        for(int i = 0; i<z; i++)
        {

        }
        */
    }

    void Update()
    {
        // For better performance, move this out of update 
        // and only call it when you need a footstep.
        
    }

    public void GetTerrainTexture(Transform f)
    {
        ConvertPosition(f.position);
        CheckTexture();
    }

    void ConvertPosition(Vector3 playerPosition)
    {
        Vector3 terrainPosition = playerPosition - t.transform.position;

        Vector3 mapPosition = new Vector3
        (terrainPosition.x / t.terrainData.size.x, 0,
        terrainPosition.z / t.terrainData.size.z);

        float xCoord = mapPosition.x * t.terrainData.alphamapWidth;
        float zCoord = mapPosition.z * t.terrainData.alphamapHeight;

        posX = (int)xCoord;
        posZ = (int)zCoord;
    }

    void CheckTexture()
    {
        float[,,] aMap = t.terrainData.GetAlphamaps(posX, posZ, 1, 1);
        textureValues[0] = aMap[0, 0, 0];
        textureValues[1] = aMap[0, 0, 1];
        textureValues[2] = aMap[0, 0, 2];

    }
}