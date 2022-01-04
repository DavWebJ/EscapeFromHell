using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DNBaseControl : MonoBehaviour {

    protected DNCManager dayNightController;


    private void OnEnable()
    {
        dayNightController = this.GetComponent<DNCManager>();
        if (dayNightController != null)
        {
            dayNightController.AddModule(this);
        }
        
    }

    private void OnDisable()
    {
        if (dayNightController != null)
        {
            dayNightController.RemoveModule(this);
        }
    }
    public abstract void UpdateModul(float intensity);
    

    
   
}
