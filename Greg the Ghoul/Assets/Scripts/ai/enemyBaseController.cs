using UnityEngine;
using System.Collections;

public class enemyBaseController : MonoBehaviour {
	public Animator anim;
	public float moveH = 0;
	public float moveV = 0;
	public GameObject interest;
	public GameObject self;
	public GameObject aggroDetector;
	public bool aggroed = false;
	public float walkSpeed;
	public float runMultiplier;
	
	// Call this to remotely set interest
	public void SetInterest(GameObject other){
		interest = other;
	}
	public void SetInterest(){
		//TODO determine something to use as interest and set it
		
		Debug.Log(interest.name);
	}
	
	// Call this to move towards a GameObject
	public void MoveTo (GameObject other){
		
	}
	
	// Call this to attack
	public void Attack () {
		anim.SetTrigger("attack");
	}
	
	// Call this to use skill
	public void UseSkill () {
		anim.SetTrigger("useSkill");
	}
	
	// Call this when killed
	public void Die () {
		anim.SetTrigger("killed");
	}
	
	// Call this when hit
	public void Get_hit () {
		anim.SetTrigger("damaged");
	}
	
	// Use this for initialization
	public virtual void Start () {
		interest = self;
	}
	public virtual void Update(){
		aggroed = aggroDetector.GetComponent<aggroController>().detected;
		Debug.Log(aggroed);
	}
}
