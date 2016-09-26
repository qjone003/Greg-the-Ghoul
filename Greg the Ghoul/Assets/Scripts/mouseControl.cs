using UnityEngine;
using System.Collections;

public class mouseControl : MonoBehaviour {
	Vector2 md;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
	public Transform target;
	public Transform self;
	public float distance;
	// Use this for initialization
	void Start () {
		distance = Vector3.Distance(self.position, target.position);
	}
	
	// Update is called once per frame
	void Update () {
		print(distance);
        md = new Vector2(Input.GetAxisRaw("Mouse X") * sensitivity,Input.GetAxisRaw("Mouse Y") * sensitivity);
        
		transform.LookAt(target);
		transform.Translate(md * Time.deltaTime);
		distance = Vector3.Distance(self.position, target.position);
		//TODO force the camera distance
	}
}
