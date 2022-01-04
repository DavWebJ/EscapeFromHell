using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlackPearl;
[RequireComponent(typeof(AudioSource))]

public class WeaponController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform muzzle = null;
    [SerializeField] private Transform sightPosition = null;
    [SerializeField] private Transform parentsModels = null;
    public Vector3 parentsModelOrigin = new Vector3();
    private WeaponItem weapon = null;

    [SerializeField] private AudioSource audios = null;
    private Animator animator = null;
    public bool isInitialized = false;
    




    [Header("Properties weapon")]
    
    private float timer = 0; 
    
    public bool isAiming = false;
    public bool canreload;
    public bool isReloading = false;
    private FirstPersonAIO player;
    public bool canFire =>!isReloading && !GameManager.instance.CheckHUD && player.IsGrounded && !player.isSprinting;
    public float fovOrigin = 60;
    public float fov_zooming = 15;

    [Header("FX particule")]
    public ParticleSystem muzzle_particle = null;
    public ParticleSystem cartridge_particle = null;

    [Header("Prefab")]
    [SerializeField] private GameObject prefab_bullet = null;
    [Header("Ammo Count:")]
    public int AmmoRemain = 0;
    

    public void Initialized()
    {
        audios = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        audios.playOnAwake = false;
        audios.loop = false;
        audios.volume = 0.5f;
        animator.Play("Get");
        audios.PlayOneShot(weapon.equiped_sound,0.5f);
        player = FindObjectOfType<FirstPersonAIO>();
        prefab_bullet = Resources.Load<GameObject>("Weapon/FX/Bullet");
        muzzle_particle = transform.Find(weapon.pathToMuzzleFx).GetComponentInChildren<ParticleSystem>();
        cartridge_particle = transform.Find(weapon.pathToCartridgeFx).GetComponentInChildren<ParticleSystem>();
        muzzle = transform.Find(weapon.pathToMuzzleTransform);
        parentsModels = transform.Find("parentmodel");
        parentsModelOrigin = new Vector3(0,0,0);
        HUDWeapon.instance.GetWeaponInfos(weapon);
        fovOrigin = player.fpscam.cam.fieldOfView;
        HUD.instance.ChangeCrossHair(HUD.crosshair_type.gun);
        AmmoRemain = Inventory.instance.AmountConsumableInInventory("AmmoGun");
        isInitialized = true;
    }

    public void SetWeaponItem(Item item)
    {
        if(item.itemType != ItemType.Weapon)
            return;
        weapon = item as WeaponItem;

        Initialized();
    }
    public void updateInputs()
    {
        AmmoRemain = Inventory.instance.AmountConsumableInInventory("AmmoGun");
        if(AmmoRemain <= 0 && weapon.ammo <= 0)
        {
            AmmoRemain = 0;
            HUDWeapon.instance.weapont_text.color = Color.red;
            HUDWeapon.instance.ShowReload(false,"");
        }

        if(weapon.ammo <= 0 && AmmoRemain > 0)
        {
            HUDWeapon.instance.ShowReload(true,"Appuyer sur R pour recharger");
        }

        Vector3 originPos = player.fpscam.armsHolder.localPosition;
        // AIM 
       
        isAiming = Input.GetKey(GameManager.instance.input.input_actionSecondary) && !Inventory.instance.isInventoryOpen;
        
        HUDWeapon.instance.GetWeaponInfos(weapon);
        Zooming(isAiming);

        if(isAiming && weapon.weaponType == WeaponType.scoped)
        {
            // player.zoomFOV = 20f;
            //StartCoroutine(onScoped());
           
        }else
        {
            // player.zoomFOV = 30f;
            //StartCoroutine(onUncoped());
            
        }
        if(player.IsGrounded && isAiming)
        {
            animator.SetBool(weapon.aim_animation,isAiming);
            
            player.fpscam.armsHolder.localPosition = Vector3.Lerp(player.fpscam.armsHolder.localPosition,weapon.aimingPos,Time.deltaTime * 8);
        }else
        {
            player.fpscam.armsHolder.localPosition = Vector3.Lerp(player.fpscam.armsHolder.localPosition,Vector3.zero,Time.deltaTime * 8);
            animator.SetBool(weapon.aim_animation,false);
        }
        
        if(!player.IsGrounded)
        {
            if(weapon.walk_aim_animation != string.Empty)
            {
                animator.SetBool(weapon.walk_aim_animation,false);
            }
            

            animator.SetBool(weapon.aim_animation,false);
            animator.SetBool(weapon.walk_animation,false);
            animator.SetBool(weapon.sprint_animation,false);

        }

        if(!player.isWalking && !player.isSprinting)
        {
    
            if(weapon.walk_aim_animation != string.Empty)
            {
                animator.SetBool(weapon.walk_aim_animation,false);
            }
            // animator.SetBool(weapon.aim_animation,isAiming);
            animator.SetBool(weapon.walk_animation,false);
            animator.SetBool(weapon.sprint_animation,false);
            
        }

        if(player.isWalking && player.fps_Rigidbody.velocity != Vector3.zero && !isAiming)
        {
    

            if(weapon.walk_aim_animation != string.Empty)
            {
                animator.SetBool(weapon.walk_aim_animation,false);
            }
            animator.SetBool(weapon.walk_animation,true);
            animator.SetBool(weapon.aim_animation,false);
           
           animator.SetBool(weapon.sprint_animation,false);
            
        }
        if(player.isWalking && player.fps_Rigidbody.velocity != Vector3.zero && isAiming)
        {
            if(weapon.walk_aim_animation != string.Empty)
            {
                animator.SetBool(weapon.walk_aim_animation,true);
            }
    
            animator.SetBool(weapon.walk_animation,false);
            // animator.SetBool(weapon.aim_animation,true);
           
           animator.SetBool(weapon.sprint_animation,false);
        }
        if(player.isSprinting && player.fps_Rigidbody.velocity != Vector3.zero)
        {
            if(weapon.walk_aim_animation != string.Empty)
            {
                animator.SetBool(weapon.walk_aim_animation,false);
            }
            animator.SetBool(weapon.walk_animation,false);
            animator.SetBool(weapon.sprint_animation,true);
            animator.SetBool(weapon.aim_animation,false);
       
        }
 

        // FIRE
        if(weapon.firemode == Firemode.auto){
            weapon.cooldown = weapon.cooldown_auto;
            if(Input.GetKeyDown(GameManager.instance.input.input_actionPrimary))
            {
                Fire(isAiming);
            }
        }
        else
        {
            weapon.cooldown = weapon.cooldown_semi;
            if(Input.GetKeyDown(GameManager.instance.input.input_actionPrimary))
            {
                Fire(isAiming);
            }
        }

        
        if (Input.GetKeyDown(GameManager.instance.input.input_reload) && weapon.ammo < weapon.max_ammo)
        {

            if(AmmoRemain > 0)
            {
                // HUDWeapon.instance.ShowReload(true,"Appuyer sur R pour recharger");
                StartCoroutine(Reloading());
            }else
            {
                HUDWeapon.instance.ShowReload(false,"");
                return;
              
            }
            

        }


    }

    private void Fire(bool Aiming)
    {

        if(Time.time > timer && canFire)
        {
            if(weapon.ammo <= 0)
            {
        
                if(Aiming)
                {
                    animator.Play(weapon.shot_aim_animation);
                    audios.PlayOneShot(weapon.empty_clip);
                }else
                {
                    animator.Play(weapon.shot_animation);
                    audios.PlayOneShot(weapon.empty_clip);
                }
            }
            else{
                //FX
                weapon.ammo --;
                
                muzzle_particle.Play();
                cartridge_particle.Play();
                audios.PlayOneShot(weapon.fire_clip);
       
                if(Aiming)
                {
                    animator.Play(weapon.shot_aim_animation);
                }else
                {
                    animator.Play(weapon.shot_animation);
                }
                HUDWeapon.instance.GetWeaponInfos(weapon);
                Instantiate(prefab_bullet,muzzle.position,muzzle.rotation);
                player.CamRecoil(weapon.recoil_force);
            }
            timer = Time.time + weapon.cooldown;
            
        }
    }

    public IEnumerator Reloading()
    {
        
        isReloading = true;
        if(!audios.isPlaying)
        {
            audios.PlayOneShot(weapon.reload_clip);
        }
        animator.Play(weapon.reload_animation);
        yield return new WaitForSeconds(weapon.reload_clip.length);
        


        int ammoToReload = weapon.max_ammo - weapon.ammo;
        
        if(ammoToReload >= AmmoRemain)
        {
            ammoToReload = AmmoRemain;
            weapon.ammo = ammoToReload + weapon.ammo;
            Inventory.instance.UpdateConsumableInInventory("AmmoGun",ammoToReload);
            
            HUDWeapon.instance.GetWeaponInfos(weapon);
            
        }else
        {
         
            weapon.ammo += ammoToReload;
            Inventory.instance.UpdateConsumableInInventory("AmmoGun",ammoToReload);
            
            HUDWeapon.instance.GetWeaponInfos(weapon);
        
            
        }
        HUDWeapon.instance.ShowReload(false,"");
        isReloading = false;
             
        
    }


    public IEnumerator onScoped()
    {
        
        yield return new WaitForSeconds(0.35f);
        HUD.instance.setScopedImage(true);
    //    player.fpscam.weapon_cam.SetActive(false);
    }

    public IEnumerator onUncoped()
    {
        // player.fpscam.weapon_cam.SetActive(true);
        yield return null;
        HUD.instance.setScopedImage(false);
        
    }

    public void Zooming(bool isZooming)
    {
        
        if(isZooming)
        {
            player.fpscam.cam.fieldOfView = Mathf.Lerp(player.fpscam.cam.fieldOfView,fovOrigin - fov_zooming,Time.deltaTime * 8);
        }else
        {
            
            player.fpscam.cam.fieldOfView = Mathf.Lerp(player.fpscam.cam.fieldOfView,fovOrigin,Time.deltaTime * 8);
        }
    }
    void Update()
    {
        if(isInitialized)
        {
            updateInputs();
            muzzle.LookAt(player.fpscam.targetLook);
            
        }
        
    }
}
