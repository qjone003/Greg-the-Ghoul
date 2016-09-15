using UnityEngine;
using UnityEditor;
using System.Collections;

public class playerInputController : MonoBehaviour {
    public float speed = 2.0f;
	private int castFrame = 0;
	private bool casting = false;
	private int castingMaxFrames = 120;
    public Animator anim;
    private float inputH;
    private float inputV;
	private bool shiftHeld;
	public ParticleSystem lightning;
	public AudioSource LightStrike;
    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
		lightning = GetComponent<ParticleSystem>();
		LightStrike = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed/2;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        if (Input.GetKeyDown("escape")) {
            Cursor.lockState = CursorLockMode.None;
			Application.Quit();
			
        }
		if (Input.GetKeyDown(KeyCode.LeftShift)){
			anim.SetBool("inputShift", true);
			shiftHeld = true;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift)){
			anim.SetBool("inputShift", false);
			shiftHeld = false;
		}
		if (inputV > 0f && shiftHeld){
			speed = 4.0f;
		}
		else{
			speed = 2.0f;
		}
		
        if (Input.GetMouseButtonDown(1)) {
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
		
		//Move the character.
        transform.Translate(0, 0, translation);
		transform.Translate(straffe, 0, 0);
	}
}