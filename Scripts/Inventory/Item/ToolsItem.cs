using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackPearl{
    [CreateAssetMenu(menuName = "Inventory/Item/Create Tools")]
    public class ToolsItem : Item
    {
        [Header("Sound")]
        public AudioClip equiped_sound = null;
        public AudioClip unequiped_sound = null;
        public AudioClip reload_clip;
        public AudioClip on_off_clip = null;
        public AudioClip batery_out_clip = null;
        public AudioClip zippo_open_clip = null;
       

        [Header("Particle effect")]
         ParticleSystem object_hit_fx = null;

        public float batery;
        public float batery_max = 1;
        public float batery_reduce_value;
        public float batery_decrease = 1;
        [Header("tools animation")]
        public string equipedTools =string.Empty;
        public string unEquipedTools =string.Empty;
        public string walk_animation =string.Empty;
        public string sprint_animation =string.Empty;
        public string reload_animation =string.Empty;


        public ToolsItem()
        {
            this.itemType = ItemType.Tools;
        }
    }
}
