using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DNCManager : MonoBehaviour {
    [Header("Time")]
    [Tooltip("Longueur Journée en Minutes")]
    [SerializeField]
    private float _journerEnMinutes = 15f;
    [SerializeField] private float elapsedTime;
    [SerializeField] private Text clock;
    public int _LeverduSoleil = 6;                                   // heure lever du soleil
    public int _DebutJournee = 10;                                    // heure debut de journée
    public int _DebutCoucherSoleil = 18;                             // heure coucher du soleil
    public int _DebutNuit = 22;

    public float JournerMinutes
    {
        get
        {
            return _journerEnMinutes;
        }

    }
    [SerializeField]
    [Range(0f, 1f)]
    private float _timeOfDay;
    public float TimeOfDay
    {
        get
        {
            return _timeOfDay;
        }
    }
    [SerializeField]
    private int _DayNomber = 0;
    public float DayNomber
    {
        get
        {
            return _DayNomber;
        }
    }
    private float _TimeScale = 100f;
    [SerializeField] private AnimationCurve timeCurve;
    [SerializeField] private float timeCurveNormalization;
    [Header("Stars Emit")]
    [SerializeField]
    private GameObject stars;
    [SerializeField] private ParticleSystem.EmissionModule etoiles;
    [SerializeField] private ParticleSystem psStars;
    [Header("SOLEIL")]
    [SerializeField] private Transform AxeRotation;
    [SerializeField] private Light sun;
    [SerializeField] private float intensitySun;
    [SerializeField] private float BaseSunIntensity;
    [SerializeField] private float SunVariation = 2f;
    [SerializeField] private Gradient sunColor;
    [Header("Saison Rotation")]
    [SerializeField] private Transform saisonRotation;
    [SerializeField]
    [Range(-45f, 45f)]
    private float maxSaisonTilt;
    [Header("Modules Base")]
   
    [SerializeField]
    private List<DNBaseControl> modulList = new List<DNBaseControl>();
    public DayPhases _dayPhases;
    public enum DayPhases
    {
        Matin,
        Journee,
        Soiree,
        Nuit

    }
    private void Awake()
    {
        _dayPhases = DayPhases.Nuit;
        
    }

    void Start () {
        NormaleTimeCurve();
        StartCoroutine(JoursNuitstateMachine());
        etoiles = psStars.emission;
        etoiles.enabled = true;
        stars.SetActive(true);
       
    }
	
	
	void Update () {

        UpdateTime();
        UpdateTimeScale();
        RotationSun();
        SunParameters();
        UpdatesModules();
        UpdateClock();
        if(TimeOfDay > 0.8f)
        {
            Nuit();
        }
        if(TimeOfDay > 0.24f && TimeOfDay < 0.45f)
        {
            Matin();
        }
        if(TimeOfDay > 0.45f && TimeOfDay < 0.7f)
        {
            Journee();
        }
        if(TimeOfDay > 0.7f && TimeOfDay < 0.8f)
        {
            Soiree();
        }

	}
   

    public void NormaleTimeCurve()
    {
        float stepSize = 0.01f;
        int numberSteps = Mathf.FloorToInt(1f / stepSize);
        float curveTotal = 0;

        for (int i = 0; i < numberSteps; i++)
        {
            curveTotal += timeCurve.Evaluate(i * stepSize);
        }

        timeCurveNormalization = curveTotal / numberSteps;
    }


    private void UpdateTime()
    {
        
        _TimeScale = 24 / (_journerEnMinutes / 60);
        _TimeScale *= timeCurve.Evaluate(elapsedTime/(JournerMinutes *60));
        _TimeScale /= timeCurveNormalization;
    }
    private void UpdateTimeScale()
    {
        elapsedTime += Time.deltaTime;
        _timeOfDay += Time.deltaTime * _TimeScale / 86400; // secondes dans une journée

        if (_timeOfDay > 1)
        {
            elapsedTime = 0;
            _DayNomber++;
            _timeOfDay -= 1;
        }
    }

    public void UpdateClock()
    {
        float time = elapsedTime / (JournerMinutes * 60);

        float hour = Mathf.FloorToInt(time * 24);

        float minutes = Mathf.FloorToInt(((time * 24) - hour) * 60);

        string hourstring;
        string minutesString;

        if( hour < 10)
        {
            hourstring = "0" + hour.ToString();
        }
        else
        {
            hourstring = hour.ToString();
        }

        if (minutes < 10)
        {
            minutesString = "0" + minutes.ToString();
        }
        else
        {
            minutesString = minutes.ToString();
        }

        clock.text = hourstring + " : " + minutesString;
    }


    public void RotationSun()
    {
        float sunAngle = TimeOfDay * 360f;
        AxeRotation.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, sunAngle));

        float saisonAngle = -maxSaisonTilt * Mathf.Cos(DayNomber / 100f * 2f * Mathf.PI);

        saisonRotation.transform.localRotation = Quaternion.Euler(new Vector3(saisonAngle, 0f, 0f));
    }

    public void SunParameters()
    {
        intensitySun = Vector3.Dot(sun.transform.forward, Vector3.down);
        intensitySun = Mathf.Clamp01(intensitySun);

        sun.intensity = intensitySun * SunVariation + BaseSunIntensity;
        if (TimeOfDay <= 0.2f || TimeOfDay >0.8f)
        {
            BaseSunIntensity = 0.2f;
        }
        else
        {
            BaseSunIntensity = 0.7f;
        }

        sun.color = sunColor.Evaluate(intensitySun);
    }

    public void AddModule(DNBaseControl module)
    {
        modulList.Add(module);
    }
    public void RemoveModule(DNBaseControl module)
    {
        modulList.Remove(module);
    }


    private void UpdatesModules()
    {
        foreach (DNBaseControl module in modulList)
        {
            module.UpdateModul(intensitySun);
        }
    }


    IEnumerator JoursNuitstateMachine()                                // switch les Phases de jours et nuit
    {
        while (true)
        {
            switch (_dayPhases)
            {
                case DayPhases.Matin:
                   ;
                    break;
                case DayPhases.Journee:
                   
                    break;
                case DayPhases.Soiree:
                   
                    break;
                case DayPhases.Nuit:
                   
                    break;

            }
            yield return null;
        }

    }


    void Matin()
    {
        _dayPhases = DayPhases.Matin;
        RenderSettings.fog = false;
        etoiles.enabled = false;
        stars.SetActive(false);
        
    }

    void Journee()
    {

        _dayPhases = DayPhases.Journee;
        RenderSettings.fog = false;
    }

    void Soiree()
    {


        _dayPhases = DayPhases.Soiree;

        RenderSettings.fog = false;
    }

    void Nuit()
    {
        //RenderSettings.fog = true;
        //RenderSettings.fogDensity = 0.01f;
        //RenderSettings.fogColor = Color.grey;
        //RenderSettings.fogMode = FogMode.Exponential;
     
        _dayPhases = DayPhases.Nuit;

        etoiles.enabled = true;
        stars.SetActive(true);





    }
}
