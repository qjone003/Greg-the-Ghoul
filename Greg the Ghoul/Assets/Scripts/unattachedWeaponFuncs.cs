using UnityEngine;
using System.Collections;

public class unattachedWeaponFuncs : MonoBehaviour {
	public GameObject self;
	
	// Called whenever interaction area is triggered by another collision shape
	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag != "Player"){
			return;
		}
		//Debug.Log("Weapon");
		other.gameObject.GetComponent<playerInputController>().addInteractable(self);
	}
	
	void OnTriggerExit (Collider other){
		if(other.gameObject.tag != "Player"){
			return;
		}
		//Debug.Log("Left Weapon");
		other.gameObject.GetComponent<playerInputController>().removeInteractable(self);
	}
	
	// Use this for initialization
	void Start () {
	
	}
}
