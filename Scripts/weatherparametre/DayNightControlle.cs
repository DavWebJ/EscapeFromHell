using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DayNightControlle : MonoBehaviour {
    TempController temppourc;
    MeteoAlleatoire met;
    public bool ete;
    public bool automne;
    public bool hiver;
    public bool printemps;
    public float DayCurrentSaison;
    public Text saison;
    
    
   
    
    
   
    // Skybox sky;
    // blend value of skybox using SkyBoxBlend Shader in render settings range 0-1  
    // private float SkyboxBlendFactor = 0.0f;

  


    public SaisonPhase currentSaison;
    
    /// number of seconds in a day  
    public float dayCycleLength = 1440;

    /// current time in game time (0 - dayCycleLength).  
    public float currentCycleTime = 0;

    /// number of hours per day.  
    public float hoursPerDay;
   // nombre de jours avant saison
    public float dayBeforSaison;

    /// The rotation pivot of Sun  
  //  public Transform rotation;

    /// current day phase  
    public DayPhase currentPhase;

    /// Dawn occurs at currentCycleTime = 0.0f, so this offsets the WorldHour time to make  
    /// dawn occur at a specified hour. A value of 3 results in a 5am dawn for a 24 hour world clock.  
    public float dawnTimeOffset;

    /// calculated hour of the day, based on the hoursPerDay setting.  
    public int worldTimeHour;

    /// calculated minutes of the day, based on the hoursPerDay setting.  
    public int minutes;
    private float timePerHour;

    /// The scene ambient color used for full daylight.  
    public Color fullLight = new Color(253.0f / 255.0f, 248.0f / 255.0f, 223.0f / 255.0f);

    /// The scene ambient color used for full night.  
    public Color fullDark = new Color(32.0f / 255.0f, 28.0f / 255.0f, 46.0f / 255.0f);

    /// The scene fog color to use at dawn and dusk.  
   // public Color dawnDuskFog = new Color(133.0f / 255.0f, 124.0f / 255.0f, 102.0f / 255.0f);

    /// The scene fog color to use during the day.  
   //public Color dayFog = new Color(180.0f / 255.0f, 208.0f / 255.0f, 209.0f / 255.0f);

    /// The scene fog color to use at night.  
   // public Color nightFog = new Color(12.0f / 255.0f, 15.0f / 255.0f, 91.0f / 255.0f);

    /// The calculated time at which dawn occurs based on 1/4 of dayCycleLength.  
    private float dawnTime;

    /// The calculated time at which day occurs based on 1/4 of dayCycleLength.  
    private float dayTime;

    /// The calculated time at which dusk occurs based on 1/4 of dayCycleLength.  
    private float duskTime;

    /// The calculated time at which night occurs based on 1/4 of dayCycleLength.  
    private float nightTime;

    /// One quarter the value of dayCycleLength.  
    private float quarterDay;
    private float halfquarterDay;

    /// The specified intensity of the directional light, if one exists. This value will be  
    /// faded to 0 during dusk, and faded from 0 back to this value during dawn.  
    public static float lightIntensity = 0.75f;
    public Light sun;
    
    /// Initializes working variables and performs starting calculations.  
    void Initialize()
    {
        quarterDay = dayCycleLength * 0.25f;
        halfquarterDay = dayCycleLength * 0.125f;
        dawnTime = 0.0f;
        dayTime = dawnTime + halfquarterDay;
        duskTime = dayTime + quarterDay + halfquarterDay;
        nightTime = duskTime + halfquarterDay;
        timePerHour = dayCycleLength / hoursPerDay;
        DayCurrentSaison = hoursPerDay * dayBeforSaison;
        if (sun != null)
        { lightIntensity = sun.intensity; }
    }

    /// Sets the script control fields to reasonable default values for an acceptable day/night cycle effect.  
    void Reset()
    {
        dayBeforSaison = 360.0f;
        dayCycleLength = 120.0f;
        hoursPerDay = 24.0f;
        dawnTimeOffset = 3.0f;
       fullDark = new Color(32.0f / 255.0f, 28.0f / 255.0f, 46.0f / 255.0f);
        fullLight = new Color(253.0f / 255.0f, 248.0f / 255.0f, 223.0f / 255.0f);
     //  dawnDuskFog = new Color(133.0f / 255.0f, 124.0f / 255.0f, 102.0f / 255.0f);
      //  dayFog = new Color(180.0f / 255.0f, 208.0f / 255.0f, 209.0f / 255.0f);
       // nightFog = new Color(12.0f / 255.0f, 15.0f / 255.0f, 91.0f / 255.0f);
    }

    // Use this for initialization  
    void Start()
    {
        lightIntensity = sun.intensity;
        
        met = GameObject.Find("Directional Light").GetComponent<MeteoAlleatoire>();
        temppourc = GetComponent<TempController>();
        sun = GetComponent<Light>();
        Initialize();
    }

    void OnGUI()
    {
        string jam = worldTimeHour.ToString();
        string menit = minutes.ToString();
        if (worldTimeHour < 10)
        {
            jam = "0" + worldTimeHour;
        }
        if (minutes < 10)
        {
            menit = "0" + minutes;
        }
       
      
        GUI.Button(new Rect(1200, 15, 100,50), currentPhase.ToString() + " : " + jam + ":" + menit);
    }

    
    void Update()
    {

        
       // RenderSettings.fog = true;
        // Rudementary phase-check algorithm:  
        if (currentCycleTime > nightTime && currentPhase == DayPhase.Soiree)
        {
            SetNight();
        }
        else if (currentCycleTime > duskTime && currentPhase == DayPhase.Journee)
        {
            SetDusk();
        }
        else if (currentCycleTime > dayTime && currentPhase == DayPhase.Matin)
        {
            SetDay();
        }
        else if (currentCycleTime > dawnTime && currentCycleTime < dayTime && currentPhase == DayPhase.Nuit)
        {
            SetDawn();
        }

        // Perform standard updates:  
        UpdateWorldTime();
        UpdateDaylight();
    
        UpdateFog();
        UpdateSaison();
      
        // Update the current cycle time:  
        currentCycleTime += Time.deltaTime;
        currentCycleTime = currentCycleTime % dayCycleLength;
        DayCurrentSaison -= Time.deltaTime;
        

    }

    /// Sets the currentPhase to Dawn, turning on the directional light, if any.  
    public void SetDawn()
    {
        if (sun != null)
        { sun.enabled = true; }
        currentPhase = DayPhase.Matin;
    }

    /// Sets the currentPhase to Day, ensuring full day color ambient light, and full  
    /// directional light intensity, if any.  
    public void SetDay()
    {
       RenderSettings.ambientLight = fullLight;
       if (sun != null)
       { sun.intensity = lightIntensity; }
        currentPhase = DayPhase.Journee;
    }

    /// Sets the currentPhase to Dusk.  
    public void SetDusk()
    {
        currentPhase = DayPhase.Soiree;
    }

    /// Sets the currentPhase to Night, ensuring full night color ambient light, and  
    /// turning off the directional light, if any.  
    public void SetNight()
    {
        RenderSettings.ambientLight = fullDark;
        if (sun != null)
        { sun.enabled = false; }
        currentPhase = DayPhase.Nuit;
    }

    /// If the currentPhase is dawn or dusk, this method adjusts the ambient light color and direcitonal  
    /// light intensity (if any) to a percentage of full dark or full light as appropriate. Regardless  
    /// of currentPhase, the method also rotates the transform of this component, thereby rotating the  
    /// directional light, if any.  
    private void UpdateDaylight()
    {
        if (currentPhase == DayPhase.Matin)
        {
            float relativeTime = currentCycleTime - dawnTime;
            RenderSettings.ambientLight = Color.Lerp(fullDark, fullLight, relativeTime / halfquarterDay);
            if (sun != null)
            { sun.intensity = lightIntensity * (relativeTime / halfquarterDay); }
        }
        else if (currentPhase == DayPhase.Soiree)
        {
            float relativeTime = currentCycleTime - duskTime;
            RenderSettings.ambientLight = Color.Lerp(fullLight, fullDark, relativeTime / halfquarterDay);
            if (sun != null)
            { sun.intensity = lightIntensity * ((halfquarterDay - relativeTime) / halfquarterDay); }
        }

        transform.Rotate(Vector3.up * ((Time.deltaTime / dayCycleLength) * 360.0f), Space.Self);  
       // transform.RotateAround(rotation.position, Vector3.forward, ((Time.deltaTime / dayCycleLength) * 360.0f));
    }

    /// Interpolates the fog color between the specified phase colors during each phase's transition.  
    /// eg. From DawnDusk to Day, Day to DawnDusk, DawnDusk to Night, and Night to DawnDusk  
    private void UpdateFog()
    {
        if (currentPhase == DayPhase.Matin)
        {
            
            float relativeTime = currentCycleTime - dawnTime;
           // RenderSettings.fogColor = Color.Lerp(dawnDuskFog, dayFog, relativeTime / halfquarterDay);
        }
        else if (currentPhase == DayPhase.Journee)
        {
            float relativeTime = currentCycleTime - dayTime;
           // RenderSettings.fogColor = Color.Lerp(dayFog, dawnDuskFog, relativeTime / (quarterDay + halfquarterDay));
        }
        else if (currentPhase == DayPhase.Soiree)
        {
            float relativeTime = currentCycleTime - duskTime;
          //  RenderSettings.fogColor = Color.Lerp(dawnDuskFog, nightFog, relativeTime / halfquarterDay);
        }
        else if (currentPhase == DayPhase.Nuit)
        {
            float relativeTime = currentCycleTime - nightTime;
          //  RenderSettings.fogColor = Color.Lerp(nightFog, dawnDuskFog, relativeTime / (quarterDay + halfquarterDay));
        }
    }

    /// Updates the World-time hour based on the current time of day.  
    private void UpdateWorldTime()
    {
        worldTimeHour = (int)((Mathf.Ceil((currentCycleTime / dayCycleLength) * hoursPerDay) + dawnTimeOffset) % hoursPerDay) + 1;
        minutes = (int)(Mathf.Ceil((currentCycleTime * (60 / timePerHour)) % 60));
       
    }

    public enum DayPhase
    {
        Nuit = 0,
        Matin = 1,
        Journee = 2,
        Soiree = 3
    }

    public enum SaisonPhase
    {
        printemps = 0,
        ete = 1,
        automne = 2,
        hiver = 3

    }
    
   

    void UpdateSaison()
    {
        
        saison.text = currentSaison.ToString("");
        if(DayCurrentSaison <6480)
        {
            Ete();
        }
        if(DayCurrentSaison <= 4320)
        {
            Automne();
        }

        if(DayCurrentSaison <= 2160)
        {
            Hiver();
        }
        if(DayCurrentSaison <= 1)
        {
            DayCurrentSaison = hoursPerDay * dayBeforSaison;
            Printemps();
        }
        if(currentSaison == SaisonPhase.printemps)
        {
            printemps = true;
            ete = false;
            automne = false;
            hiver = false;
        }
        else if(currentSaison == SaisonPhase.ete)
        {
            ete = true;
            printemps = false;
            automne = false;
            hiver = false;
        }
        else if(currentSaison == SaisonPhase.automne)
        {
            automne = true;
            ete = false;
            hiver = false;
            printemps = false;

        }
        else if(currentSaison == SaisonPhase.hiver)
        {
            hiver = true;
            printemps = false;
            ete = false;
            automne = false;
        }
    }
    
    void Printemps()
    {
        currentSaison = SaisonPhase.printemps;
        printemps = true;
    }

    void Ete()
    {
        currentSaison = SaisonPhase.ete;
        ete = true;
    }

    void Automne()
    {
        currentSaison = SaisonPhase.automne;
    }

    void Hiver()
    {
        currentSaison = SaisonPhase.hiver;
    }
   
   



}
