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
		if(contact.gameObject.transform.parent.transform.name == "Skeleton@Skin"){
			Debug.Log("Enemy Hit");
			contact.gameObject.transform.parent.transform.gameObject.GetComponent<enemyBaseController>().Get_hit(40);
		}
	}
}

