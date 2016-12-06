using UnityEngine;
using System.Collections;

public class projectileMovment : MonoBehaviour {
	public GameObject gameObject;
	public float speed;
	// Update is called once per frame
	void Update () {
	gameObject.transform.Translate(0, 0, (float)speed);
	}
	void OnTriggerEnter(Collider other){
		Debug.Log("Hit Something");
		Debug.Log(other.tag);
		if(other.gameObject.tag =="HitBox"){
			Debug.Log("Enemy Hit");
			Destroy(other.gameObject);
		}
	}
}
