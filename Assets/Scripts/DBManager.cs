using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Text;
using Mono.Data.SqliteClient;
using UnityEngine.UI;
using System.Collections.Generic;

public class DBManager : MonoBehaviour {
	private string connection;
	private IDbConnection dbcon;
	private IDbCommand dbcmd;
	private IDataReader reader;
	private StringBuilder builder;
	private string filepath;

	// Use this for initialization
	void Start () {

	}

	public void CreateDB(string p)
	{
		// check if file exists in Application.persistentDataPath
		filepath = Application.persistentDataPath + "/" + p;
		if (File.Exists (filepath)) {
			File.Delete (filepath);
		}

		if (!File.Exists (filepath)) {
			WWW loadDB = new WWW ("jar:file://" + Application.dataPath + "!/assets/" + p);
			while (!loadDB.isDone) {
			}
			// then save to Application.persistentDataPath
			File.WriteAllBytes (filepath, loadDB.bytes);
		}

		//open db connection
		connection = "URI=file:" + filepath;
		dbcon = new SqliteConnection(connection);
		dbcon.Open();
	}

	public void OpenDB()
	{
		//open db connection
		connection = "URI=file:" + filepath;
		dbcon = new SqliteConnection(connection);
		dbcon.Open();
	}

	public void CloseDB(){
		reader.Close(); // clean everything up
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbcon.Close();
		dbcon = null;
	}

	public IDataReader BasicQuery(string query){ // run a basic Sqlite query
		dbcmd = dbcon.CreateCommand(); // create empty command
		dbcmd.CommandText = query; // fill the command
		reader = dbcmd.ExecuteReader(); // execute command which returns a reader
		return reader; // return the reader

	}

	public ArrayList SingleSelectWhere(string tableName , string itemToSelect,string wCol,string wPar, string wValue){ // Selects a single Item
		string query;
		query = "SELECT " + itemToSelect + " FROM " + tableName + " WHERE " + wCol + wPar + wValue;
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		//string[,] readArray = new string[reader, reader.FieldCount];
		string[] row = new string[reader.FieldCount];
		ArrayList readArray = new ArrayList();
		while(reader.Read()){
			int j=0;
			while(j < reader.FieldCount)
			{
				row[j] = reader.GetString(j);
				j++;
			}
			readArray.Add(row);
		}
		return readArray; // return matches
	}

	public Array MultipleSelectWhere(string tableName , string itemToSelect,string wCol,string wPar, string wValue){ // Selects a single Item
		string query;
		int fieldCount=0;
		List<string[]> rowList = new List<string[]> ();
		if (String.IsNullOrEmpty(wCol)) {
			query = "SELECT " + itemToSelect + " FROM " + tableName;
		} else {
			query = "SELECT " + itemToSelect + " FROM " + tableName + " WHERE " + wCol + wPar + wValue;
		}
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		while(reader.Read()){
			fieldCount = reader.FieldCount;
			string[] values = new string[fieldCount];
			int j = 0;
			while(j < fieldCount)
			{
				values[j] = reader.GetString(j);
				j++;
			}
			rowList.Add (values);
		}


		string[,] readArray = new string[rowList.Count, fieldCount];

		for(int i = 0; i < rowList.Count; i++){
			for(int x = 0; x < fieldCount; x++){
				readArray [i, x] = rowList [i] [x];
			}
		}

		return readArray; // return matches
	}

	// Update is called once per frame
	void Update () {

	}
}