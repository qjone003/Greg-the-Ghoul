using UnityEngine;
using System.Collections;

public class player_hitdetection : MonoBehaviour {
	
	public Slider health;
	private bool youDead = false;

	// Update is called once per frame
	void Update () {
		if(!youDead){
			Debug.Log("Checkpoint screen here");
		}
	
	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag =="damage_source"){
			health.value -= .011f;
		}
		else{
			youDead = true;
		}
		
	}
}
