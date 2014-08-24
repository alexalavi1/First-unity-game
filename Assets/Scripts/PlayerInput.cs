using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	NavMeshAgent agent;

	public int maxHealth = 4;
	int health;

	public GameObject fireBall;

	void Start () {
		health = maxHealth;
		agent = GetComponent<NavMeshAgent> ();
	}

	void Update () {
		if(Input.GetButton("Fire1")) {
			RaycastHit hitman;
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitman)) {
				agent.SetDestination(hitman.point);
			}
		}
		if(Input.GetButtonDown("Fire2")) {
			agent.Stop();
			animation.Play("Attack");
			RaycastHit hitman;
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitman)) {
				Collider[] hits = Physics.OverlapSphere(transform.position, 3.0f);

				for(int i = 0; i < hits.Length; i++) {
					if(hits[i].tag == "Enemy")
						hits[i].SendMessage("TakeDamage", 1, SendMessageOptions.DontRequireReceiver);
				}

				transform.LookAt(new Vector3(hitman.point.x, transform.position.y, hitman.point.z));
			}
		}
		if(Input.GetButtonDown("Ability1")) {
			RaycastHit hitman;
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitman)) {
				transform.LookAt(new Vector3(hitman.point.x, transform.position.y, hitman.point.z));
				GameObject t_obj = (GameObject)Instantiate(fireBall, transform.position + transform.forward, Quaternion.identity);
				t_obj.transform.LookAt(hitman.point);
			}
		}
	}

	public void TakeDamage(int damage) {
		health -= damage;
		if(health < damage)
			Destroy(gameObject);
	}
}
