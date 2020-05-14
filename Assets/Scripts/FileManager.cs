using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager : MonoBehaviour {

	private string filepath;

	// Use this for initialization
	void Start () {
	}

	public void CreateFile()
	{
		// check if file exists in Application.persistentDataPath
		filepath = Application.persistentDataPath + "/QRcodes.pdf";
		if (File.Exists (filepath)) {
			File.Delete (filepath);
		}

		if (!File.Exists (filepath)) {
			WWW loadDB = new WWW ("jar:file://" + Application.dataPath + "!/assets/QRcodes.pdf");
			while (!loadDB.isDone) {
			}
			// then save to Application.persistentDataPath
			File.WriteAllBytes (filepath, loadDB.bytes);
		}

		//open db connection
		Application.OpenURL(filepath);
		//connection = "URI=file:" + filepath;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
