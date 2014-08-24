using UnityEngine;
using System.Collections;

public class EnemyInput : MonoBehaviour {

	NavMeshAgent agent;

	GameObject player;

	public float attackRange = 2.0f;

	public int maxHealth = 4;
	int health;

//	GUITexture healthTextureFront, healthTextureBack;
//
//	public Texture2D healthTex;

	bool attacked = false;
	bool gotHit = false;

	public float invulTime = 0.6f;

	void Start () {
		player = GameObject.Find ("Player");
		agent = GetComponent<NavMeshAgent> ();

		health = maxHealth;

//		Rect t_rect = new Rect (-15.0f, 20.0f, 30.0f, 7.5f);
//
//		GameObject t_obj = new GameObject ();
//		t_obj.AddComponent<GUITexture> ();
//		t_obj.transform.position = Vector3.one / 2.0f;
//		t_obj.transform.localScale = new Vector3 (0f, 0f, 1f);
//
//		healthTextureFront = t_obj.guiTexture;
//		healthTextureFront.pixelInset = t_rect;
//		healthTextureFront.texture = healthTex;
//		healthTextureFront.color = Color.green;
//
//		GameObject t_obj2 = new GameObject ();
//		t_obj2.AddComponent<GUITexture> ();
//		t_obj2.transform.position = Vector3.one / 2.0f;
//		t_obj2.transform.localScale = new Vector3 (0f, 0f, 1f);
//
//		healthTextureBack = t_obj2.guiTexture;
//		healthTextureBack.pixelInset = t_rect;
//		healthTextureBack.texture = healthTex;
//		healthTextureBack.color = Color.red;
	}

	void Update () {
		if(player) {
			agent.SetDestination (player.transform.position);
			if(!attacked) {
				if(Vector3.Distance(transform.position, player.transform.position) < attackRange) {
					animation.Play("Attack");
					player.SendMessage("TakeDamage", 1);
					StartCoroutine("AttackCD");
				}
			}
		}
//		healthTextureFront.transform.position = Camera.main.WorldToViewportPoint (transform.position);
//		healthTextureBack.transform.position = Camera.main.WorldToViewportPoint (transform.position);
	}

	IEnumerator AttackCD() {
		attacked = true;
		yield return new WaitForSeconds (animation ["Attack"].length);
		attacked = false;
	}

	IEnumerator InvulCD() {
		gotHit = true;
		collider.enabled = false;
		yield return new WaitForSeconds (invulTime);
		gotHit = false;
		collider.enabled = true;
	}

//	void OnGUI() {
//		Vector3 pos = Camera.main.WorldToScreenPoint (transform.position);
//		GUI.DrawTexture (new Rect (pos.x, pos.y, 30.0f, 15.0f), healthTex);
//	}

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