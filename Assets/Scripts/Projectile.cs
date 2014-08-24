using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float force = 100f;

	public GameObject explosionParticle;
	//public GameObject trailParticle;

	void Start () {
	
	}

	void Update () {
		rigidbody.AddForce (transform.forward * force);
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Enemy") {
			collision.gameObject.SendMessage("TakeDamage", 1f);
			GameObject t_obj = (GameObject)Instantiate(explosionParticle, transform.position, Quaternion.identity);
			Destroy(t_obj, t_obj.particleSystem.duration);
			Destroy(gameObject);
		}
	}
}