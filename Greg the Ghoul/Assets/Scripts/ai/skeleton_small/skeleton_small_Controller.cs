using UnityEngine;
using System.Collections;

public class skeleton_small_Controller : enemyBaseController {

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		if(interest == self){
			//TODO just idle
		}
		else if(interest.tag == "Player"){
			//TODO attack, or use skill based on distance and nearby hostiles
		}
		
		//TODO determine where interest is based on nearby hostiles and other objects
	}
}