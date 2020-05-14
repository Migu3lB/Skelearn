using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class PreguntasManager : MonoBehaviour {

	string inicio,fin;
	public string nameToSelect;
	string[] opcion;
	public GameObject nextButton;
	GameObject [] options,huesosToValidate;
	DBManager dBManager;
	int menor = 1000, mayor = 0, actual, arrayLength, ubicacionOption,aleatorioAux;
	public int boneToSelect, boneToSelectId, aleatorio;
	Array huesos;
	System.Random rnd = new System.Random ();

	// Use this for initialization
	void Start () {
		if (MenuPreguntasMgr.cantPreguntas == 15) {
			SceneManager.LoadScene ("ResultadoPreguntas");
		} else {
			MenuPreguntasMgr.cantPreguntas++;
			nextButton = GameObject.Find ("Next");
			opcion = new string[4];
			options = new GameObject[4];
			huesosToValidate = new GameObject[2];

			GetOptions ();
			DisappearObjects ();

			dBManager = GameObject.FindObjectOfType (typeof(DBManager)) as DBManager;
			dBManager.CreateDB ("SkelAppDB.db");
			dBManager.OpenDB ();
			Array result = dBManager.MultipleSelectWhere ("Partes_Cuerpo", "*", "ID_Sistema_Oseo", "=", MenuPreguntasMgr.sistOseoSelected);
			int y = 0;

			foreach (var item in result) {
				if ((y % 3) == 0) {
					actual = Int32.Parse (item.ToString ());
					if (actual < menor) {
						menor = actual;
					}
					if (actual > mayor) {
						mayor = actual;
					}
				}
				y++;
			}
			mayor++;

			AsignarAleatorio ();

			huesos = dBManager.MultipleSelectWhere ("Huesos", "*", "ID_Partes_Cuerpo", "=", aleatorio.ToString ());
			arrayLength = huesos.GetLength (0);
			boneToSelect = rnd.Next (0, arrayLength);
			boneToSelect = rnd.Next (0, arrayLength);
			boneToSelectId = Int32.Parse (huesos.GetValue (boneToSelect, 0).ToString ());
			nameToSelect = huesos.GetValue (boneToSelect, 1).ToString ();

			if (aleatorio != 1 || aleatorio != 8 || aleatorio != 10 || aleatorio != 9) {
				ubicacionOption = rnd.Next (1, 5);
				huesos = dBManager.MultipleSelectWhere ("Huesos", "*", null, null, null);
				arrayLength = huesos.GetLength (0);

				for (int i = 0; i < 4; i++) {
					aleatorioAux = rnd.Next (1, arrayLength);
					while (i > 0 && isUsed (i, huesos.GetValue (aleatorioAux, 1).ToString ()) && string.Compare (huesos.GetValue (aleatorioAux, 1).ToString (), nameToSelect) != 0) {
						aleatorioAux = rnd.Next (1, arrayLength);
					}
					opcion [i] = huesos.GetValue (aleatorioAux, 1).ToString ();
				}
			}

			dBManager.CloseDB ();

			if (MenuPreguntasMgr.cantPreguntas <= 15) {
				if (aleatorio == 1 || aleatorio == 8 || aleatorio == 10 || aleatorio == 9) {

					if (aleatorio == 1) {
						huesosToValidate [0].SetActive (true);
					} else if (aleatorio == 8) {
						huesosToValidate [1].SetActive (true);
					} else if (aleatorio == 10) {
						huesosToValidate [2].SetActive (true);
					} else if (aleatorio == 9) {
						huesosToValidate [3].SetActive (true);
					}

					Renderer[] componentes = GameObject.Find ("Huesos" + aleatorio).GetComponentsInChildren <Renderer> ();

					foreach (var r in componentes) {
						r.enabled = true;
					}

					GameObject.Find ("Question").GetComponent<Text> ().text = "Seleccione:  " + nameToSelect + ".";
			
				} else {
					Renderer[] componentes = GameObject.Find ("Huesos" + aleatorio).GetComponentsInChildren <Renderer> ();

					foreach (var r in componentes) {
						if (Int32.Parse (r.name) == boneToSelectId) {
							r.enabled = true;
						}
					}
					GameObject.Find ("Question").GetComponent<Text> ().text = "Seleccione el nombre del hueso que se muestra.";
					for (int i = 1; i < 5; i++) {
						if (ubicacionOption == i) {
							options [i - 1].SetActive (true);
							GameObject.Find ("Opcion" + i + "Text").GetComponent<Text> ().text = nameToSelect;
						} else {
							options [i - 1].SetActive (true);
							GameObject.Find ("Opcion" + i + "Text").GetComponent<Text> ().text = opcion [i - 1];
						}
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {		

	}
	
	public void AsignarAleatorio(){
		if (MenuPreguntasMgr.cantPreguntas != 1 && MenuPreguntasMgr.cantPreguntas != 4 && MenuPreguntasMgr.cantPreguntas != 7 && MenuPreguntasMgr.cantPreguntas != 10) {
			if (Int32.Parse (MenuPreguntasMgr.sistOseoSelected.ToString ()) == 1) {
				aleatorio = 8;
			} else if (Int32.Parse (MenuPreguntasMgr.sistOseoSelected.ToString ()) == 2) {
				aleatorio = 1;
			} else if (Int32.Parse (MenuPreguntasMgr.sistOseoSelected.ToString ()) == 3) {
				aleatorio = 10;
			} else if (Int32.Parse (MenuPreguntasMgr.sistOseoSelected.ToString ()) == 4) {
				aleatorio = 9;
			}
		} else {
			if (menor == 1) {
				menor++;
			}
			if (mayor == 9) {
				mayor--;
			}
			aleatorio = rnd.Next (menor, mayor);
		}
	}

	public void DisappearObjects(){
		
		GameObject.Find ("Mensaje").GetComponent<Text> ().enabled = false;

		huesosToValidate[0] = GameObject.Find ("BonesTorso");
		huesosToValidate[1] = GameObject.Find ("BonesCraneo-Explotado");

		for(int i = 1; i < 3; i++){
			Renderer[] renderers = GameObject.Find ("Huesos"+i).GetComponentsInChildren <Renderer> ();
			foreach(var r in renderers){
				r.enabled = false;
			}
		}

		huesosToValidate [0].SetActive (false);
		huesosToValidate [1].SetActive (false);

		for(int i = 0; i < 4; i++){
			options[i].SetActive (false);
		}

		nextButton.SetActive (false);

	}

	public void GetOptions(){
		for(int i = 1; i < 5; i++){
			options [i-1] = GameObject.Find ("Opcion" + i);
		}
	}

	public bool isUsed(int max, string value){
		for(int i = 0; i <= max; i++){
			if (string.Equals (opcion [i], value)) {
				return true;
			} else {
				return false;
			}
		}
		return false;
	}

}
