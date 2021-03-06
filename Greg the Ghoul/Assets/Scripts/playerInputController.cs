﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

public class playerInputController : MonoBehaviour {
    public float speed = 2.0f;
	private bool horizontalPlaneLock = false;
	private bool wasGrounded = true;
	private int jumpLock = 0;
	private int jumpLockMax = 10;
    public Animator anim;
	private float distToGround;
	private int jumpLimiter = 10;
	private int jumpFrame = 0;
	private bool attacking = false;
	private GameObject[] interactables = {};
	public Collider hitbox;
	public float lastHit = 0;
	
	//UI objects 
	public GameObject InteractText;
	public Slider health;
	public Slider mana;
	private bool youDead = false;
	
	
	//Weapons
	public ParticleSystem plague;
	public GameObject playerPrefab;
	public GameObject summon;
	public GameObject painProject;
	public bool snakeStaffActive = false;
	public bool staffPainActive = false;
	public GameObject weapon1;
	public GameObject weapon2;
	public GameObject weapon3;
	public GameObject weapon4;
	public GameObject weapon5;
	private bool weapon1b = false;
	private bool weapon2b = false;
	private bool weapon3b = false;
	private bool weapon4b = false;
	private bool weapon5b = false;
	
	public GameObject groundWeapon1;
	public GameObject groundWeapon2;
	public GameObject groundWeapon3;
	public GameObject groundWeapon4;
	public GameObject groundWeapon5;
	
	//input
    private float inputH;
    private float inputV;
	private bool inputSprint;
	private bool inputJump;
	private bool inputEscape;
	//Particles
	public ParticleSystem painDark;
	public ParticleSystem lightning;
	public AudioSource LightStrike;
	public AudioSource painCast;
	
	private bool IsGrounded(){
		if (!wasGrounded){
			jumpLock = jumpLockMax;
		}
		return Physics.Raycast(transform.position, -Vector3.up, (distToGround)) &&
		GetComponent<Rigidbody>().velocity.y <= 1 && GetComponent<Rigidbody>().velocity.y >= -1;
	}
	
	private bool InAir(){
		float temp = distToGround/4;
		return !Physics.Raycast(transform.position, -Vector3.up, temp);
	}
	
	//Handle interactions with other gameobjects
	public void addInteractable(GameObject interactable){
		for(int i = 0; i < interactables.Length; i++){
			if(interactables[i] == interactable){
				return;
			}
		}
		GameObject[] temp = interactables;
		interactables = new GameObject[interactables.Length + 1];
		//Debug.Log(interactables.Length);
		for(int i = 0; i < temp.Length; i++){
			interactables[i] = temp[i];
			//Debug.Log(interactables[i].name);
		}
		interactables[interactables.Length-1] = interactable;
		//Debug.Log(interactables[interactables.Length-1].name);
		//Debug.Log("added");
		//put code to change gui here (you know that 1 interactable has been added)
	}
	
	public void removeInteractable(GameObject interactable){
		//Debug.Log("Remove called");
		bool inArray = false;
		for(int i = 0; i < interactables.Length; i++){
			if(interactable == interactables[i]){
				inArray = true;
			}
		}
		if(!inArray){
			return;
		}
		GameObject[] temp = new GameObject[interactables.Length];
		for(int i = 0; i < interactables.Length; i++){
			temp[i] = interactables[i];
		}
		interactables = new GameObject[interactables.Length - 1];
		int j = 0;
		for(int i = 0; i < temp.Length; i++){
			if(temp[i] == interactable){
				continue;
			}
			interactables[j] = temp[i];
			//Debug.Log(interactables[j].name);
			j++;
		}
		//Debug.Log("removed");
		//put code to change gui here (you know that 1 interactable has been removed)
	}
	
