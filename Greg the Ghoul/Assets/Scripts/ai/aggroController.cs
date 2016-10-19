using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

public class aggroController : MonoBehaviour {
	public Collider detectRange;
	public Collider followRange;
	public GameObject self;
	public bool detected = false;
	public string[] enemies = {"Player"};
	
	void OnTriggerEnter(Collider other){
		bool notEnemy = true;
		for(int i = 0; i < enemies.Length; i++){
			if(other.gameObject.tag == enemies[i]){
				notEnemy = false;
			}
		}
		if(notEnemy){
			return;
		}
		if(detected){
		}
		else{
			detected = true;
			followRange.gameObject.SetActive(true);
			detectRange.gameObject.SetActive(false);
			Debug.Log("Hi!");
		}
		
	}
	
	void OnTriggerExit(Collider other){
		if(!detected){
			return;
		}
		followRange.gameObject.SetActive(false);
		detectRange.gameObject.SetActive(true);
		detected = false;
		Debug.Log("Bye!");
	}
	
	void Start () {
	}
}
