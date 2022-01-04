using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using BlackPearl;
[RequireComponent(typeof(AudioSource))]



public class MeteoAlleatoire : MonoBehaviour
{

    public  AudioSource son;
    public Transform _player;
    public Transform _Meteo;
    public float hauteurMeteo = 10f;
    DayNightControlle cyclejournuit;
    TempController temperatures;
    VitalState playstate;
    public  Light foudre;
    public float timer;
    public float mintime;
    public float maxtime;


    [Header("Météo")]
    public bool statesoleil;
    public bool stateneige;
    public bool statetempete;
    public bool statepluie;
    public bool stateorage;
    public bool statenuage;

    [Header("StateMeteo")]
    public GameObject soleil;
    public GameObject pluie;
    public GameObject neige;
    public GameObject orage;
    public GameObject tempete;

    [SerializeField]private ParticleSystem PSnuagegris;
 [SerializeField]private ParticleSystem PSneige;
 [SerializeField]private ParticleSystem PSnuageblanc;
 [SerializeField]private ParticleSystem PSpluie;
 [SerializeField]private ParticleSystem PSfoudre;
 [SerializeField]private ParticleSystem PSpluieForte;
 [SerializeField]private GameObject goneige;
 [SerializeField]private GameObject gonuageblanc;
 [SerializeField]private GameObject gopluie;
 [SerializeField]private GameObject goMafoudre;
 [SerializeField]private GameObject gopluieForte;


    

    private ParticleSystem.EmissionModule emneige;
    private ParticleSystem.EmissionModule emnuageblanc;
    private ParticleSystem.EmissionModule empluie;
    private ParticleSystem.EmissionModule emfoudre;
    private ParticleSystem.EmissionModule emnuagegris;
    private ParticleSystem.EmissionModule empluieforte;


    public float AudioFadeTime = 0.25f;
    public AudioClip soleilaudio;
    public AudioClip neigeaudio;
    public AudioClip orageaudio;
    public AudioClip pluieaudio;
    public AudioClip briseaudio;
    public AudioClip tempeteaudio;


   

    
    

    
    void Start()
    {
        son = GetComponent<AudioSource>();
        playstate = GetComponent<VitalState>();
        GameObject _playergameobject = GameObject.FindGameObjectWithTag("Player");
        _player = _playergameobject.transform;
        GameObject meteogameobject = GameObject.FindGameObjectWithTag("meteo");
        _Meteo = meteogameobject.transform;
        cyclejournuit = GetComponent<DayNightControlle>();
        
        temperatures = GetComponent<TempController>();

       

        emneige = PSneige.emission;
        emnuageblanc = PSnuageblanc.emission;
        empluie = PSpluie.emission;
        emnuagegris = PSnuagegris.emission;
        empluieforte = PSpluieForte.emission;
        emfoudre = PSfoudre.emission;
        emneige.enabled = false;
        emnuageblanc.enabled = false;
        empluie.enabled = false;
        empluieforte.enabled = false;
        emnuagegris.enabled = false;
        emfoudre.enabled = false;
        foudre.enabled = false;
        goMafoudre.SetActive(false);
        goneige.SetActive(false);
        gonuageblanc.SetActive(false);
        gopluie.SetActive(false);
        gopluieForte.SetActive(false);
        Soleil();
    }


    void Update()
    {
        _Meteo.transform.position = new Vector3(_player.position.x, _player.position.y + hauteurMeteo, _player.position.z);

        if (statesoleil == true)
        {
            goMafoudre.SetActive(false);
            goneige.SetActive(false);
            gonuageblanc.SetActive(false);
            gopluie.SetActive(false);
            gopluieForte.SetActive(false);
            soleil.SetActive(true);
            pluie.SetActive(false);
            neige.SetActive(false);
            orage.SetActive(false);
            tempete.SetActive(false);
            Soleil();
        }
        if (statepluie == true)
        {
            goMafoudre.SetActive(false);
            goneige.SetActive(false);
            gonuageblanc.SetActive(false);
            gopluie.SetActive(true);
            gopluieForte.SetActive(false);
            soleil.SetActive(false);
            pluie.SetActive(true);
            neige.SetActive(false);
            orage.SetActive(false);
            tempete.SetActive(false);
            Pluie();
        }
        if (stateneige == true)
        {
            soleil.SetActive(false);
            pluie.SetActive(false);
            neige.SetActive(true);
            orage.SetActive(false);
            tempete.SetActive(false);
            goMafoudre.SetActive(false);
            goneige.SetActive(true);
            gonuageblanc.SetActive(false);
            gopluie.SetActive(false);
            gopluieForte.SetActive(false);
            Neige();
        }
        if (stateorage == true)
        {
            goMafoudre.SetActive(true);
            goneige.SetActive(false);
            gonuageblanc.SetActive(false);
            gopluie.SetActive(false);
            gopluieForte.SetActive(true);
            soleil.SetActive(false);
            pluie.SetActive(false);
            neige.SetActive(false);
            orage.SetActive(true);
            tempete.SetActive(false);
            Orage();
        }
        if (statetempete == true)
        {
            goMafoudre.SetActive(true);
            goneige.SetActive(false);
            gonuageblanc.SetActive(false);
            gopluie.SetActive(false);
            gopluieForte.SetActive(true);
            soleil.SetActive(false);
            pluie.SetActive(false);
            neige.SetActive(false);
            orage.SetActive(false);
            tempete.SetActive(true);
            Tempete();
        }
    }
    
