using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]

public class SousLeau : MonoBehaviour {
    private Camera cam;
    private Material defaultSkybox;
    private float defaultStartDistance;
    public float volmax;
    public float Waterlevel;
    public bool IsunderWater;
    public Color normalcolor;
    public Color underwatercolor;
    
    public AudioSource clip;
    public AudioClip EntrandoAgua, Submerso;
    public GameObject souslamer;
    public bool IsSwiming;
 

    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>(); // new Color(0, 0.4f, 0.7f, 1);
        defaultSkybox = RenderSettings.skybox;
        defaultStartDistance = RenderSettings.fogStartDistance;
        
        clip = GetComponent<AudioSource>();
        RenderSettings.fog = false;
        normalcolor = new Color32(178, 178, 178, 100);
        underwatercolor = new Color32(144, 189, 204,255);
       
        souslamer = new GameObject();
        souslamer.AddComponent(typeof(AudioSource));
        souslamer.GetComponent<AudioSource> ().loop = true;
        souslamer.transform.localPosition = new Vector3(0, 0, 0);
        souslamer.GetComponent<AudioSource>().clip = Submerso;
        souslamer.transform.parent = transform;
        souslamer.SetActive  (false);
        souslamer.GetComponent<AudioSource>().volume = 0.5f;
        







    }


    // Update is called once per frame
    void Update () {
        
         if ((transform.position.y < Waterlevel) != IsunderWater)
            {
                IsunderWater = transform.position.y < Waterlevel;
                if (IsunderWater)
                {
                    
                    SetUnderWater();
                }
                else
                {
                    SetNormal();

                }
            }
        


    }

    void SetNormal()
    {
        RenderSettings.fogColor = normalcolor;
        RenderSettings.fogDensity = 0.000f;
     
        
    }


    


    void SetUnderWater()
    {
        RenderSettings.fogColor = underwatercolor;
        
        RenderSettings.fog = true;
     
        clip.PlayOneShot(EntrandoAgua);
        souslamer.SetActive(true);
        RenderSettings.fogDensity = 0.1f;
        RenderSettings.fogStartDistance = 0.0f;
        cam.backgroundColor = new Color(0, 0.4f, 0.7f, 1);




    }

    











}
