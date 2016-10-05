using UnityEngine;
using UnityEditor;
using System.Collections;

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
	
	//Weapons
	public GameObject weapon1;
	public GameObject weapon2;
	public GameObject weapon3;
	public GameObject weapon4;
	public GameObject weapon5;
	
	//input
    private float inputH;
    private float inputV;
	private bool inputSprint;
	private bool inputJump;
	private bool inputEscape;
	
	public ParticleSystem lightning;
	public AudioSource LightStrike;
	
	private bool IsGrounded(){
		if (!wasGrounded){
			jumpLock = jumpLockMax;
		}
		return Physics.Raycast(transform.position, -Vector3.up, distToGround) &&
		GetComponent<Rigidbody>().velocity.y <= 1 && GetComponent<Rigidbody>().velocity.y >= -1;
	}
	void weaponSwitch (GameObject weapon){
		if(weapon.activeSelf){
			weapon.SetActive(false);
			return;
		}
		weapon1.SetActive(false);
		weapon2.SetActive(false);
		weapon3.SetActive(false);
		weapon4.SetActive(false);
		weapon5.SetActive(false);
		weapon.SetActive(true);
		
	}
	
    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
		lightning = GetComponent<ParticleSystem>();
		LightStrike = GetComponent<AudioSource>();
		distToGround = GetComponent<Collider>().bounds.extents.y;
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
		anim.SetBool("inAir", !IsGrounded());
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
			GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
		}
		if (jumpFrame >= 0 || !IsGrounded()){
			jumpFrame--;
			anim.SetBool("inAir", true);
		}
		if (jumpLock > 0){
			jumpLock--;
		}
		
		//Casting
        if (Input.GetMouseButtonDown(1) && (anim.GetCurrentAnimatorStateInfo(0).IsName("idle")
				|| anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") ||
				anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))) {
			lightning.Play();
			lightning.enableEmission = true;
			LightStrike.Play();
			anim.Play("standing_1H_cast_spell_01");
        }
		
		//Right handed melee weapon attack
        if (!attacking && Input.GetMouseButton(0) && (anim.GetCurrentAnimatorStateInfo(0).IsName("idle")
				|| anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") ||
				anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))) {
			//TODO Do particle effects for spawning weapon in hand
			//TODO spawn weapon in hand/make visible
			anim.Play("R_1H_Attack");
			anim.SetTrigger("R 1H attack2");
        }
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("R_1H_Attack") ||
			anim.GetCurrentAnimatorStateInfo(0).IsName("R_1H_Attack2")){
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
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("surface") || anim.GetCurrentAnimatorStateInfo(0).IsName("standing_1H_cast_spell_01")
			|| attacking){
			horizontalPlaneLock = true;
		}
		
		//Lock the character's horizontal plane position.
		if (horizontalPlaneLock){
			horizontalPlaneLock = !horizontalPlaneLock;
			translation = 0f;
			straffe = 0f;
		}
		
		//Move the character on the horizontal plane
        transform.Translate(0, 0, translation);
		transform.Translate(straffe, 0, 0);
		
		wasGrounded = IsGrounded();
	}
}