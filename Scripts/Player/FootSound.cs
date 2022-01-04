using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class FootSound : MonoBehaviour {

    public AudioSource audioS;
    public AudioClip[] dur;
    public AudioClip[] herbe;
    public AudioClip[] sable;
    public AudioClip[] eau;
    public AudioClip[] gravier;
    public AudioClip[] neige;
    public AudioClip[] moquette;
    public AudioClip[] bois;
    public float stepInterval = 0.6f;
    public  float nextStep = 0f;



    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Dur" && Time.time > nextStep)
        {
            nextStep = Time.time + stepInterval;
            audioS.PlayOneShot(dur[UnityEngine.Random.Range(0, dur.Length)]);
            return;
        }
        if (col.tag == "Herbe" && Time.time > nextStep)
        {
            nextStep = Time.time + stepInterval;
            audioS.PlayOneShot(herbe[UnityEngine.Random.Range(0, herbe.Length)]);
            return;
        }
        if (col.tag == "Sable" && Time.time > nextStep)
        {
            nextStep = Time.time + stepInterval;
            audioS.PlayOneShot(sable[UnityEngine.Random.Range(0, sable.Length)]);
            return;
        }
        if (col.tag == "Eau" && Time.time > nextStep)
        {
            nextStep = Time.time + stepInterval;
            audioS.PlayOneShot(eau[UnityEngine.Random.Range(0, eau.Length)]);
            return;
        }
        if (col.tag == "Gravier" && Time.time > nextStep)
        {
            nextStep = Time.time + stepInterval;
            audioS.PlayOneShot(gravier[UnityEngine.Random.Range(0, gravier.Length)]);
            return;
        }
        if (col.tag == "Neige" && Time.time > nextStep)
        {
            nextStep = Time.time + stepInterval;
            audioS.PlayOneShot(neige[UnityEngine.Random.Range(0, neige.Length)]);
            return;
        }
        if (col.tag == "Moquette" && Time.time > nextStep)
        {
            nextStep = Time.time + stepInterval;
            audioS.PlayOneShot(moquette[UnityEngine.Random.Range(0, moquette.Length)]);
            return;
        }
        if (col.tag == "Bois" && Time.time > nextStep)
        {
            nextStep = Time.time + stepInterval;
            audioS.PlayOneShot(bois[UnityEngine.Random.Range(0, bois.Length)]);
            return;
        }

    }









}
