using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlackPearl;

public class FootStepManager : MonoBehaviour
{

    [Header("References")]
    public AudioSource audioS;
    public FirstPersonAIO player;
    public static FootStepManager instance;

    [Header("Clip walk:")]
    public AudioClip[] gravel_walk;
    public AudioClip[] grass_walk;
    public AudioClip[] sand_walk;
    public AudioClip[] water_walk;
    public AudioClip[] concrete_walk;
    public AudioClip[] metal_walk;
    public AudioClip[] wood_walk;
    public AudioClip[] dirt_walk;

    [Header("Clip sprint:")]
    public AudioClip[] gravel_sprint;
    public AudioClip[] grass_sprint;
    public AudioClip[] sand_sprint;
    public AudioClip[] water_sprint;
    public AudioClip[] concrete_sprint;
    public AudioClip[] metal_sprint;
    public AudioClip[] wood_sprint;
    public AudioClip[] dirt_sprint;

    [Header("Jump and Land clip:")]
    public AudioClip jumpclip;
    public AudioClip landclip;

    [Header("Parameters:")]
    public float stepInterval = 0;
    public float walkStepInterval = 0.5f;
    public float SprintStepInterval = 0.3f;

    [Header("Volume Controller:")]
    public float minVolWalk = 0.5f;
    public float maxVolWalk = 0.7f;
    public float minVolSprint = 0.3f;
    public float maxVolSprint = 0.5f;
    [Header("Pitch Controller:")]
    public float minPitchWalk = 0.8f;
    public float maxPitchWalk = 1;
    public float minPitchSprint = 1;
    public float maxPitchSprint = 1.2f;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        audioS = GetComponent<AudioSource>();
        player = GetComponent<FirstPersonAIO>();
    }

    public void GetTheSurfacesName(string surface)
    {
        if(player.isWalking)
        {
            PlayWalkFootStep(surface);
        }
        if(player.isSprinting)
        {
            PlaySprintFootStep(surface);
        }

        if(player.isJumping)
        {
            PlayJumpFootStep();
        }
    }

    public void PlayWalkFootStep(string surface)
    {
        switch (surface)
        {
            case "Grass":
            if(Time.time > stepInterval)
            {
                stepInterval = Time.time + walkStepInterval;
        
                float volume = UnityEngine.Random.Range(minVolWalk, maxVolWalk);
                audioS.pitch = UnityEngine.Random.Range(minPitchWalk, maxPitchWalk);
                audioS.PlayOneShot(grass_walk[UnityEngine.Random.Range(0, grass_walk.Length)],volume);
                return;
            }
            break;
            case "Metal":
            if(Time.time > stepInterval)
            {
                stepInterval = Time.time + walkStepInterval;
        
                float volume = UnityEngine.Random.Range(minVolWalk, maxVolWalk);
                audioS.pitch = UnityEngine.Random.Range(minPitchWalk, maxPitchWalk);
                audioS.PlayOneShot(metal_walk[UnityEngine.Random.Range(0, metal_walk.Length)],volume);
                return;
            }
            
            break;
            case "Concrete":
            if(Time.time > stepInterval)
            {
                stepInterval = Time.time + walkStepInterval;
        
                float volume = UnityEngine.Random.Range(minVolWalk, maxVolWalk);
                audioS.pitch = UnityEngine.Random.Range(minPitchWalk, maxPitchWalk);
                audioS.PlayOneShot(concrete_walk[UnityEngine.Random.Range(0, concrete_walk.Length)],volume);
                return;
            }
            
            break;
            case "Gravel":
            if(Time.time > stepInterval)
            {
                stepInterval = Time.time + walkStepInterval;
        
                float volume = UnityEngine.Random.Range(minVolWalk, maxVolWalk);
                audioS.pitch = UnityEngine.Random.Range(minPitchWalk, maxPitchWalk);
                audioS.PlayOneShot(gravel_walk[UnityEngine.Random.Range(0, gravel_walk.Length)],volume);
                return;
            }
           
            break;
            case "Wood":
            if(Time.time > stepInterval)
            {
                stepInterval = Time.time + walkStepInterval;
        
                float volume = UnityEngine.Random.Range(minVolWalk, maxVolWalk);
                audioS.pitch = UnityEngine.Random.Range(minPitchWalk, maxPitchWalk);
                audioS.PlayOneShot(wood_walk[UnityEngine.Random.Range(0, wood_walk.Length)],volume);
                return;
            }
            
            break;
            case "Dirt":
            if(Time.time > stepInterval)
            {
                stepInterval = Time.time + walkStepInterval;
        
                float volume = UnityEngine.Random.Range(minVolWalk, maxVolWalk);
                audioS.pitch = UnityEngine.Random.Range(minPitchWalk, maxPitchWalk);
                audioS.PlayOneShot(dirt_walk[UnityEngine.Random.Range(0, dirt_walk.Length)],volume);
                return;
            }
            break;
            case "Water":
            if(Time.time > stepInterval)
            {
                stepInterval = Time.time + walkStepInterval;
        
                float volume = UnityEngine.Random.Range(minVolWalk, maxVolWalk);
                audioS.pitch = UnityEngine.Random.Range(minPitchWalk, maxPitchWalk);
                audioS.PlayOneShot(water_walk[UnityEngine.Random.Range(0, water_walk.Length)],volume);
                return;
            }
            break;
            case "Sand":
            if(Time.time > stepInterval)
            {
                stepInterval = Time.time + walkStepInterval;
        
                float volume = UnityEngine.Random.Range(minVolWalk, maxVolWalk);
                audioS.pitch = UnityEngine.Random.Range(minPitchWalk, maxPitchWalk);
                audioS.PlayOneShot(sand_walk[UnityEngine.Random.Range(0, sand_walk.Length)],volume);
                return;
            }
            break;
            
            default:
            
            break;
        }
    }

    public void PlaySprintFootStep(string surface)
    {
        switch (surface)
        {
            case "Grass":
            if(Time.time > stepInterval)
            {
                stepInterval = Time.time + SprintStepInterval;
                float volume = UnityEngine.Random.Range(minVolSprint, maxVolSprint);
                audioS.pitch = UnityEngine.Random.Range(minPitchSprint, maxPitchSprint);
                audioS.PlayOneShot(grass_sprint[UnityEngine.Random.Range(0, grass_sprint.Length)],volume);
                return;
            }
            break;
            case "Metal":
            if(Time.time > stepInterval)
            {
                stepInterval = Time.time + SprintStepInterval;
                float volume = UnityEngine.Random.Range(minVolSprint, maxVolSprint);
                audioS.pitch = UnityEngine.Random.Range(minPitchSprint, maxPitchSprint);
                audioS.PlayOneShot(metal_sprint[UnityEngine.Random.Range(0, metal_sprint.Length)],volume);
                return;
            }
            break;
            case "Concrete":
            if(Time.time > stepInterval)
            {
                stepInterval = Time.time + SprintStepInterval;
                float volume = UnityEngine.Random.Range(minVolSprint, maxVolSprint);
                audioS.pitch = UnityEngine.Random.Range(minPitchSprint, maxPitchSprint);
                audioS.PlayOneShot(concrete_sprint[UnityEngine.Random.Range(0, concrete_sprint.Length)],volume);
                return;
            }
            break;
            case "Gravel":
            if(Time.time > stepInterval)
            {
                stepInterval = Time.time + SprintStepInterval;
                float volume = UnityEngine.Random.Range(minVolSprint, maxVolSprint);
                audioS.pitch = UnityEngine.Random.Range(minPitchSprint, maxPitchSprint);
                audioS.PlayOneShot(gravel_sprint[UnityEngine.Random.Range(0, gravel_sprint.Length)],volume);
                return;
            }
            break;
            case "Wood":
            if(Time.time > stepInterval)
            {
                stepInterval = Time.time + SprintStepInterval;
                float volume = UnityEngine.Random.Range(minVolSprint, maxVolSprint);
                audioS.pitch = UnityEngine.Random.Range(minPitchSprint, maxPitchSprint);
                audioS.PlayOneShot(wood_sprint[UnityEngine.Random.Range(0, wood_sprint.Length)],volume);
                return;
            }
            break;
            case "Dirt":
            if(Time.time > stepInterval)
            {
                stepInterval = Time.time + SprintStepInterval;
                float volume = UnityEngine.Random.Range(minVolSprint, maxVolSprint);
                audioS.pitch = UnityEngine.Random.Range(minPitchSprint, maxPitchSprint);
                audioS.PlayOneShot(dirt_sprint[UnityEngine.Random.Range(0, dirt_sprint.Length)],volume);
                return;
            }
            break;
            case "Water":
            if(Time.time > stepInterval)
            {
                stepInterval = Time.time + SprintStepInterval;
                float volume = UnityEngine.Random.Range(minVolSprint, maxVolSprint);
                audioS.pitch = UnityEngine.Random.Range(minPitchSprint, maxPitchSprint);
                audioS.PlayOneShot(water_sprint[UnityEngine.Random.Range(0, water_sprint.Length)],volume);
                return;
            }
            break;
            case "Sand":
            if(Time.time > stepInterval)
            {
                stepInterval = Time.time + SprintStepInterval;
                float volume = UnityEngine.Random.Range(minVolSprint, maxVolSprint);
                audioS.pitch = UnityEngine.Random.Range(minPitchSprint, maxPitchSprint);
                audioS.PlayOneShot(sand_sprint[UnityEngine.Random.Range(0, sand_sprint.Length)],volume);
                return;
            }
            break;
            
            default:
            print("no material");
            break;
        }
    }


    public void PlayJumpFootStep()
    {
        if(!audioS.isPlaying)
            audioS.PlayOneShot(jumpclip,1);
        return;
    }
    public void PlayLandFootStep()
    {
        if(!audioS.isPlaying)
            audioS.PlayOneShot(landclip,1);
        return;
    }

}
