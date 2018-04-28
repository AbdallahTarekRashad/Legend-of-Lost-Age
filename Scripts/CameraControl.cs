using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	[SerializeField] private Transform Player_Cam,Centerpoint;
	[SerializeField] private float distance,height,orbiting_speed,verticalSpeed , Maxheghit , minheghit;
	void Update(){
		Centerpoint.position = gameObject.transform.position+ new Vector3 (0,5f,0);
		Centerpoint.eulerAngles += new Vector3 (0,Input.GetAxis("Mouse X")*Time.deltaTime*orbiting_speed,0);
		height += Input.GetAxis ("Mouse Y") * Time.deltaTime * -verticalSpeed;
		height = Mathf.Clamp (height, minheghit, Maxheghit);
	}
	void LateUpdate(){
		Player_Cam.position = Centerpoint.position + Centerpoint.forward*-1*distance+ Vector3.up*height;
		Player_Cam.LookAt(Centerpoint);
	}
}

