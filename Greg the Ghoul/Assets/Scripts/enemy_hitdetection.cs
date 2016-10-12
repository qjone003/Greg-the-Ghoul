using UnityEngine;
using System.Collections;

public class enemy_hitdetection : MonoBehaviour {
	public string opponent;
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag != opponent){
			return;
		}
		Debug.Log("Hit");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
