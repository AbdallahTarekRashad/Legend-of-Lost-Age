using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	private Animator animator;
	public GameObject Sword;
	public GameObject Staff;
	public float fireRate = 2;
	public string[] comboParams ;
	private int comboIndex = 0 ;
	private float resetTimer;
	private bool battlemode = false;
	private bool Attack = false;
	[SerializeField] private ParticleSystem WeponSwitch;
	//int x = 2;
	// Use this for initialization
	void Start () {
		if(comboParams == null || (comboParams != null && comboParams.Length == 0)){
			comboParams = new string[]{ "Attack1", "Attack2", "Attack3", "Attack4" ,"Attack5","Attack6","Attack7"};
		}	
		animator = GetComponent<Animator> ();
		Sword.SetActive (false);
		Staff.SetActive (false);
	}
	
	// Update is called once per frame
	void Update(){

		if (Input.GetKey (KeyCode.Keypad1)) {
			battlemode = true;
			animator.Play ("getSword");
			GameManager.instance.Battle_ModeOn (battlemode);
		}
		if (Input.GetKey (KeyCode.Keypad2)) {
			battlemode = true;
			animator.Play ("getStaff");
			GameManager.instance.Battle_ModeOn (battlemode);
		}
		if (battlemode == true) {
			if (Input.GetKey (KeyCode.E)) {
				battlemode = false;
				animator.Play ("Sheath");
				GameManager.instance.Battle_ModeOn (battlemode);
			}
		}
		if (GameManager.instance.BattleMode) {
			ComboAttack ();
		}
	}
	//Animation Events
	public void WeaponSwitch(){
		WeponSwitch.Play ();
		Sword.SetActive (true);
		Staff.SetActive (false);
	}
	public void StaffSwitch2(){
		WeponSwitch.Play ();
		Staff.SetActive (true);
		Sword.SetActive (false);
	}
	public void Hit(){
		Sword.GetComponent<BoxCollider> ().enabled = true;
		Staff.GetComponent<BoxCollider> ().enabled = true;
	}

	public void sheath(){
		Staff.SetActive (false);
		Sword.SetActive (false);
	}
	//End Animation Event
	private void ComboAttack(){
		
			if (Input.GetMouseButtonDown (0) && comboIndex < comboParams.Length) {
				Attack = true;
				GameManager.instance.Player_Attack (Attack);
			    Debug.Log (comboParams [comboIndex] + " triggered");
				animator.SetTrigger (comboParams [comboIndex]);
				comboIndex++;
				resetTimer = 0f;
			}
			if (comboIndex > 0) {
				resetTimer += Time.deltaTime;
				if (resetTimer > fireRate) {
				Attack = false;
				GameManager.instance.Player_Attack (Attack);
					animator.SetTrigger ("Reset");
					Sword.GetComponent<BoxCollider> ().enabled = false;
				    Staff.GetComponent<BoxCollider> ().enabled = false;
					comboIndex = 0;
				}
			}
		}
	}
