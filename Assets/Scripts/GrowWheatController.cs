using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowWheatController : MonoBehaviour
{

    public int grownupstate;
    [SerializeField] private Mesh[] wheatmeshes;

    private bool _isgrownup = true;

    public event OnVariableChangeDelegate OnVariableChange;
    public delegate void OnVariableChangeDelegate(bool newVal);
    public bool yes;
    public bool isgrownup
    {
        get
        {
            return _isgrownup;
        }
        set
        {
            if (_isgrownup == value) return;
            _isgrownup = value;
            if (OnVariableChange != null)
                OnVariableChange(_isgrownup);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isgrownup = true;
        grownupstate = 0;
        OnVariableChange += VariableChangeHandler;
    }

    // Update is called once per frame
    void Update()
    {
        yes = isgrownup;
        GrowUpAppearence();
    }

    public void GrowUpAppearence()
    {
        GetComponent<MeshFilter>().mesh = wheatmeshes[grownupstate];
    }

    private void VariableChangeHandler(bool newVal)
    {
        if(isgrownup == false)
        {

            Invoke("IsgrownUp", 10);
        }
    }

    private void IsgrownUp()
    {
        isgrownup = true;
    }


}
