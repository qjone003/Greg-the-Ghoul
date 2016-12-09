using UnityEngine;
using System.Collections;
using System;
using System.Linq;


public class enemyBaseController : MonoBehaviour {
	public Animator anim;
	public float moveV = 0f;
	public float moveH = 0f;
	public GameObject interest;
	public GameObject self;
	public GameObject aggroDetector;
	public Vector3 toTarget;
	public bool seeAggroTarget = false;
	public bool aggroAware = false;
	public float walkSpeed = 1f;
	public float runMultiplier = 1.5f;
	public float minRunDistance = 3.5f;
	public float minWalkDistance = 1f;
	public GameObject lastLocation;
	public NavMeshAgent navMeshAgent;
	public float disToAttack = .5f;
	public int health = 100;
	
	//pathing
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
		else if(aggroAware){
			SetInterest(lastLocation);
		}
		else{
			SetInterest(interest);
			//TODO determine something to use as interest and set it
			//Do a bunch of raycasts?  Expand a collision box?
		}
		//Debug.Log(interest.name);
	}
	
	private IEnumerator FollowTarget(){
		Vector3 previousTargetPosition = new Vector3(float.PositiveInfinity,
		float.PositiveInfinity);
		while(Vector3.SqrMagnitude(transform.position - interest.transform.position) > 0.1f){
			if(Vector3.SqrMagnitude(previousTargetPosition - interest.transform.position) > 0.1f){
				navMeshAgent.SetDestination(interest.transform.position);
				previousTargetPosition = interest.transform.position;
			}
			yield return new WaitForSeconds(0.1f);
		}
		yield return null;
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
	public void Get_hit (int damage) {
		health = health-damage;
		if(health <= 0){
			Die();
			Destroy(self,2);
			return;
		}
		anim.SetTrigger("damaged");
	}
	
	// Use this for initialization
	public virtual void Start () {
		navMeshAgent.SetDestination(interest.transform.position);
		StartCoroutine(FollowTarget());
	}
	public virtual void Update(){
		
		aggroAware = aggroDetector.GetComponent<aggroController>().aware;
		seeAggroTarget = aggroDetector.GetComponent<aggroController>().seen;
		SetInterest();
		
		moveH = (Math.Abs(navMeshAgent.velocity.x) + Math.Abs(navMeshAgent.velocity.z))/navMeshAgent.speed;
		try{
		if(Vector3.Distance(interest.transform.position, self.transform.position) <= disToAttack){
			if(aggroDetector.GetComponent<aggroController>().enemies.Contains(interest.tag)){
				Attack();
			}
		}
		}
		catch{
		}
		anim.SetFloat("moveV", moveV);
		anim.SetFloat("moveH", moveH);
	}
}
