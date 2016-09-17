﻿using UnityEngine;
using UnityEditor;
using System.Collections;

public class playerInputController : MonoBehaviour {
    public float speed = 2.0f;
	private int castFrame = 0;
	private bool casting = false;
	private int castingMaxFrames = 120;
    public Animator anim;
	private float distToGround;
	private int jumpLimiter = 10;
	private int jumpFrame = 0;
	
	//input
    private float inputH;
    private float inputV;
	private bool inputSprint;
	private bool inputJump;
	private bool inputEscape;
	
	public ParticleSystem lightning;
	public AudioSource LightStrike;
	
	private bool IsGrounded(){
		return Physics.Raycast(transform.position, -Vector3.up, distToGround) &&
		GetComponent<Rigidbody>().velocity.y <= 1 && GetComponent<Rigidbody>().velocity.y >= -1;
	}
	
    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
		lightning = GetComponent<ParticleSystem>();
		LightStrike = GetComponent<AudioSource>();
		distToGround = GetComponent<Collider>().bounds.extents.y;
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
		if (jumpFrame <= 0 && inputJump && IsGrounded() && -0.1 <= GetComponent<Rigidbody>().velocity.y && GetComponent<Rigidbody>().velocity.y <= 0.1){
			jumpFrame = jumpLimiter;
			GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
		}
		if (jumpFrame >= 0){
			jumpFrame--;
			anim.SetBool("inAir", true);
		}
		
		//Casting
        if (Input.GetMouseButtonDown(1) && !casting) {
			lightning.Play();
			lightning.enableEmission = true;
			LightStrike.Play();
			anim.Play("standing_1H_cast_spell_01");
			casting = true;
        }
		
		//Lock the character's horizontal plane position if they are casting.
		if (castFrame < castingMaxFrames && casting){
			translation = 0f;
			straffe = 0f;
			castFrame++;
		}
		else{
			casting = false;
			castFrame = 0;
		}
		
		//Move the character on the horizontal plane
        transform.Translate(0, 0, translation);
		transform.Translate(straffe, 0, 0);
	}
}