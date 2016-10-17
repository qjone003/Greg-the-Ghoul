using UnityEngine;
using System.Collections;

public class aggroController : MonoBehaviour {
	public Collider detectRange;
	public Collider followRange;
	public GameObject self;
	private bool detected = false;
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag != "Player"){
			return;
		}
		if(detected){
			
		}
		else{
			
		}
		
	}
	
	void OnTriggerExit(Collider other){
		
	}
	
	void Start () {
	}
}
