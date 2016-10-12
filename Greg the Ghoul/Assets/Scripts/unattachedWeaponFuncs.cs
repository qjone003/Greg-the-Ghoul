using UnityEngine;
using System.Collections;

public class unattachedWeaponFuncs : MonoBehaviour {
	public GameObject self;
	
	// Called whenever interaction area is triggered by another collision shape
	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag != "Player"){
			return;
		}
	}
	
	void OnTriggerExit (Collider other){
		if(other.gameObject.tag != "Player"){
			return;
		}
	}
	
	// Use this for initialization
	void Start () {
	
	}
}
