using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VuforiaManager : MonoBehaviour, ITrackableEventHandler {
	private TrackableBehaviour mTrackableBehaviour;
	ObjectsManager objectsManager;

	// Use this for initialization
	void Start () {
		objectsManager = GameObject.FindObjectOfType(typeof(ObjectsManager)) as ObjectsManager;
		mTrackableBehaviour = GetComponent<TrackableBehaviour> ();
		if(mTrackableBehaviour){
			mTrackableBehaviour.RegisterTrackableEventHandler (this);
		}
	}
	
	// Update is called once per frame
	public void OnTrackableStateChanged (TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus) {
		if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			objectsManager.trackableName = mTrackableBehaviour.TrackableName;
		} else {
			GameObject.Find ("Description").GetComponent<Canvas> ().enabled = false;
		}
	}
}
