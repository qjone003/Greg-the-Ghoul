using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

public class aggroController : MonoBehaviour {
	public Collider detectRange;
	public Collider followRange;
	public GameObject self;
	public bool detected = false; //They are within detection range
	public bool seen = false; //They are within detection range and visible
	public bool aware = false; //They are within detection range and have been seen
	public GameObject aggroTarget;
	public string[] enemies = {"Player"};
	
	void OnTriggerEnter(Collider other){
		bool notEnemy = true;
		for(int i = 0; i < enemies.Length; i++){
			if(other.gameObject.tag == enemies[i]){
				notEnemy = false;
				aggroTarget = other.gameObject;
			}
		}
		if(notEnemy){
			return;
		}
		if(!detected){
			detected = true;
			followRange.gameObject.SetActive(true);
			detectRange.gameObject.SetActive(false);
			//Debug.Log("Hi!");
		}
		
	}
	
	void OnTriggerExit(Collider other){
		if(!detected){
			return;
		}
		followRange.gameObject.SetActive(false);
		detectRange.gameObject.SetActive(true);
		detected = false;
		aggroTarget = null;
		//Debug.Log("Bye!");
	}
	
	void Start () {
	}
	
	void Update () {
		if(detected){
			//for now if they are detected they are seen
			seen = true;
			aware = true;
			//TODO check if they can be seen
			
			//TODO check if we are aware but they cannot be seen
		}
		else{
			//once out of max detection range enemy is safe
			seen = false;
			aware = false;
		}
		
	}
}