    IEnumerator Flash()
    {
        if (stateorage == true)
        {

            foudre.intensity = 0.0f;
            yield return new WaitForSeconds(5f);
            foudre.intensity = 0.2f;
            yield return new WaitForSeconds(timer);
            foudre.intensity = 2f;
            yield return new WaitForSeconds(10f);
            foudre.intensity = 0.0f;
            yield return new WaitForSeconds(timer);
            foudre.intensity = 0.5f;
            yield return new WaitForSeconds(timer);
            foudre.intensity = 0.0f;
            yield return new WaitForSeconds(5f);
            foudre.intensity = 0.3f;
            yield return new WaitForSeconds(1f);
            foudre.intensity = 1.2f;
            yield return new WaitForSeconds(5f);
            foudre.intensity = 0.0f;
            yield return new WaitForSeconds(10f);
            foudre.intensity = 1.8f;
            yield return new WaitForSeconds(10f);
            foudre.intensity = 0.0f;
            if (stateorage == true) { }
            StartCoroutine(Flash());
            
            timer = Random.Range(mintime, maxtime);

        }
        else if (stateorage != true)
        {
            StopCoroutine(Flash());
        }

    }

    void Soleil()
    {
       
        statesoleil = true;
        StopCoroutine(Flash());
        foudre.enabled = false;
        emneige.enabled = false;
        emnuageblanc.enabled = false;
        empluie.enabled = false;
        empluieforte.enabled = false;
        emnuagegris.enabled = false;
        emfoudre.enabled = false;
        
        if (son.volume > 0
            && son.clip != soleilaudio)
            son.volume -= Time.deltaTime * AudioFadeTime;
        
        if (son.volume == 0)
        {
         son.Stop();
         son.clip = soleilaudio;
         son.loop = true;
         son.Play();
        }

        if (son.volume <= 0.3
        && son.clip == soleilaudio)
           son.volume += Time.deltaTime * AudioFadeTime;


       
       
    }

    void Neige()
    {
        foudre.enabled = false;
        emneige.enabled = true;
     
        emnuageblanc.enabled = false;
        empluie.enabled = false;
        empluieforte.enabled = false;
        emnuagegris.enabled = false;
        emfoudre.enabled = false;


        stateneige = true;

        StopCoroutine(Flash());

        if (son.volume > 0
         && son.clip != neigeaudio)
            son.volume -= Time.deltaTime * AudioFadeTime;
            
        if (son.volume == 0)
        {   
            son.Stop();
            son.clip = neigeaudio;
            son.loop = true;
            son.Play();
        }   
        if (son.volume < 1
        &&  son.clip == neigeaudio)
            son.volume += Time.deltaTime * AudioFadeTime;



    }
    void Orage()
    {
        
        emfoudre.enabled = true;
        emneige.enabled = false;
        emnuageblanc.enabled = false;
        empluie.enabled = false;
        empluieforte.enabled = true;
        emnuagegris.enabled = false;

        stateorage = true;
        foudre.enabled = true;
        StartCoroutine(Flash());

        if (GetComponent<AudioSource>().volume > 0
            && GetComponent<AudioSource>().clip != orageaudio)
            GetComponent<AudioSource>().volume -= Time.deltaTime * AudioFadeTime;


        if (GetComponent<AudioSource>().volume == 0)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = orageaudio;
            GetComponent<AudioSource>().clip = pluieaudio;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();

        }

        if (GetComponent<AudioSource>().volume < 1
            && GetComponent<AudioSource>().clip == orageaudio
            && GetComponent<AudioSource>().clip == pluieaudio)
            GetComponent<AudioSource>().volume += Time.deltaTime * AudioFadeTime;
      


    }


    void Pluie()
    {
        
        statepluie = true;
        empluie.enabled = true;
        emneige.enabled = false;
        emnuageblanc.enabled = false;

        empluieforte.enabled = false;
        emnuagegris.enabled = false;
        emfoudre.enabled = false;
        StopCoroutine(Flash());

        foudre.enabled = false;
        if (GetComponent<AudioSource>().volume > 0
            && GetComponent<AudioSource>().clip != pluieaudio)
            GetComponent<AudioSource>().volume -= Time.deltaTime * AudioFadeTime;
        
        if (GetComponent<AudioSource>().volume == 0)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = pluieaudio;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();

        }
        if (GetComponent<AudioSource>().volume < 1
            && GetComponent<AudioSource>().clip == pluieaudio)
            GetComponent<AudioSource>().volume += Time.deltaTime * AudioFadeTime;
       
        
    }

    void Nuageux()
    {
       
        foudre.enabled = false;

        StopCoroutine(Flash());

        statenuage = true;
        
        if (GetComponent<AudioSource>().volume > 0
            && GetComponent<AudioSource>().clip != briseaudio)
            GetComponent<AudioSource>().volume -= Time.deltaTime * AudioFadeTime;


        if (GetComponent<AudioSource>().volume == 0)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = briseaudio;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();

        }
        if (GetComponent<AudioSource>().volume < 1
            && GetComponent<AudioSource>().clip == briseaudio)
            GetComponent<AudioSource>().volume += Time.deltaTime * AudioFadeTime;
     
        
    }

    void Tempete()
    {
        foudre.enabled = true;
        StartCoroutine(Flash());

        empluieforte.enabled = false;
        emneige.enabled = false;
        emnuageblanc.enabled = false;
        empluie.enabled = false;

        emnuagegris.enabled = false;
        emfoudre.enabled = true;

        statetempete = true;


        if (GetComponent<AudioSource>().volume > 0
            && GetComponent<AudioSource>().clip != tempeteaudio)
            GetComponent<AudioSource>().volume -= Time.deltaTime * AudioFadeTime;


        if (GetComponent<AudioSource>().volume == 0)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = tempeteaudio;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();

        }
        if (GetComponent<AudioSource>().volume < 1
            && GetComponent<AudioSource>().clip == tempeteaudio)
            GetComponent<AudioSource>().volume += Time.deltaTime * AudioFadeTime;
     

    }
}
    
   












    


