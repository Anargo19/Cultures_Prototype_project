using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NbResourcePile : MonoBehaviour
{
    public int NbResource;
    /*public int NbResource
    {
        get { return Nb_Resources; }
        set
        {
            if (Nb_Resources == value) return;
            Nb_Resources = value;
            if (OnVariableChange != null)
                OnVariableChange(Nb_Resources);
        }
    }*/

    private int Nb_Resources = 0;
   
    public delegate void OnVariableChangeDelegate(int newVal);
    public event OnVariableChangeDelegate OnVariableChange;
    // Start is called before the first frame update
    void Start()
    {
        this.OnVariableChange += VariableChangeHandler;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void VariableChangeHandler(int newVal)
    {
        Debug.Log("YAS");
    }


}
