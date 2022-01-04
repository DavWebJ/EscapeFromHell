using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostZone : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
           HUD.instance.ScreenEffect("frost");
           
           
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
        {
           HUD.instance.ScreenEffect("defrost");
           
           
        }
    }

    
}
