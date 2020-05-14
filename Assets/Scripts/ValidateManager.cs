using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ValidateManager : MonoBehaviour {

	string huesoName;
	PreguntasManager preguntasManager;
	Text mensaje;
	static int intentos;
	static bool nextLevel;
	MeshRenderer[] renderers;

	// Use this for initialization
	void Start () {
		intentos = 3;
		nextLevel = false;
		mensaje = GameObject.Find ("Mensaje").GetComponent<Text> ();
		preguntasManager = GameObject.FindObjectOfType(typeof(PreguntasManager)) as PreguntasManager;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){
		huesoName = transform.name;

		if (preguntasManager.aleatorio == 1) {
			renderers = GameObject.Find ("BonesCraneo-Explotado").GetComponentsInChildren <MeshRenderer> ();
		}else if(preguntasManager.aleatorio == 2){
			renderers = GameObject.Find ("BonesTorso").GetComponentsInChildren <MeshRenderer> ();
		}

		foreach(MeshRenderer r in renderers){		
			if (String.Equals (r.name, transform.name) && r.enabled && intentos > 0 && !nextLevel) {

				if (preguntasManager.boneToSelectId != Int32.Parse (huesoName)) {
					intentos--;
					mensaje.text = "Incorrecto, te quedan " + intentos + " intentos!!";
				} else {
					mensaje.text = "Correcto!!";
					nextLevel = true;
					MenuPreguntasMgr.puntaje++;
					preguntasManager.nextButton.SetActive (true);
				}
			}
			if (intentos == 0) {
				mensaje.text = "Incorrecto. Siguiente pregunta...";
				preguntasManager.nextButton.SetActive (true);
			}
		}

		mensaje.enabled = true;

	}

	public void botonValidate (int idSelected){
		huesoName = GameObject.Find ("Opcion" + idSelected + "Text").GetComponent<Text> ().text;

		if (!nextLevel) {

			if (String.Compare (preguntasManager.nameToSelect, huesoName) != 0) {
				mensaje.text = "Incorrecto. Siguiente pregunta...";
				nextLevel = true;
				disableOption ();
			} else {
				mensaje.text = "Correcto!!";
				MenuPreguntasMgr.puntaje++;
				nextLevel = true;
				disableOption ();
			}
		}

		mensaje.enabled = true;
		preguntasManager.nextButton.SetActive (true);
	}

	public void nextLevelFunction (){
		SceneManager.LoadScene ("Preguntas");
	}

	public void disableOption (){
		for(int i = 1; i < 5; i++){
			GameObject.Find ("Opcion" + i).GetComponent<Button> ().enabled = false;
		}
	}
}
