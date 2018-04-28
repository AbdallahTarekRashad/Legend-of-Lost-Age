using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameMenu : MonoBehaviour {

	public void play(){
		SceneManager.LoadScene (1);
	}
	public void Restart(){
		SceneManager.LoadScene ("gameMenu");
	}
}
