using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetComponent : MonoBehaviour
{

    public CapsuleCollider2D[] capCols;

    // Start is called before the first frame update
    void Start()
    {
        capCols = GetComponentsInChildren<CapsuleCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            EnableChildComponents();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            DisableChildComponents();
        }
    }

    void EnableChildComponents()
    {
        foreach(CapsuleCollider2D col in capCols) 
        {
            col.enabled = true;
        }
    }

    void DisableChildComponents()
    {
        foreach(CapsuleCollider2D col in capCols)
        {
            col.enabled = false;
        }
    }
}
