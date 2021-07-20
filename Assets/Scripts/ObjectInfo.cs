using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectInfo : MonoBehaviour
{
    public bool isSelected = false;
    public bool isMoving = false;
    public bool isIdle = true;
    public bool isWalking = false;
    private bool hungry = false;

    //Bool for Jobs
    public bool isGathering = false;
    

    private Animator animator;

    public string objectName;

    public ResourceType typeResource;
    public NavMeshAgent agent;

    public NodeManager.ResourceTypes heldResourceType;
    public int heldResource;
    public int maxHeldResource;
    public float hunger { private set; get; }
    public float health { private set; get; }

    //GameObjects for Ressources
    public GameObject Stone;
    public GameObject Clay;
    public GameObject Wood;


    void Awake()
    {
        objectName = RandomName();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.Play("Idle");
        hunger = 1;
        health = 1;
        
       // Text Name = gameObject.transform.Find("NameTxt").GetComponent<Text>();
       // Name.text = name; 
    }

    // Update is called once per frame
    void Update()
    {

       if (isWalking)
        {
            animator.Play("Walk");
            if (Vector3.Distance(gameObject.transform.position, agent.destination) < 0.1f)
            {
                isMoving = false;
                isWalking = false;
                gameObject.GetComponent<Animator>().Play("Idle");
            }
        }

        if (Input.GetMouseButtonDown(1) && isSelected && !EventSystem.current.IsPointerOverGameObject())
        {
            RightClick();
        }

        
    }

    private string RandomName()
    {
        List<string> name = new List<string>();
        name.Add("Titouan");
        name.Add("Anael");
        name.Add("Gaetan");
        name.Add("Raphael");
        name.Add("Adrien");
        return name[Random.Range(0, name.Count - 1)];
    }
    

    public void RightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.Log("RightClickedToMove");
        LayerMask.GetMask("Default");
        if(Physics.Raycast(ray, out hit, 100))
        {
            Debug.Log("RaycastLaunched");
            Debug.Log(hit.point);
            if (hit.collider.tag == "Ground")
            {
                isMoving = true;
                isIdle = false;
                isWalking = true;
                animator.Play("Walk");
                Debug.Log("Is Moving");
                agent.destination = hit.point;
            }else if(hit.collider.tag == "Resource")
            {
                agent.destination = hit.collider.gameObject.transform.position;
                Debug.Log("Harvesting");
            }
        }



    }


    
}
