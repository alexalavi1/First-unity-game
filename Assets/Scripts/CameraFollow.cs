using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject target;
	public Vector3 offset;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(target) {
			transform.position = target.transform.position + offset;
			transform.LookAt (target.transform.position);
		}
	}
}
