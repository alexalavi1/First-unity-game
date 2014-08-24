using UnityEngine;
using System.Collections;

public class ArcherInput : MonoBehaviour {

	NavMeshAgent agent;
	
	GameObject player;
	
	public float attackRange = 20.0f;
	
	public int maxHealth = 4;
	int health;
	
	bool attacked = false;
	bool gotHit = false;
	
	public float invulTime = 0.6f;

	public GameObject projectile;

	public float attackCD = 1.6f;
	
	void Start () {
		player = GameObject.Find ("Player");
		agent = GetComponent<NavMeshAgent> ();
		
		health = maxHealth;
	}
	
	void Update () {
		if(player) {
			if(Vector3.Distance(player.transform.position, transform.position) > attackRange) {
				agent.SetDestination (player.transform.position);
			} else {
				agent.Stop();
				if(!attacked) {
					animation.Play("Attack");
					transform.LookAt(player.transform.position);
					GameObject t_obj = (GameObject)Instantiate(projectile, transform.position + transform.forward, Quaternion.identity);
					t_obj.transform.LookAt(player.transform.position);
					StartCoroutine("AttackCD");
				}
			}
		}
	}
	
	IEnumerator AttackCD() {
		attacked = true;
		yield return new WaitForSeconds (attackCD);
		attacked = false;
	}
	
	IEnumerator InvulCD() {
		gotHit = true;
		collider.enabled = false;
		yield return new WaitForSeconds (invulTime);
		gotHit = false;
		collider.enabled = true;
	}
	
	public void TakeDamage(int damage) {
		if(!gotHit) {
			health -= damage;
			if(health < damage)
				Destroy(gameObject);
			else
				StartCoroutine("InvulCD");
		}
	}
}
