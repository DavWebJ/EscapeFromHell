using System.Collections;
using System.Collections.Generic;
using BlackPearl;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public enum crosshair_type
    {
        normal,gun,pickup
    }
    public static HUD instance = null;

    [Header("Colors vitals bar")]
    public Gradient lifeBarColor = new Gradient();
    public Gradient staminaBarColor = new Gradient();
    public Gradient bateryBarColor = new Gradient();

    [Header("UI visual effect")]
    [SerializeField] private Animation fallAnimationEffect = null;
    [SerializeField] private Animation frostAnimationEffect = null;

    [SerializeField] private Transform gridMessage = null;
    [SerializeField] private GameObject prf_Message = null;
    [SerializeField] private GameObject scopedOverlay = null;
    [Header("sound fx")]
    public AudioSource audioSource;
    [SerializeField] private AudioClip hitclip;

    [Header("Crosshair")]
    [SerializeField] private GameObject crosshair_normal;
    [SerializeField] private GameObject crosshair_gun;
    [SerializeField] private GameObject crosshair_pickup;
    public crosshair_type crosshair_Type;

   
    private void Awake() {
    if(instance == null)
        instance = this;
        
    }

    private void Start() {

        audioSource = GetComponent<AudioSource>();
        crosshair_normal = transform.Find("screen effect/Canvas/crosshairnormal").gameObject;
        crosshair_gun = transform.Find("screen effect/Canvas/crosshairgun").gameObject;
        crosshair_pickup = transform.Find("screen effect/Canvas/crosshairpickup").gameObject;
        gridMessage = transform.Find("HUD_Infos/message").transform;
        ChangeCrossHair(crosshair_type.normal);

    }
    
    public void ScreenEffect(string nameEffect)
    {
        switch (nameEffect)
        {
            case"BloodFallDamage":
            fallAnimationEffect.Play();
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(hitclip);
            }
            
            break;
            case"frost":
    
            // frost.settings.Vignette = Mathf.Lerp(frost.settings.Vignette,0.5f,Time.deltaTime / 2);
      
           
            // if(!audioSource.isPlaying)
            // {
            //     audioSource.PlayOneShot(hitclip);
            // }
            break;
            case"defrost":

            // frost.settings.Vignette = 0;
        
            
            
            // if(!audioSource.isPlaying)
            // {
            //     audioSource.PlayOneShot(hitclip);
            // }
            break;
            default:
            break;
        }
    }

    public void setScopedImage(bool activate)
    {

        scopedOverlay.SetActive(activate);
        
    }
    public void SetVisualMessage(bool add,Item item)
    {
        if(item == null)
        return;

        VisualMessage msg = Instantiate(prf_Message,gridMessage).GetComponent<VisualMessage>();
        if(msg != null)
        {
            msg.SendVisualMessage(add,item);
        }
    }
    public void SetVisualMessage(string message,Color color)
    {
        if(message == string.Empty)
        return;

        VisualMessage msg = Instantiate(prf_Message,gridMessage).GetComponent<VisualMessage>();
        if(msg != null)
        {
            msg.SendVisualMessage(message,color);
        }
    }

    public void ChangeCrossHair(crosshair_type crosshairtype)
    {
        switch (crosshairtype)
        {
            case crosshair_type.normal:
            crosshair_gun.SetActive(false);
            crosshair_pickup.SetActive(false);
            crosshair_normal.SetActive(true);
            break;
            case crosshair_type.gun:
            crosshair_gun.SetActive(true);
            crosshair_pickup.SetActive(false);
            crosshair_normal.SetActive(false);
            break;
            case crosshair_type.pickup:
            crosshair_gun.SetActive(false);
            crosshair_pickup.SetActive(true);
            crosshair_normal.SetActive(false);
            break;
            default:
            break;
        }
    }
}
