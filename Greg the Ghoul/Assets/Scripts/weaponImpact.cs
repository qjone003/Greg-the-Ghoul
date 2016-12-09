using UnityEngine;
using System.Collections;

public class weaponImpact : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider contact){
		if(contact.gameObject.transform.parent.transform.name == "Skeleton@Skin"){
			Debug.Log("Enemy Hit");
			contact.gameObject.transform.parent.transform.gameObject.GetComponent<enemyBaseController>().Get_hit(50);
		}
	}
}
