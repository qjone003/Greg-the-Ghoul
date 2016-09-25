using UnityEngine;
using System.Collections;

public class mouseControl : MonoBehaviour {
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
	public Transform target;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		/*
        var md = new Vector2(Input.GetAxisRaw("Mouse X"),Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;
       
        //mouseLook.y = Mathf.Clamp(mouseLook.y, 0f, 0f);
        transform.localRotation = Quaternion.AngleAxis(mouseLook.y, Vector3.right);
        ghoul.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, ghoul.transform.up);
		*/
		transform.LookAt(target);
	}
}
