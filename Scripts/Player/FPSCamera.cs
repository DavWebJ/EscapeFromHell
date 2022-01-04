using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackPearl{
    public class FPSCamera : MonoBehaviour
    {
        public Transform targetLook = null;
        public Transform targetEject = null;
        public Transform targetZoom = null;
        public Transform armsHolder = null;
        public float zoomAim = 15;
        private FirstPersonAIO player;
        public Camera cam = null;
        public float rayDist = 3;
        public float dropForce = 20;

        private Interract currInteract = null;
        private float fov_origin = 60;


        private void Start() {
           
            // rain = GetComponentInChildren<RainCameraController>();
        
        }

        private void Update() {
            
        }
        public void Init(FirstPersonAIO _player)
        {
            this.player = _player;
            cam = GetComponent<Camera>();
            fov_origin = cam.fieldOfView;
            targetLook = transform.Find("TargetLook");
            targetEject = transform.Find("DropPoint");
            armsHolder = transform.Find("HolderArms");
            targetZoom = transform.Find("TargetZoom");

 
        }


        public void Aim(bool aiming)
        {
            // if(aiming)
            // {
            //     cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,fov_origin - zoomAim , Time.deltaTime * 8);
            // }
            // else
            // {
            //     cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,fov_origin , Time.deltaTime * 8);
            // }
        }

        public void updateTargetLook()
        {
            Vector3 origin = cam.transform.position;
            Vector3 direction = cam.transform.forward;
            // Debug.DrawLine(origin,direction * 1000f,Color.green);
            RaycastHit hit;
            if(Physics.Raycast(cam.transform.position,cam.transform.forward * 1000f,out hit))
            {
                targetLook.position = hit.point;
            }else
            {
                targetLook.position = cam.transform.forward * 1000f;
            }
        }
        
        public void updateRaycast()
        {
            
            if(CanInterract())
            {
              
                Ray ray = new Ray(cam.transform.position,cam.transform.forward);
                RaycastHit hit;
                Physics.Raycast(ray,out hit,rayDist);

                if(hit.collider && hit.collider.GetComponent<Interract>())
                {
                   
                    if(currInteract == null || currInteract != hit.collider.GetComponent<Interract>())
                    {
                        if(currInteract != null)
                        {
                            currInteract.EventInterraction(false);
                          
                        }
                        currInteract = hit.collider.GetComponent<Interract>();
                        currInteract.EventInterraction(true);
                        
             
                    } 
                    
                }
                else
                {
                    if(currInteract != null)
                    {
                        currInteract.EventInterraction(false);
                        currInteract = null;
                    
                    }
                    HUD.instance.ChangeCrossHair(HUD.crosshair_type.normal);
                    
                }

                if(currInteract != null)
                currInteract.Update_Input();
                // HUD.instance.ChangeCrossHair(HUD.crosshair_type.pickup);


            }
            else
            {
               
                if(currInteract != null)
                {
                    currInteract.EventInterraction(false);
                    currInteract = null;
                 
                }
            }
        }

        public bool CanInterract()
        {

            return(Inventory.instance.isInventoryOpen == false);
        }

        public void DropObject(Item item)
        {
            if(item == null || item.amount <= 0 || item.ItemGroundPrefabs == null || item.itemType == ItemType.Buildable)
            {
                return;
            }

             GameObject go = Instantiate(item.ItemGroundPrefabs,targetEject.position,targetEject.rotation);

            if(go.GetComponent<Rigidbody>())
            {
                Rigidbody rb = go.GetComponent<Rigidbody>();
                
                rb.AddForce(player.transform.forward * (dropForce / rb.mass));
            }
            if(go.GetComponent<Interract>())
            {
                go.GetComponent<Interract>().SetItem(item);
            }
            // audios.PlayOneShot(pickup_clip);
            HotBar.instance.Selection();
        }
    }


}