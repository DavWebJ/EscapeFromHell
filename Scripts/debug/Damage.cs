using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlackPearl;
public class Damage : MonoBehaviour
{
    public int damage = 10;
    public float timer = 0;

    void FixedUpdate()
    {  
        if (timer >= 2)
        {
            timer = 0;
        }
        timer += Time.fixedDeltaTime;
    }
    void OnTriggerStay(Collider coll) {
        if(coll.tag == "Player")
        {
            if(timer >= 2)
            {
               
                coll.GetComponent<VitalState>().Takedamage(damage);
               
            }

            
        }
    }
}
