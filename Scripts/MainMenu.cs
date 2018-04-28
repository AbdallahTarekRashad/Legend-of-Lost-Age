using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

	public void PlayGame(string SceneName){
		SceneManager.LoadScene (SceneName);
	}

	public void QuiteGame(){
		Application.Quit ();
	}
}
