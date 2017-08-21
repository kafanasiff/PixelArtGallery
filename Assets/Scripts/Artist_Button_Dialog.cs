// Project:			Pixel Art Gallery by Anachronic Designs
// Coder(s):		Kevin Afanasiff
// Last Updated:	Dec. 9th, 2016

/*
 * This script controls input for all artist buttons, and opens the appropriate artist dialog when a button is clicked.
 * */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Artist_Button_Dialog : MonoBehaviour {

	private Ray ray;
	private RaycastHit hit;

	public List<GameObject> artistDialogs;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		// Mouse input
		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_WEBPLAYER
		if (Input.GetMouseButtonUp(0)) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				GetInput(hit);
			}
		}
		#endif

		// Touch input
		#if UNITY_ANDROID || UNITY_IOS
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
			ray = Camera.main.ScreenPointToRay(Input.GetTouch (0).position);
			if (Physics.Raycast(ray, out hit)) {
				GetInput(hit);
			}
		}
		#endif
	}

	// Controls all artist buttons
	private void GetInput(RaycastHit hit) {
		string buttonName = hit.transform.gameObject.name;
		switch (buttonName) {
		case "Artist_Button_0":
			SelectArtist(0);
			break;
		case "Artist_Button_1":
			SelectArtist(1);
			break;
		case "Artist_Button_2":
			SelectArtist (2);
			break;
		default:
			Debug.Log ("Error - Button not recognized");
			break;
		}
	}

	// Display an artist dialog
	private void SelectArtist(int artistNumber) {
		//Debug.Log ("Artist" + artistNumber.ToString ());
		artistDialogs[artistNumber].SetActive (true);
		gameObject.SetActive(false);
	}

}
