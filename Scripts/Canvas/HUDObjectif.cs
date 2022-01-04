using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlackPearl;

public class HUDObjectif : MonoBehaviour
{
    public static HUDObjectif instance = null;


    private void Awake() {
    if(instance == null)
        instance = this;
        
    }
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }
}