	void weaponSwitch (GameObject weapon){
		if(weapon.activeSelf){
			weapon.SetActive(false);
			staffPainActive = false;
			snakeStaffActive = false;
			return;
		}
		weapon1.SetActive(false);
		weapon2.SetActive(false);
		weapon3.SetActive(false);
		weapon4.SetActive(false);
		weapon5.SetActive(false);

		switch (weapon.name){
			case "Snake_Staff":
				if(weapon4b == true){
					weapon4.SetActive(true);
					snakeStaffActive = true;
				}
				break;
			case "StaffOfPain":
				if(weapon5b == true){
					weapon5.SetActive(true);
					staffPainActive = true;
				}
				break;
			case "sword_01":
				if(weapon1b == true){
					weapon1.SetActive(true);
				}
				break;
			case "mace_01":
				if(weapon2b == true){
					weapon2.SetActive(true);
				}
				break;
			case "axe_01":
				if(weapon3b == true){
					weapon3.SetActive(true);
				}
				break;
			default:
				break;
		}
	}
	
	void weaponPickUp(GameObject weapon){
		if(!(weapon.tag == "ground_weapon")){
			return;
		}
		switch (weapon.name){
			case "Snake_Staff":
				weapon4b = true;
				break;
			case "StaffOfPain":
				weapon5b = true;
				break;
			case "sword_01":
				weapon1b = true;
				break;
			case "mace_01":
				weapon2b = true;
				break;
			case "axe_01":
				weapon3b = true;
				break;
			default:
				break;
		}
		Destroy(weapon);
	}
	
    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
		lightning = GetComponent<ParticleSystem>();
		LightStrike = GetComponent<AudioSource>();
		distToGround = hitbox.bounds.extents.y;
		//Debug.Log(distToGround);
		anim.Play("surface");
	}
	
	// Update is called once per frame
	void Update () {
		//input
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
		inputSprint = Input.GetButton("Sprint");
		inputJump = Input.GetButton("Jump");
		inputEscape = Input.GetButton("Escape");
		
		anim.SetBool("inputJump", inputJump);
		anim.SetBool("inAir", InAir());
        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);
		anim.SetFloat("velocityUp", GetComponent<Rigidbody>().velocity.y);
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed/2;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        if (inputEscape) {
            Cursor.lockState = CursorLockMode.None;
			Application.Quit();
        }
		//Health and mana control
		if(health.value <= 0){
			youDead = true;
			Debug.Log("Checkpoint screen here cause you dead");
		}
		//picking up weapons
		if(Input.GetButtonDown("interact")){
			if(interactables.Length > 0){
				weaponPickUp(interactables[0]);
				removeInteractable(interactables[0]);	
			}
		}
		
		//weapon switching
		if(Input.GetButtonDown("weapon1")){
			weaponSwitch(weapon1);
		}
		else if(Input.GetButtonDown("weapon2")){
			weaponSwitch(weapon2);
		}
		else if(Input.GetButtonDown("weapon3")){
			weaponSwitch(weapon3);
		}
		else if(Input.GetButtonDown("weapon4")){
			weaponSwitch(weapon4);
		}
		else if(Input.GetButtonDown("weapon5")){
			weaponSwitch(weapon5);
		}
		
		//Sprinting
		if (inputV > 0f && inputSprint){
			anim.SetBool("inputShift", true);
			speed = 4.0f;
		}
		else{
			anim.SetBool("inputShift", false);
			speed = 2.0f;
		}
		
		//Jumping
		if (jumpFrame <= 0 && inputJump && IsGrounded() && !(jumpLock > 0)){
			jumpFrame = jumpLimiter;
			GetComponent<Rigidbody>().AddForce(Vector3.up * 1000, ForceMode.Impulse);
		}
		if (jumpFrame >= 0){
			jumpFrame--;
			anim.SetBool("inAir", true);
		}
		if (jumpLock > 0){
			jumpLock--;
		}
		
		
		//Casting
        if (Input.GetMouseButtonDown(1) && staffPainActive && (anim.GetCurrentAnimatorStateInfo(0).IsName("idle")
				|| anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") ||
				anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))) {
			lightning.Play();
			lightning.enableEmission = true;
			LightStrike.Play();
			anim.Play("standing_1H_cast_spell_01");
			Instantiate(summon, playerPrefab.transform.position+(transform.forward*2), playerPrefab.transform.rotation);
			Instantiate(summon, playerPrefab.transform.position+(transform.forward*2)+(transform.right / 1), playerPrefab.transform.rotation);
			Instantiate(summon, playerPrefab.transform.position+(transform.forward*2)+(transform.right / 2), playerPrefab.transform.rotation);
			Instantiate(summon, playerPrefab.transform.position+(transform.forward*2)+(transform.right / -1), playerPrefab.transform.rotation);
			Instantiate(summon, playerPrefab.transform.position+(transform.forward*2)+(transform.right / -2), playerPrefab.transform.rotation);
			mana.value -= 0.5f;
			//wallSummon = true;
        }
		 if (Input.GetMouseButtonDown(1) && snakeStaffActive && (anim.GetCurrentAnimatorStateInfo(0).IsName("idle")
				|| anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") ||
				anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))) {
			plague.Play();
			plague.enableEmission = true;
			anim.Play("standing_1H_cast_spell_01");
			Instantiate(summon, playerPrefab.transform.position+(transform.forward*2), playerPrefab.transform.rotation);
			mana.value -= 0.3f;
        }
		
		//Right handed melee weapon attack
        if (!attacking && Input.GetMouseButton(0) && staffPainActive == false && snakeStaffActive == false && (anim.GetCurrentAnimatorStateInfo(0).IsName("idle")
				|| anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") ||
				anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))) {
			//TODO Do particle effects for spawning weapon in hand
			//TODO spawn weapon in hand/make visible
			anim.Play("R_1H_Attack");
			anim.SetTrigger("R 1H attack2");
        }
		// Left click ability for staff of pain
		 if (!attacking && Input.GetMouseButton(0) && staffPainActive && (anim.GetCurrentAnimatorStateInfo(0).IsName("idle")
				|| anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") ||
				anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))) {
					anim.Play("Standing_2H_Magic_Attack_01");
					painCast.Play();
					painDark.Play();
					painDark.enableEmission = true;
					GameObject painClone = (GameObject)Instantiate(painProject, playerPrefab.transform.position+(transform.forward*1)+(transform.up*1), playerPrefab.transform.rotation);
		
			}
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("R_1H_Attack") ||
			anim.GetCurrentAnimatorStateInfo(0).IsName("R_1H_Attack2")|| 
			anim.GetCurrentAnimatorStateInfo(0).IsName("Standing_2H_Magic_Attack_01")){
			attacking = true;
		}
		//Continue attacking if held down (or mouse click timed well)
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("R_1H_Attack2")) {
			anim.SetTrigger("R 1H attack");
			attacking = true;
        }
		
		//Make currently selected weapon visible if using it
		if (attacking && !(anim.GetCurrentAnimatorStateInfo(0).IsName("R_1H_Attack") ||
		anim.GetCurrentAnimatorStateInfo(0).IsName("R_1H_Attack2"))){
			//TODO logic for making weapons invisible
			attacking = false;
		}
		
		//Check for reasons to lock horizontal plane position
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("surface") || anim.GetCurrentAnimatorStateInfo(0).IsName("standing_1H_cast_spell_01") ||
			anim.GetCurrentAnimatorStateInfo(0).IsName("Standing_2H_Magic_Attack_01")|| attacking){
			horizontalPlaneLock = true;
		}
		
		//Lock the character's horizontal plane position.
		if (horizontalPlaneLock){
			horizontalPlaneLock = !horizontalPlaneLock;
			translation = 0f;
			straffe = 0f;
		}
		
		//Move the character on the horizontal planez
        transform.Translate(0, 0, translation);
		transform.Translate(straffe, 0, 0);
		
		//Check for interactable objects near player
		
		
		wasGrounded = IsGrounded();
	}
	void tookDamage(float ouch){
		if(Time.time - lastHit > 1.3){
			lastHit = Time.time;
			health.value -= ouch;
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag =="damage_source"){
			tookDamage(0.05f);
		}
		if(other.gameObject.tag =="enemy_weapon"){
			if(other.gameObject.GetComponent<heldWeaponScript>().heldBy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack")){
				tookDamage(.2f);
			}
		}
		
	}
	void OnTriggerStay(Collider other){
		if(other.gameObject.tag == "interactable"){
			InteractText.GetComponent<Text>().enabled = true;
		}
		else{
			InteractText.GetComponent<Text>().enabled = false;
		}
		
	}
			
}