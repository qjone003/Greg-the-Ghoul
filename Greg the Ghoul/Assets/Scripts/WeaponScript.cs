using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

  public Transform attachPoint;
  public GameObject owner;
     
     void Start()
     {
         if (owner != null)
         {
             transform.localScale = owner.transform.localScale;
         }
     }
 
     void Update()
     {
         if (owner != null)
         {
             transform.position = attachPoint.position;
             transform.rotation = attachPoint.rotation;
         }
         else Destroy(gameObject);
     }