using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosshair : MonoBehaviour
{
    private void Awake()
    {
        if(!Cursor.visible) 
        {
            Cursor.visible = true;
        }
    }
}
