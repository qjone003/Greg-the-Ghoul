using UnityEngine;
using System.Collections;

using Pathfinding;

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
	public GameObject lastLocation;
	
	//pathing
	public Path path;
	public float speed = 100;
	public float nextWaypointDistance = 3;
	private int currentWaypoint = 0;
	
	// Call this to remotely set interest
	public void SetInterest(GameObject other){
		interest = other;
	}
	public void SetInterest(){
		if(seeAggroTarget){
			if(interest != aggroDetector.GetComponent<aggroController>().aggroTarget){
				SetInterest(aggroDetector.GetComponent<aggroController>().aggroTarget);
			}
		}
		else{
			//TODO determine something to use as interest and set it
			if(interest != lastLocation){
				SetInterest(lastLocation);
			}
			//Do a bunch of raycasts?  Expand a collision box?
		}
		//Debug.Log(interest.name);
	}
	
	// Call this to move towards a GameObject
	public void MoveTo (GameObject other){
		toTarget = other.transform.position - self.transform.position;
		float translation = 0f;
		//Debug.Log(toTarget.magnitude);
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
		
		//Update the path after every waypoint
		if(currentWaypoint >= 4 || currentWaypoint >= path.vectorPath.Count){
			self.GetComponent<Seeker>().StartPath(transform.position, other.transform.position, OnPathComplete);
		}
		MoveOnPath();
	}
	
	public void OnPathComplete (Path p) {
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}
	// Pathing
	private void MoveOnPath () {
		if (path == null){
			Debug.Log("NO PATH!");
			return;
			//there is no path
			//This should never happen
		}
		if (currentWaypoint >= path.vectorPath.Count){
			currentWaypoint++;
			return;
			//reached end of path
		}
		
		//Rotate the character
		Vector3 tempV = new Vector3(interest.transform.position.x,
									transform.position.y,
									interest.transform.position.z
									);
		transform.LookAt(tempV);
		
		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
		dir *= speed * Time.deltaTime;
		self.GetComponent<CharacterController>().SimpleMove(dir);
		
		//Move towards next waypoint if close enough
		if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) <
		nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
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
		self.GetComponent<Seeker>().StartPath(transform.position, interest.transform.position, OnPathComplete);
		Debug.Log((path==null));
	}
	public virtual void Update(){
		Debug.Log((path==null));
		
		seeAggroTarget = aggroDetector.GetComponent<aggroController>().seen;
		SetInterest();
		MoveTo(interest);
		Debug.Log(seeAggroTarget);
		anim.SetFloat("moveV", moveV);
		anim.SetFloat("moveH", moveH);
	}
}
