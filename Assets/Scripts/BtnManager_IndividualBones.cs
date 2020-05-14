using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BtnManager_IndividualBones : MonoBehaviour {

	string nombre;
	string desc;
	string idHueso;
	string huesoName;
	DBManager dBManager;
	ObjectsManager objectsManager;
		
	// Use this for initialization
	void Start () {
		objectsManager = GameObject.FindObjectOfType(typeof(ObjectsManager)) as ObjectsManager;
		dBManager = GameObject.FindObjectOfType(typeof(DBManager)) as DBManager;
		dBManager.CreateDB("SkelAppDB.db");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		Debug.Log("Hola");
		huesoName = transform.name;

		var bones = GameObject.Find ("Bones"+objectsManager.trackableName);

		SkinnedMeshRenderer[] huesos;
		huesos = bones.GetComponentsInChildren<SkinnedMeshRenderer> ();

		foreach (SkinnedMeshRenderer hueso in huesos) {
				Debug.Log("Miguel_B: " + " hueso.name: " + hueso.name + " transform.name: " + transform.name);
			if (String.Equals (hueso.name, transform.name) && hueso.enabled) {
				int a = Convert.ToInt32 (huesoName);
				idHueso = huesoName;

				dBManager.OpenDB ();
				ArrayList result = dBManager.SingleSelectWhere ("Huesos", "*", "ID", "=", idHueso);
				if (result.Count > 0) {
					nombre = ((string[])result [0]) [1];
					desc = ((string[])result [0]) [2];
				}
					
				dBManager.CloseDB ();

				GameObject.Find ("name").GetComponent<Text> ().text = nombre;
				GameObject.Find ("desc").GetComponent<Text> ().text = desc;
				OcultarHuesos ();
				GameObject.Find ("Description").GetComponent<Canvas> ().enabled = true;
			}
		}
	}

	public void OcultarHuesos(){
		var mano = GameObject.Find (objectsManager.trackableName+"01");
		var bones = GameObject.Find ("Bones"+objectsManager.trackableName);

		SkinnedMeshRenderer[] huesos;
		huesos = bones.GetComponentsInChildren<SkinnedMeshRenderer> ();

		foreach(SkinnedMeshRenderer hueso in huesos){
			hueso.enabled = false;
		}

		SkinnedMeshRenderer[] objetos;
		objetos = mano.GetComponentsInChildren<SkinnedMeshRenderer>();

		foreach(SkinnedMeshRenderer objeto in objetos){
			if(!String.Equals(objeto.name,huesoName)){
				objeto.enabled = false;
			}
		}
	}

}
