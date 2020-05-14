using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultadoPreguntasMgr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find ("Puntaje").GetComponent <Text> ().text = MenuPreguntasMgr.puntaje.ToString() + " / 15";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("escape")) {
			SceneManager.LoadScene ("MainMenu");
		}
		
	}

	public void NewGame(){
		MenuPreguntasMgr.cantPreguntas = 0;
		MenuPreguntasMgr.puntaje = 0;
		MenuPreguntasMgr.isUno = false;
		SceneManager.LoadScene ("Preguntas");
	}

	public void Return(){
		SceneManager.LoadScene ("MenuPreguntas");
	}
}
