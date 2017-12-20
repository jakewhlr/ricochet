using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {
	public void LoadSceneByIndex(int i){
		SceneManager.LoadScene (i);
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
