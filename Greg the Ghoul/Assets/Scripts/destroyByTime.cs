using UnityEngine;
using System.Collections;

public class destroyByTime : MonoBehaviour {

	  public float lifetime;
	  public GameObject gameObject;
     void Start ()
     {
         Destroy (gameObject, lifetime);
     }
	
}
