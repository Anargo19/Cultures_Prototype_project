using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfo : MonoBehaviour
{
    private bool isFarm;
    public bool isSelected;
    [SerializeField] public Sprite storageResource { private set; get; }
    public int NbResources;
    public string Name { private set; get; }
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<BuildingType>().isFarm)
        {
            isFarm = true;
            Name = "Farm";
            storageResource = Resources.Load<Sprite>("Sprites/wheat");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
