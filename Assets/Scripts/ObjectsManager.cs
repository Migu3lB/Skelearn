using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour {

	public string trackableName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowObjects(){
		var mano = GameObject.Find (trackableName+"01");
		var bones = GameObject.Find ("Bones"+trackableName);

		MeshRenderer[] huesos;
		huesos = bones.GetComponentsInChildren<MeshRenderer> ();

		foreach(MeshRenderer hueso in huesos){
			hueso.enabled = true;
		}

		MeshRenderer[] objetos;
		objetos = mano.GetComponentsInChildren<MeshRenderer>();

		foreach(MeshRenderer objeto in objetos){
			objeto.enabled = true;
		}

		GameObject.Find ("Description").GetComponent<Canvas> ().enabled = false;
	}
}
