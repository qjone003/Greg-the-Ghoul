using UnityEngine;
using System.Collections;

public class projectileMovment : MonoBehaviour {
	public GameObject gameObject;
	public float speed;
	// Update is called once per frame
	void Update () {
	gameObject.transform.Translate(0, 0, (float)speed);
	}
	void OnTriggerEnter(Collider contact){
		Debug.Log(contact.gameObject.transform.gameObject.tag);
		if(contact.gameObject.tag == "Untagged"){
			Destroy(contact.gameObject);
			Debug.Log("Enemy Hit");
		}
	}
}

