using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	void Update(){
		if(Input.GetKey(KeyCode.R)){
			RestartLevel();
		}
		if(Input.GetKey(KeyCode.Return)){
			NextLevel();
		}
	}

	public void NextLevel(){
		int currentLevel = SceneManager.GetActiveScene().buildIndex;
		int nextLevel = (currentLevel + 1) % SceneManager.sceneCountInBuildSettings;
		SceneManager.LoadScene(nextLevel);
	}

	public void RestartLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
