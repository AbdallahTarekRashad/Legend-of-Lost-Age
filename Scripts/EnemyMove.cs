using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {
	private Transform player;
	private NavMeshAgent nav;
	private Animator anim;
	EnemyHealth enemyHealth;


	// Use this for initialization
	void Start () {
		player = GameManager.instance.Player.transform;
		anim = GetComponent<Animator> ();
		nav = GetComponent<NavMeshAgent> ();
		enemyHealth = GetComponent<EnemyHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		//if (!GameManager.instance.EnemyDie ()) {
		if (!GameManager.instance.GameOver () && !enemyHealth.EnemyDie()) {
			nav.SetDestination (player.position); // hna b3d lma bt3ml NAVIGATION [ NavMeshAgent ] li L Enemy --> btdeh L Direction elli hym4i n7yto
		} else if (!GameManager.instance.GameOver() || GameManager.instance.GameOver() && enemyHealth.EnemyDie()) {
			nav.enabled = false;
		} else {
			nav.enabled = false;
			anim.Play ("Idle");
			//anim.SetTrigger ("HeroDie");
		}
		//} 


	}
}
