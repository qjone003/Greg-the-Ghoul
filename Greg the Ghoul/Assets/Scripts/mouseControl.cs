using UnityEngine;
using System.Collections;

public class mouseControl : MonoBehaviour {
	private Vector2 md;
    public float sensitivity = 5.0f;
	public Transform target;
	public Transform self;
	public float distance;
	private float newDistance;
	private float distanceChange = 0;
	private Vector3 distanceCorrection;
	
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
		transform.LookAt(target);
		newDistance = Vector3.Distance(self.position, target.position);
		distanceChange = newDistance - distance;
		distanceCorrection = new Vector3(0, 0, distanceChange);
		transform.Translate(distanceCorrection);
		
		
	}
}
