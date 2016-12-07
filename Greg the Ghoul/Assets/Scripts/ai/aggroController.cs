using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

public class aggroController : MonoBehaviour {
	public Collider detectRange;
	public Collider followRange;
	public GameObject self;
	public GameObject face;
	public bool detected = false; //They are within detection range
	public bool seen = false; //They are within detection range and visible
	public bool aware = false; //They are within detection range and have been seen
	public GameObject aggroTarget;
	public string[] enemies = {"Player"};
	public Transform lastKnownPosition;
	
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
			aware = true;
			
			//check if they can be seen
			try{
			for(int i = 0; i < aggroTarget.GetComponent<sightLineChecks>().edges.Length; i++){
				Transform target = aggroTarget.GetComponent<sightLineChecks>().edges[i];
				
				RaycastHit hit;
				bool finish = false;
				Vector3 lastPosition = face.transform.position;
				Vector3 raycastDir = target.position - face.transform.position;
			
				while(!finish){
					if(Physics.Raycast(lastPosition, raycastDir, out hit)){
						switch(hit.transform.tag){
							//CAN SEE THROUGH
							case "damage_source":
							case "player_detector":
							case "ground_weapon":
							case "interactable":
							case "enemy_weapon":
							case "player_weapon":
							case "enemy":
								lastPosition = hit.transform.position;
								break;
						
							//CANT SEE THROUGH
							default:
								lastPosition = hit.transform.position;
								finish = true;
								seen = false;
								break;
						}
						//Debug.Log(hit.transform.gameObject.name);
					}
					Debug.DrawLine(face.transform.position, lastPosition, Color.red, 1);
					//CAN SEE THEM
					if(hit.transform.tag == aggroTarget.tag){
						lastKnownPosition.transform.position = lastPosition;
						seen = true;
						Debug.DrawLine(face.transform.position, target.position, Color.blue, 1);
						break;
					}
				}
			}
			}
			catch(NullReferenceException){
				//they don't have sight line checks so they can't be seen
			}
			
			//TODO check if we are aware but they cannot be seen
		}
		else{
			//once out of max detection range enemy is safe
			seen = false;
			aware = false;
		}
		
	}
}
