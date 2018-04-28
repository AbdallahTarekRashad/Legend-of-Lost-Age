using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

	public float currentHealth = 20;
	[SerializeField] private float timeSinceLastHit = 0.5f;

	private float timer;
	private float disappearSpeed = 2f;
	bool enemyDie = false;
	bool disappearEnemy = false;

	Animator anim;
	NavMeshAgent nav;
	Rigidbody rigidbody;
	AudioSource audioSource;
	ParticleSystem bloodFlow;

	// Use this for initialization
	void Start () {
		GameManager.instance.RegisteredEnemy (this);
		anim = GetComponent<Animator> ();
		nav = GetComponent<NavMeshAgent> ();
		audioSource = GetComponent<AudioSource> ();
		rigidbody = GetComponent<Rigidbody> ();
		bloodFlow = GetComponentInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (disappearEnemy) {
			transform.Translate (-Vector3.up * disappearSpeed * Time.deltaTime);
		}
		if (currentHealth <= 0) {
			gameObject.GetComponent<CapsuleCollider> ().enabled = false;
		}
	}


	public void EnemyHit (float currentHP) {
		if (currentHP > 0) {
			enemyDie = false;
		} else {
			enemyDie = true;
		}
	}
	public bool EnemyDie () {
		return enemyDie;
	}

	public void OnTriggerEnter(Collider other) {
		print ("Hi");
		if (timer > timeSinceLastHit && !GameManager.instance.GameOver()) {
			if (other.tag == "HeroWepon") {
				takeHit ();
				timer = 0;
			}
		}
	}

	public void takeHit() {
		EnemyHit (currentHealth);
		if (!EnemyDie ()) {
			anim.Play ("Hurt");
			currentHealth -= 10;
			audioSource.PlayOneShot (audioSource.clip);
			bloodFlow.Play ();
		} else {
			enemyKilled ();
		}
	}

	public void enemyKilled() {
		if (EnemyDie ()) {
			GameManager.instance.KilledEnemy (this);
			nav.enabled = false;
			anim.SetTrigger ("EnemyDie");
			rigidbody.isKinematic = true;
			StartCoroutine (removeEnemy ());
		}
	}

	IEnumerator removeEnemy() {
		yield return new WaitForSeconds (3f);
		disappearEnemy = true;
		yield return new WaitForSeconds (3f);
		Destroy (gameObject);
	}

}
