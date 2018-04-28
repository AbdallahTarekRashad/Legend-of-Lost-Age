using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	[SerializeField] private float StartHealth = 100;
	[SerializeField] private float timesincelastHit = 3f;
	[SerializeField]private ParticleSystem bloodFlow;
	private float currentHealth;
	private float Timer = 0f;
	private Animator anim;
	private PlayerAttack attack;
	public Image healthbar;
	// Use this for initialization
	void Start () {
		currentHealth = StartHealth;
		anim = GetComponent<Animator> ();
		attack = GetComponent<PlayerAttack> ();
	}
	
	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime;
	}
	void OnTriggerEnter(Collider other){
		if (Timer >= timesincelastHit && !GameManager.instance.GameOver()) {
		if (other.tag == "wepon") {
			TakeHit ();
			Timer = 0;
		}
	}
}
	void TakeHit (){
		if (currentHealth > 0) {
			bloodFlow.Play ();
			GameManager.instance.PlayerHit (currentHealth);
			currentHealth -= 10;
			healthbar.fillAmount = currentHealth / StartHealth;
			//print (healthbar.fillAmount);
			if (!GameManager.instance.BattleMode) {
				anim.Play ("Hit");
			} else if (GameManager.instance.BattleMode && !GameManager.instance.PlayerAttack){
				if (attack.Sword.activeInHierarchy == true) {
					anim.Play ("Hit_sword");
				}else if (attack.Staff.activeInHierarchy == true){
					anim.Play ("Hit_staff");
				}
			}

		}
		if (currentHealth <= 0) {
			killPlayer ();
		}
	}
	void killPlayer(){
		GameManager.instance.PlayerHit (currentHealth);
		anim.SetTrigger ("Die");
		//control.enabled = false;
		attack.enabled = false;
		//StartCoroutine(Loadafteranimation());
		}

	/*public IEnumerator Loadafteranimation(){
		//yield return new WaitForSeconds (anim2.time);
		//SceneManager.LoadScene ("GameOver");
	}*/
}