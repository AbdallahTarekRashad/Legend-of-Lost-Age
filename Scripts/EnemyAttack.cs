using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	private float range = 5f;
	private float timeBetweenAttacks = 1f;
	private BoxCollider[] boxC;
	private GameObject player;
	private bool playerInRange = false;
	private Animator anim;
	EnemyHealth enemyHealth;

	// Use this for initialization
	void Start () {
		player = GameManager.instance.Player;
		boxC = GetComponentsInChildren<BoxCollider> ();
		anim = GetComponent<Animator> ();
		enemyHealth = GetComponent<EnemyHealth> ();
		StartCoroutine (attack ());
	}
	
	// Update is called once per frame
	void Update () {
		// hna hy4of lw L Hero 2oryp mno wla l2
		if (Vector3.Distance (transform.position, player.transform.position) < range) {
			playerInRange = true;
		} else {
			playerInRange = false;
		}
	}

	IEnumerator attack() {
		//hna b2a lw 2oryp hy ATTACK
		if (playerInRange && !GameManager.instance.GameOver () && !enemyHealth.EnemyDie()) {
				anim.Play ("Attack");
				yield return new WaitForSeconds (timeBetweenAttacks);
			}
			yield return null;
			StartCoroutine (attack ());
	}


	// dol b2a (events) mwgooda fi L Animator 
	// di bt5li L (BoxCollider) bt3t L (weapon) enabled 3nd L l7za di (EnemyBeginAttack) w di (EnemyEndAttack)
	// w dol 3l4an L (Box Collider) bt3t L (weapon) hya elli bt2sr 3la L (Hero) w bst3mlha fi function L (Hurt)

	public void EnemyBeginAttack () {
		foreach (var weapon in boxC) {
			weapon.enabled = true;
		}
	}
	public void EnemyEndAttack () {
		foreach (var weapon in boxC) {
			weapon.enabled = false;
		}
	}


}
