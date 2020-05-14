using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPreguntasMgr : MonoBehaviour {

	public static string sistOseoSelected;
	public static int puntaje;
	public static int cantPreguntas;
	public static bool isUno;

	// Use this for initialization
	void Start () {
		isUno = false;
		puntaje = 0;
		cantPreguntas = 0;
		DontDestroyOnLoad (transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKey ("escape")) && (string.Compare(SceneManager.GetActiveScene ().name,"MenuPreguntas") == 0)) {
			SceneManager.LoadScene ("MainMenu");
		}

		if ((Input.GetKey ("escape")) && (string.Compare(SceneManager.GetActiveScene ().name,"Preguntas") == 0)) {
			SceneManager.LoadScene ("MenuPreguntas");
		}
	}

	public void SetSistemaOseo(string sistOseo){
		sistOseoSelected = sistOseo;
		SceneManager.LoadScene ("Preguntas");
	}
}
