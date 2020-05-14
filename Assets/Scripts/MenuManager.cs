using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuManager : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("escape")) {
				Application.Quit ();
		}
	}

	public void LoadScenes(string sceneToLoad){
		SceneManager.LoadScene (sceneToLoad);
	}
}
