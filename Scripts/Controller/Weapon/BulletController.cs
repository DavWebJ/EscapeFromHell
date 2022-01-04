using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlackPearl;
public class BulletController : MonoBehaviour
{
   [SerializeField] private float speed = 800;
   [SerializeField] private float impact = 200;
   

   private Vector3 lastPos = new Vector3();
    void Start()
    {
        lastPos = transform.position;
    }

    
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        RaycastHit hit;
          if(Physics.Linecast(lastPos,transform.position,out hit))
          {
              if(hit.collider && hit.collider.sharedMaterial)
              {

                  GameObject go = GameManager.instance.resources
                  .getSurface(hit.collider.sharedMaterial.name);
                  if(go != null)
                  {
                      GameObject impact = Instantiate(go,hit.point + hit.normal * 0.001f,Quaternion.LookRotation(hit.normal));
                      impact.transform.parent = hit.collider.transform;
                  }
              }
              Destroy(gameObject);

            
            //   if(hit.rigidbody)
            //   {
            //       hit.rigidbody.AddForce(-hit.normal * impact);
            //   }
          }

        lastPos = transform.position;
    }
}
