using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private Animator anim;
	//public RuntimeAnimatorController anim2;
	[SerializeField] private Transform centerpoint;
	[SerializeField] private float rotationspeed;
	bool foraward,back,left,right ,slow_walk ;
	int angel_to_rotate;
	// Use this for initialization

	void Awake(){
		anim = GetComponent<Animator> ();
	}

	void Start () {
		//anim.runtimeAnimatorController = anim2;
	}
	
	// Update is called once per frame
	void Update () {
		//Input for WASD
		foraward = Input.GetKey (KeyCode.W);
		back     = Input.GetKey (KeyCode.S);
		left     = Input.GetKey (KeyCode.A);
		right    = Input.GetKey (KeyCode.D);
		if (Input.GetKeyDown (KeyCode.LeftShift))
		slow_walk = !slow_walk;
		anim.SetBool ("sprint",slow_walk);

		Calculate_angle ();

		anim.SetFloat ("movement",Mathf.Max(Mathf.Abs(Input.GetAxis ("Vertical")),Mathf.Abs(Input.GetAxis ("Horizontal"))));
		//anim.SetFloat ("angel",Mathf.DeltaAngle(transform.eulerAngles.y,centerpoint.eulerAngles.y + angel_to_rotate));
		if (anim.GetCurrentAnimatorStateInfo (0).IsTag ("turnable")) {
			transform.eulerAngles += new Vector3 (0,
				Mathf.DeltaAngle(transform.eulerAngles.y,centerpoint.eulerAngles.y + angel_to_rotate)*Time.deltaTime*rotationspeed
			,0);
		}
	}

	void Calculate_angle(){
		
		if (foraward && !back) {
			if (left && !right)
				angel_to_rotate = -45;
			else if (!left && right)
				angel_to_rotate = 45;
			else
				angel_to_rotate = 0;
		}else if (!foraward && back) {
			if (left && !right)
				angel_to_rotate = -145;
			else if (!left && right)
				angel_to_rotate = 145;
			else
				angel_to_rotate = 180;
		} else {
			if (left && !right)
				angel_to_rotate = -90;
			else if (right && !left)
				angel_to_rotate = 90;
			else
				angel_to_rotate = 0;
		}
	}

	public void FootR(){
		
	}
	public void FootL(){

	}
}
