using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using BlackPearl;


public class AudioM : MonoBehaviour
{
    public static AudioM instance;
    public AudioSource audioSource;
    public AudioSource AudiosHUD;
    public AudioSource thunder_audios = null;
    public AudioClip[] thunderclip;
    public float maxvol = 1f;
    public float minvol = 0.1f;
    public float currentVolume;
    public float AudioFadeTime = 3.5f;
    public AudioClip ChasingClip;
    public AudioClip ambientNormalClip;
    public AudioClip AI_victoryClip;
    public float timer = 0;
    public float thunderTransitionTime = 5;
    

    [Header("HUD Inventory:")]
    public AudioClip hover_clip;


    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }


    }
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        currentVolume =  1;
        // PlayAmbientBackground(ambientNormalClip,true,true);
       AudiosHUD = GameObject.FindGameObjectWithTag("AudioHUD").GetComponent<AudioSource>();
       thunder_audios = transform.Find("thunder_fx").GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = currentVolume;
        audioSource.Play();
    }



    public void PlayTransitionBeginChasing()
    {
        PlayAmbientBackground(ChasingClip,true,false);
    }

    public void PlayTransitionEndChasing()
    {

        PlayAmbientBackground(ambientNormalClip,true,false);

    }
    public void PlayTransitionAIVictory()
    {

        PlayAmbientBackground(AI_victoryClip,false,false);

    }

    

    public void PlayHUDHoverClip()
    {
        currentVolume =  audioSource.volume;
        
        if(!AudiosHUD.isPlaying)
        {
            AudiosHUD.PlayOneShot(hover_clip,0.5f);
            return;
        }
    }

    public void PlayOneShotClip(AudioClip clip)
    {
        
        audioSource.PlayOneShot(clip,0.5f);
    }



    public void PlayAmbientBackground(AudioClip clip, bool loop,bool now)
    {
        currentVolume =  audioSource.volume;

        if(now && loop)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.volume = minvol;
            audioSource.Play();
            return;
        }
        if(audioSource.isPlaying && audioSource.clip != clip)
        {
                audioSource.volume = Mathf.Lerp(currentVolume,0,Time.deltaTime * AudioFadeTime);
                if(audioSource.volume <= 0.02f)
                {
          
                    audioSource.Stop();
                }

                if(!audioSource.isPlaying)
                {
               
                    audioSource.clip = clip;
                    audioSource.loop = loop;
                    audioSource.volume = minvol;
                    if(loop == false)
                    {
                        audioSource.PlayOneShot(clip);
                        return;
                    }
                    audioSource.Play();
                }

        }else
        {
                audioSource.clip = clip;
                audioSource.loop = loop;
                audioSource.volume = minvol;
                audioSource.Play();
        }



    }

    private void Update() {
        // timer += Time.deltaTime;
        // if (timer >= thunderTransitionTime) 
        // {
            
        //     thunder_audios.clip = thunderclip[Random.Range(0, thunderclip.Length)];
        //     thunder_audios.PlayOneShot(thunder_audios.clip);
        //     timer = 0.0f;
        // }
    }

}
