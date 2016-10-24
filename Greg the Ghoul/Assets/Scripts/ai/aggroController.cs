using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

public class aggroController : MonoBehaviour {
	public Collider detectRange;
	public Collider followRange;
	public GameObject self;
	public bool detected = false;
	public bool seen = false;
	public bool aware = false;
	public GameObject aggroTarget;
	public bool aggro = false;
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
		if(detected){
		}
		else{
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
		//Debug.Log("Bye!");
	}
	
	void Start () {
	}
	
	void Update () {
		if(detected && seen){
			//Within detection range and in sight
			aggro = true;
			aware = true;
		}
		if(detected && !seen){
			//Within detection range but not in sight
			if(aware){
				//Knows that they are nearby and has seen them
				aggro = true;
			}
			else{
				//Hasn't seen them yet but they are nearby
				aggro = false;
			}
		}
		if(!detected){
			//Not within detection range
			aware = false;
			aggro = false;
		}
	}
}
