using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public Animator anim;
	private float moveH = 0;
	private float moveV = 0;
	private GameObject interest;
	public GameObject self;
	private bool aggroed = false;
	
	// Call this to remotely set interest
	public void SetInterest(GameObject other){
		interest = other;
	}
	public void SetInterest(){
		//TODO determine something to use as interest and set it
		
		Debug.Log(interest.name);
	}
	
	// Call this to move towards a GameObject
	void MoveTo (GameObject other){
		
	}
	
	// Call this to attack
	void Attack () {
		anim.SetTrigger("attack");
	}
	
	// Call this to use skill
	void UseSkill () {
		anim.SetTrigger("useSkill");
	}
	
	// Call this when killed
	void Die () {
		anim.SetTrigger("killed");
	}
	
	// Call this when hit
	void Get_hit () {
		anim.SetTrigger("damaged");
	}
	
	// Use this for initialization
	void Start () {
		interest = self;
	}
	
	// Update is called once per frame
	void Update () {
		if(interest == self){
			//TODO just idle
		}
		else if(interest.tag == "Player"){
			//TODO chase player, attack, or use skill based on distance and nearby hostiles
		}
		else{
			// Go to object of interest
			MoveTo(interest);
		}
		
		//TODO determine where interest is based on nearby hostiles and other objects
	
	}
}