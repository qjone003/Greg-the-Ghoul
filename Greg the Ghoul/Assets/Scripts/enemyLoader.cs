using UnityEngine;
using System.Collections;

public class enemyLoader : MonoBehaviour {

	public GameObject[] enemies;
	
	public void OnTriggerEnter(Collider other){
		if(other.transform.gameObject.tag == "Player"){
			for(int i = 0; i < enemies.Length; i++){
				enemies[i].SetActive(true);
			}
		}
	}
	public void OnTriggerExit(Collider other){
		if(other.transform.gameObject.tag == "Player"){
			for(int i = 0; i < enemies.Length; i++){
				enemies[i].SetActive(false);
			}
		}
	}
}
