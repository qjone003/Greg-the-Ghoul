using UnityEngine;
using System.Collections;

public class unattachedWeaponFuncs : MonoBehaviour {
	public GameObject self;
	private bool isColliding;
	
	// Called whenever interaction area is triggered by another collision shape
	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag != "Player"){
			return;
		}
		if(isColliding){
			return;
		}
		isColliding = true;
		//Debug.Log("Weapon");
		
		other.gameObject.GetComponent<playerInputController>().addInteractable(self);
	}
	
	void OnTriggerExit (Collider other){
		if(other.gameObject.tag != "Player"){
			return;
		}
		if(!isColliding){
			return;
		}
		isColliding = false;
		//Debug.Log("Left Weapon");
		other.gameObject.GetComponent<playerInputController>().removeInteractable(self);
	}
	
	// Use this for initialization
	void Start () {
	
	}
}
