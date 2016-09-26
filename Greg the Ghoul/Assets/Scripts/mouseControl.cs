using UnityEngine;
using System.Collections;

public class mouseControl : MonoBehaviour {
    Vector2 mouseLook;
    Vector2 smoothV;
	private bool cameraControlled = false;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    GameObject ghoul;
	// Use this for initialization
	void Start () {
        ghoul = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(cameraControlled){
			var md = new Vector2(Input.GetAxisRaw("Mouse X"),Input.GetAxisRaw("Mouse Y"));

			md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
			smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
			smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
			mouseLook += smoothV;
			mouseLook.y = Mathf.Clamp(mouseLook.y, this.transform.rotation.x, this.transform.rotation.x);

			ghoul.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, ghoul.transform.up);
		}
		else if(ghoul.anim.GetCurrentAnimatorStateInfo(0).IsName("surface")){
			continue;
		}
	}
}
