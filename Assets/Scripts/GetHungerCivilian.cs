using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetHungerCivilian : MonoBehaviour
{
    private JobManager jobManager;
    private InputManager inputManager;
    private GameObject civilian;
    // Start is called before the first frame update
    void Start()
    {
        jobManager = GameObject.Find("Managers").GetComponent<JobManager>();
        inputManager = jobManager.player.GetComponent<InputManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.selectedObject != null && inputManager.selectedObject.tag == "Selectable")
        {
            
            gameObject.GetComponent<Image>().fillAmount = inputManager.selectedObject.GetComponent<ObjectInfo>().hunger;
        }

    }
}
