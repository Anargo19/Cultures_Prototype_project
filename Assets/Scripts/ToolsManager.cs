using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsManager : MonoBehaviour
{

    public GameObject head;
    public GameObject farm;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void BTN_placement_building_HeadQuarter()
    {
        float mosPosx = Input.mousePosition.x;
        float mosPosz = Input.mousePosition.z;
        Instantiate(head, new Vector3(mosPosx, head.transform.position.y, mosPosz), head.transform.rotation);
    }

    public void BTN_placement_building_Farm()
    {
        float mosPosx = Input.mousePosition.x;
        float mosPosz = Input.mousePosition.z;
        Instantiate(farm, new Vector3(mosPosx, 4.01f, mosPosz), farm.transform.rotation);
    }
}
