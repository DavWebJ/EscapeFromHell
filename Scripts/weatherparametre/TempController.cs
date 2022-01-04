using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BlackPearl;


public class TempController : MonoBehaviour
{

    // MeteoAlleatoire _meteoalleatoire;
    public DNCManager daynight;
    public VitalState _statev;
    [Header("pourcentage d'humidité")]
    public float currentPourcentage;
   // public Text NombrePourcentage;
    // public WeatherManager meteo;
    public GameObject image_MeteoSoleil;
    public GameObject image_ouragan;
    public GameObject orage;
    public float pourcMinjourneeEte;                    // temperature minimum journée été
    public float pourcMaxjourneeEte;
    public float TimerChangerPourcentage = 0f;
    public float ResetPourcentageSwitch = 250f;

    [Header("Temperature Settings")]
    public float TempMinmatnuitEte;                    // temperature minimum nuit été
    public float TempMaxmatnuitEte;                    // temperature maximum nuit été
    public float TempMinjourneeEte;                    // temperature minimum journée été
    public float TempMaxjourneeEte;                    // temperature maximum journée été
    public float TempMinmatnuitHiver;
    public float TempMaxmatnuitHiver;
    public float TempMinjourneeHiver;
    public float TempMaxjourneeHiver;
    public float VitesseTemp = 5f;
    [SerializeField] public Image tempFil;
    
    public float TempHiver = 18f;                             // max temperture froide
    public float TempEte = 40f;                             // max temperature chaude
    public float currenttemp;                       // temperature actuelle
   
    public float TimerChangerTemp = 0f;
    public float ResetTempSwitch = 30f;                      // timer decompte avant de changer de temperature
    public Text tempNumpber;



    [Header("StateFaim/Soif")]
    public float Maxtemfaim;      // max temperature froide avant d'avoir faim
    public float Maxtempsoif;    // max temperature chaude avant d'avoir soif                            
    public bool isSoif;
    public bool isFaim;





    void Start()
    {

        // meteo = GameObject.FindGameObjectWithTag("DayNightManager").GetComponent<WeatherManager>();
        _statev = GameObject.FindGameObjectWithTag("Player").GetComponent<VitalState>();
        //_meteoalleatoire = GameObject.FindGameObjectWithTag("Directional Light").GetComponent<MeteoAlleatoire>();
         daynight = GetComponent<DNCManager>();

    }


    void Update()
    {

         PourcentageSwitch();
        TimerSwitch();



        //if (currenttemp <= TempHiver)                                            // change la couleur du thermométre pour le froid
        //{
        //    tempNumpber.color = Color.cyan;

        //}
        //else if (currenttemp >= TempEte - 39)                                     // change la couleur du thermométre pour le chaud
        //{
        //    tempNumpber.color = Color.red;


        //}

        //if (currenttemp >= Maxtempsoif)
        //{

        //}
        //else
        //{

        //}
        //if(currenttemp <= Maxtemfaim)
        //{

        //}
        //else
        //{

        //}
    }

    void TimerSwitch()                                             // methode timer pour décompte avant de changer de temperature 
    {


        TimerChangerTemp -= Time.deltaTime;
        if (TimerChangerTemp < 0)
            TimerChangerTemp = 0;
        if (TimerChangerTemp > 0)
            return;
        if (TimerChangerTemp == 0)

            UpdateTemp();


        TimerChangerTemp = ResetTempSwitch;
    }


    void PourcentageSwitch()                                             // methode timer pour décompte avant de changer de temperature 
    {


        TimerChangerPourcentage -= Time.deltaTime;
        if (TimerChangerPourcentage < 0)
            TimerChangerPourcentage = 0;
        if (TimerChangerPourcentage > 0)
            return;
        if (TimerChangerPourcentage == 0)

             Updatepourcent();

            TimerChangerPourcentage = ResetPourcentageSwitch;
    }

    void Updatepourcent()
    {
        ChangePourcentage();

       // NombrePourcentage.text = currentPourcentage.ToString("00");
    }


    void UpdateTemp()
    {

        ChangeTemp();


        tempNumpber.text = currenttemp.ToString("00");                       // convertie en texte ecran la temperature actuelle

    }


    public void ChangeTemp()                                                        // méthode pour température au hasard pour chaque saison
    {
       

        TempETE();




    }
   

    void TempETE()
    {
        if (daynight._dayPhases == DNCManager.DayPhases.Matin || daynight._dayPhases == DNCManager.DayPhases.Nuit)
        {
            currenttemp = Random.Range(TempMinmatnuitEte, TempMaxmatnuitEte);
            tempFil.fillAmount = currenttemp / TempMaxmatnuitEte;
            currenttemp += VitesseTemp * Time.deltaTime;
            if (currenttemp >= TempMaxmatnuitEte)
            {
                currenttemp = TempMaxmatnuitEte;
            }

            if (currenttemp <= TempMinmatnuitEte)
            {
                currenttemp = TempMinmatnuitEte;
            }
        }
        else
        {
            if(daynight._dayPhases == DNCManager.DayPhases.Journee || daynight._dayPhases == DNCManager.DayPhases.Soiree)
            {
                currenttemp = Random.Range(TempMinjourneeEte, TempMaxjourneeEte);
                tempFil.fillAmount = currenttemp / TempMaxjourneeEte;
                currenttemp += VitesseTemp * Time.deltaTime;
                if (currenttemp >= TempMaxjourneeEte)
                {
                    currenttemp = TempMaxjourneeEte;
                }

                if (currenttemp <= TempMinjourneeEte)
                {
                    currenttemp = TempMinjourneeEte;
                }
            }
           


        }
       
    }

  

    public void ChangePourcentage()
    {
        HumiditerETE();
    }






    void HumiditerETE()
    {
        currentPourcentage = Random.Range(pourcMinjourneeEte, pourcMaxjourneeEte);
        if (currentPourcentage > 60)
        {
            // meteo.state = WeatherManager.StateWeather.storm;
            orage.SetActive(true);
            image_MeteoSoleil.SetActive(false);
            image_ouragan.SetActive(false);
            // meteo.Invoke("SpawnStorm", VitesseTemp);
          //  meteo.Invoke("SpawnHuricane", VitesseTemp);

        }
        else
        {
            if (currentPourcentage < 40)
            {
                // meteo.state = WeatherManager.StateWeather.normal;
                orage.SetActive(false);
                image_MeteoSoleil.SetActive(true);
                image_ouragan.SetActive(false);
                // meteo.Invoke("SpawnNormalWeather", VitesseTemp);
               // meteo.Invoke("SpawnHuricane", VitesseTemp);
            }
            else if (currentPourcentage > 40 && currentPourcentage < 60)
            {
                // meteo.state = WeatherManager.StateWeather.Huricane;
                orage.SetActive(false);
                image_MeteoSoleil.SetActive(false);
                image_ouragan.SetActive(true);
              //  meteo.Invoke("SpawnHuricane", VitesseTemp);
            }
        }

       
    }



}

    

