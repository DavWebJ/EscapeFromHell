using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackPearl
{
     [CreateAssetMenu(menuName = "Inventory/Item/Create Weapon")]
    public class WeaponItem : Item
    {
        [Header("References")]
        public string pathToMuzzleFx;
        public string pathToMuzzleTransform;
        public string pathToCartridgeFx;
        [Header("sound")]
        public AudioClip equiped_sound,unequiped_sound,cartridge_clip,fire_clip,reload_clip,empty_clip;

        [Header("Weapon Properties")]
        public float cooldown_auto = .1f;
        public float cooldown_semi = .5f;
        public float cooldown = 0;
        public float recoil_force =0;
        public int weaponDamage;
        public int bonusDamageMultiplier;
        


        [Header("Ammo")]
        public int ammo = 20;
        public int max_ammo = 20;

        [Header("weapon animation")]
        public string equipedWeapon =string.Empty;
        public string unEquipedWeapon =string.Empty;
        public string walk_animation =string.Empty;
        public string sprint_animation =string.Empty;
        public string shot_animation =string.Empty;
        public string walk_aim_animation =string.Empty;
        public string shot_aim_animation =string.Empty;
        public string reload_animation =string.Empty;
        public string aim_animation = string.Empty;

        [Header("Aiming Position")]
        public Vector3 aimingPos = new Vector3();
    
        public WeaponItem()
        {
            this.itemType = ItemType.Weapon;
            
            
        }
    }
}