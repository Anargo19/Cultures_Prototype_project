using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetNameatStart : MonoBehaviour
{
    public JobManager jobManager;
    private InputManager inputManager;
    private GameObject civilian;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = jobManager.player.GetComponent<InputManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inputManager.selectedObject != null)
        {
            if (inputManager.selectedObject.tag == "Selectable")
            {
                gameObject.GetComponent<TextMeshProUGUI>().text = inputManager.selectedObject.GetComponent<ObjectInfo>().objectName;
            }
        }
       
    }
}
