using UnityEngine;
using System.Collections;

public class enemyBaseController : MonoBehaviour {
	public Animator anim;
	public float moveV = 0f;
	public float moveH = 0f;
	public GameObject interest;
	public GameObject self;
	public GameObject aggroDetector;
	public Vector3 toTarget;
	public bool seeAggroTarget = false;
	public float walkSpeed = 1f;
	public float runMultiplier = 1.5f;
	public float minRunDistance = 3.5f;
	public float minWalkDistance = 1f;
	
	// Call this to remotely set interest
	public void SetInterest(GameObject other){
		interest = other;
	}
	public void SetInterest(){
		if(seeAggroTarget){
			SetInterest(aggroDetector.GetComponent<aggroController>().aggroTarget);
		}
		else{
			//TODO determine something to use as interest and set it
			SetInterest(self);
			//Do a bunch of raycasts?  Expand a collision box?
		}
		//Debug.Log(interest.name);
	}
	
	// Call this to move towards a GameObject
	public void MoveTo (GameObject other){
		toTarget = other.transform.position - self.transform.position;
		float translation = 0f;
		Debug.Log(toTarget.magnitude);
		if(seeAggroTarget){
			//run up to interest then slow down
			if(toTarget.magnitude > minRunDistance){
				//You're far away, run
				translation = walkSpeed * runMultiplier;
			}
			else if(toTarget.magnitude >= minWalkDistance){
				//You're close walk
				translation = walkSpeed;
			}
			else{
				//You're really close, don't move
				translation = 0;
			}
		}
		else if(interest != self){
			//there's no rush, just walk
			translation = walkSpeed;
		}
		else{
			translation = 0f;
		}
		if(translation > walkSpeed){
			moveH = 1f;
		}
		else if(translation > 0){
			moveH = 0.2f;
		}
		else{
			moveH = 0f;
		}
		translation *= Time.deltaTime;
		
		//Rotate the character
		Vector3 tempTarget = new Vector3(	other.transform.position.x,
											transform.position.y,
											other.transform.position.z);
		transform.LookAt(tempTarget);
		//Move the character towards the GameObject
		Vector3 tempTrans = transform.forward * translation;
		self.GetComponent<CharacterController>().SimpleMove(tempTrans);
		
		Debug.Log(tempTrans);
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
		seeAggroTarget = aggroDetector.GetComponent<aggroController>().seen;
		SetInterest();
		MoveTo(interest);
		Debug.Log(seeAggroTarget);
		anim.SetFloat("moveV", moveV);
		anim.SetFloat("moveH", moveH);
	}
}
