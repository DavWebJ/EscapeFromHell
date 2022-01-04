using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoudreLight : MonoBehaviour {


    MeteoAlleatoire M;
    
    public Light foudre;
    public float timer;
    public float mintime;
    public float maxtime;

    


     void Start()
    {
        
            StopCoroutine(Flashing());

        
        
        
        
    }

    IEnumerator Flashing()
    {
       

        foudre.intensity = 0.0f;
        yield return new WaitForSeconds(5f);
        foudre.intensity = 0.0f;
        yield return new WaitForSeconds(timer);
        foudre.intensity= 2f;
        yield return new WaitForSeconds(timer);
        foudre.intensity = 0.0f;
        yield return new WaitForSeconds(timer);
        foudre.intensity = 1.5f;
        yield return new WaitForSeconds(timer);
        foudre.intensity = 0.0f;
        yield return new WaitForSeconds(5f);
        foudre.intensity = 0.0f;
        yield return new WaitForSeconds(timer);
        foudre.intensity = 1f;
        yield return new WaitForSeconds(timer);
        foudre.intensity = 0.0f;
        yield return new WaitForSeconds(timer);
        foudre.intensity = 0.5f;
        yield return new WaitForSeconds(timer);
        foudre.intensity = 0.0f;

        StartCoroutine(Flashing());

        timer = Random.Range(mintime, maxtime);







    }
}
