using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JoursNuitCycle : MonoBehaviour {
    private TempController changetemperature;
    private MeteoAlleatoire meteo;
    public Transform pivotSoleil;
    public int centreDuMonde = 1000;                                 // centre de la map (moitier du terrain en valeur)

    public float dayAtmosphereThickness = 0.5f;
    public float nightAtmosphereThickness = 1.3f;
    public float dayExposure = 2;
    public float nightExposure = 1.1f;

    public Light _SUN;
    public int _Jours;
    public int _Heures;
    public int _Minutes;
    public int _Secondes;
    public float _DecompteTemps;

    public float JourneeEnTemps;
    public float RotationEnTemps;

    public int _LeverduSoleil = 6;                                   // heure lever du soleil
    public int _DebutJournee = 10;                                    // heure debut de journée
    public int _DebutCoucherSoleil = 18;                             // heure coucher du soleil
    public int _DebutNuit = 22;                                      // heure Nuit
   

    

    public float Vitessejours = 0.05f;                               //vitesse journée
    public float _Matinintensiter = 0.6f;                            // intensitée lumiere lever du soleil
    public float _JourneeIntensiter = 1.7f;                            // intensitée lumiere journéé
    public float _SoireeIntensiter = 0.4f;                           // intensitée lumiere coucher du soleil
    public float _NuitIntensiter = 0.0f;                             // intensitée lumiere nuit
    public float Timemultiplieure = 20f;

    public int _positionEcranHorloge = 100;
    public int _positionhauteurHorloge = 20;

    public float vitesseAbianceSkybox = 0.0001f;
    
    public float matinAmbienceIntensiter = 0.6f;
    public float journeeAmbienceIntensiter = 1.2f;
    public float soireeAmbienceIntensiter = 0.3f;
    public float nuitAmbienceIntensiter = 0;
    public float minPoint = -0.2f;
    public float minAmbientPoint = -0.2f;

   public  Skybox sky;
     
    Material skymat;

    public Gradient nightDayColor;


    public DayPhases _dayPhases;
    public enum DayPhases
    {
        Matin,
        Journee,
        Soiree,
        Nuit

    }


     void Awake()
    {
       _dayPhases = DayPhases.Matin;

        _SUN.intensity = nuitAmbienceIntensiter;

        transform.position = new Vector3((centreDuMonde * 2),0,centreDuMonde);
        transform.localEulerAngles = new Vector3(0,-90,0);

        RenderSettings.ambientIntensity = _NuitIntensiter;
        meteo = GetComponent<MeteoAlleatoire>();
        changetemperature = GetComponent<TempController>();
    }  




    void Start () {
        skymat = RenderSettings.skybox;
        _SUN = GetComponent<Light>();
        StartCoroutine(JoursNuitstateMachine());

        _dayPhases = DayPhases.Matin;                                                                  //debute le jeu en nuit (a changer )

        _Heures = 05;                                                     // heure de debut jeu (a changer)
        _Minutes = 59;                                                   // minutes de debut jeu (a changer)
        _DecompteTemps = 59;                                                  // idem pour les secondes

       

        GameObject pivotSoleilGO = GameObject.FindGameObjectWithTag("pivotSoleil");
        pivotSoleil = pivotSoleilGO.transform;
        pivotSoleil.transform.position = new Vector3(centreDuMonde, 0, centreDuMonde);
	}
	
	
	void Update () {

        SecondsCounter();
        RotationSoleilmanager();
        
      

        float tRange = 1 - minPoint;
        float dot = Mathf.Clamp01((Vector3.Dot(_SUN.transform.forward, Vector3.down) - minPoint) / tRange);
        float i = ((_JourneeIntensiter - _NuitIntensiter) * dot) + _NuitIntensiter;

        _SUN.intensity = i;

        tRange = 1 - minAmbientPoint;

        dot = Mathf.Clamp01((Vector3.Dot(_SUN.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
        i = ((journeeAmbienceIntensiter - nuitAmbienceIntensiter) * dot) + nuitAmbienceIntensiter;
        RenderSettings.ambientIntensity = i;

        _SUN.color = nightDayColor.Evaluate(dot);
        RenderSettings.ambientLight = _SUN.color;

       
        i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
        skymat.SetFloat("_AtmosphereThickness", i);
        i = ((dayExposure - nightExposure) * dot) + nightExposure;
        skymat.SetFloat("_Exposure", i);
    }

    IEnumerator JoursNuitstateMachine()                                // switch les Phases de jours et nuit
    {
        while (true)
        {
            switch (_dayPhases)
            {
                case DayPhases.Matin:
                    Matin();
                    break;
                case DayPhases.Journee:
                    Journee();
                    break;
                case DayPhases.Soiree:
                    Soiree();
                    break;
                case DayPhases.Nuit:
                    Nuit();
                    break;

            }
            yield return null;
        }

    }



    void SecondsCounter ()
    {
        
        if (_DecompteTemps == 60)
            _DecompteTemps = 0;

        _DecompteTemps += Time.deltaTime ;

        _Secondes = (int)_DecompteTemps;                       // convertie le float en int

        if (_DecompteTemps < 60)
            return;

        if (_DecompteTemps > 60)
            _DecompteTemps = 60;

        if (_DecompteTemps == 60)
            MinutesCounter();
    }


    void MinutesCounter ()
    {
        
        _Minutes++;

        if(_Minutes == 60)
        {
            HeuresCounter();
            _Minutes = 00;
        }

    }

    void HeuresCounter ()
    {
        
        _Heures ++;

        if (_Heures == 24)
        {
            
            _Heures = 0;
        }

    }

    

    

    void RotationSoleilmanager()
    {
        JourneeEnTemps = _DebutNuit - _LeverduSoleil;
        RotationEnTemps = (JourneeEnTemps / 365) ;
        transform.RotateAround(pivotSoleil.position, Vector3.forward,RotationEnTemps * Time.deltaTime);

    }



    void Matin()
    {
        
        if (_SUN.intensity < _Matinintensiter)
            _SUN.intensity += Vitessejours * Time.deltaTime;

        if (_SUN.intensity > _Matinintensiter)
            _SUN.intensity = _Matinintensiter;

        if (RenderSettings.ambientIntensity < matinAmbienceIntensiter)
            RenderSettings.ambientIntensity += vitesseAbianceSkybox * Time.deltaTime;
        

        if (RenderSettings.ambientIntensity > matinAmbienceIntensiter)
            RenderSettings.ambientIntensity = matinAmbienceIntensiter;

        if (_Heures == _DebutJournee && _Heures < _DebutCoucherSoleil)
        {
            _dayPhases = DayPhases.Journee;
        }
    }

    void Journee()
    {
        
        if (_SUN.intensity < _JourneeIntensiter)
            _SUN.intensity += Vitessejours * Time.deltaTime;

        if (_SUN.intensity > _JourneeIntensiter)
            _SUN.intensity = _JourneeIntensiter;

        if (RenderSettings.ambientIntensity < journeeAmbienceIntensiter)
            RenderSettings.ambientIntensity += vitesseAbianceSkybox * Time.deltaTime;

        if (RenderSettings.ambientIntensity > journeeAmbienceIntensiter)
            RenderSettings.ambientIntensity = journeeAmbienceIntensiter;

        if (_Heures == _DebutCoucherSoleil && _Heures < _DebutNuit)
        {
            _dayPhases = DayPhases.Soiree;
        }
    }

    void Soiree()
    {
        

        if (_SUN.intensity > _SoireeIntensiter)
            _SUN.intensity -= Vitessejours * Time.deltaTime;

        if (_SUN.intensity < _SoireeIntensiter)
            _SUN.intensity = _SoireeIntensiter;

        if (RenderSettings.ambientIntensity > soireeAmbienceIntensiter)
            RenderSettings.ambientIntensity -= vitesseAbianceSkybox * Time.deltaTime;

        if (RenderSettings.ambientIntensity < soireeAmbienceIntensiter)
            RenderSettings.ambientIntensity = soireeAmbienceIntensiter;

        if (_Heures == _DebutNuit)
        {
            _dayPhases = DayPhases.Nuit;
        }
    }

    void Nuit()
    {
        RenderSettings.fog = false;
        

        if (_SUN.intensity > _NuitIntensiter)
            _SUN.intensity -= Vitessejours * Time.deltaTime;

        if (_SUN.intensity < _NuitIntensiter)
            _SUN.intensity = _NuitIntensiter;

        if (RenderSettings.ambientIntensity > nuitAmbienceIntensiter)
            RenderSettings.ambientIntensity -= vitesseAbianceSkybox * Time.deltaTime;

        if (RenderSettings.ambientIntensity < nuitAmbienceIntensiter)
            RenderSettings.ambientIntensity = nuitAmbienceIntensiter;

        if (_Heures == _LeverduSoleil && _Heures < _DebutJournee)
        {
            _dayPhases = DayPhases.Matin;
        }
    }

     void OnGUI()
    {
        GUI.color = Color.red;
       

        
        GUI.Label(new Rect(Screen.width - 120, 30, _positionEcranHorloge, _positionhauteurHorloge), _Heures + ":" + _Minutes + ":" + _Secondes);


       

       
        
    }
}
