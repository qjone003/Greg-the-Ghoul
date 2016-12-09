using UnityEngine;
using System.Collections;

public class skeletonRes : MonoBehaviour {
	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle2")){
			int x = Random.Range(0,5);
			switch(x){
				case 0:
					anim.Play("SwingHeavy");
					break;
				case 1:
					anim.Play("SwingNormal");
					break;
				case 2:
					anim.Play("SwingQuick");
					break;
				case 3:
					anim.Play("Idle");
					break;
				case 4:
					anim.Play("Hit");
					break;
				case 5:
					anim.Play("Hit2");
					break;
				default:
					break;
			}	
		}
	}
}
