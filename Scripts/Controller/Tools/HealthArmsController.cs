using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlackPearl;
[RequireComponent(typeof(AudioSource))]
public class HealthArmsController : MonoBehaviour
{
    [Header("References")]

    private Item item = null;

    [SerializeField] private AudioSource audios = null;
    private Animator animator = null;
    private FirstPersonAIO player;
    public bool isInitialized = false;
   public void Initialized()
    {
        audios = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        audios.playOnAwake = false;
        audios.loop = false;
        audios.volume = 0.5f;
        player = FindObjectOfType<FirstPersonAIO>();
        audios.PlayOneShot(item.equiped_clip,0.5f);
        // Heal();
        StartCoroutine(PlayHeal());
        isInitialized = true;
    }



    public void SetToolsItem(Item item)
    {
        this.item = item;
        

    }

    public void Heal()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(!audios.isPlaying)
        {
            audios.PlayOneShot(item.heal_clip);
        }
        player.GetComponent<VitalState>().AddHealthFull(100);

    }
    public IEnumerator PlayHeal()
    {
    
        yield return new WaitForSeconds(3.2f);

        HotBar.instance.Selection();
        yield break;

    }

   
    void Update()
    {
        
    }
}
