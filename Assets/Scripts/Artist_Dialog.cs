using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Artist_Dialog : MonoBehaviour {

	private Ray ray;
	private RaycastHit hit;

	public GameObject artistButtonDialog;
	public List<GameObject> images;
	private int currentImage = 0;
	private Vector3 originalCameraPosition;

	// Use this for initialization
	void Start () {
		originalCameraPosition = Camera.main.transform.position;
		images [currentImage].SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
		#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE
		if (Input.GetMouseButtonUp(0)) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			GetInput (ray);
		}

		else if (Input.GetMouseButton(0) && Input.touchCount == 0) {
			PanScreen();
		}
		#endif

		// Multi ball button
		#if UNITY_ANDROID || UNITY_IOS
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
			ray = Camera.main.ScreenPointToRay(Input.GetTouch (0).position);
			GetInput (ray);
		}

		else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			PanScreen ();
		}
		#endif
	}

	private void GetInput(Ray ray) {
		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform.gameObject.name == "Button_Back") {
				Debug.Log ("Back");
				artistButtonDialog.SetActive (true);
				gameObject.SetActive (false);
				ResetCamera();
			}
			if (hit.transform.gameObject.name == "Button_Left") {
				Debug.Log ("Left");
				PreviousImage ();
				ResetCamera();
			}
			if (hit.transform.gameObject.name == "Button_Right") {
				Debug.Log ("Right");
				NextImage ();
				ResetCamera();
			}
		} 
	}

	private void ResetCamera() {
		Camera.main.transform.position = originalCameraPosition;
		Camera.main.orthographicSize = 5;
	}

	private void PanScreen() {


		//Vector3 CameraPos;
		float speed = 0.1f;
		float MouseX;
		float MouseY;
		MouseX = Input.GetAxis("Mouse X");
		MouseY = Input.GetAxis("Mouse Y");

		//Debug.Log ("MouseX = " + MouseX.ToString ());
		#if UNITY_ANDROID || UNITY_IOS
		if (Input.touchCount > 0) {
			//Camera.main.backgroundColor = Color.white;

			Vector2 touchDeltaPosition = Input.touches[0].deltaPosition;
			MouseX = touchDeltaPosition.x;
			MouseY = touchDeltaPosition.y;
		}
		#endif


		Camera.main.transform.Translate (-MouseX * speed, -MouseY * speed, 0);
		/*
		CameraPos = new Vector3(-MouseX * speed, -MouseY * speed, 0);
		Camera.main.transform.position += CameraPos;
		*/

		/*
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform.gameObject.name == "TapAnywhere") {
				Debug.Log ("move camera");
				#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE
				Camera.main.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, Camera.main.transform.position.z);
				#endif
			} 
		}
		*/
	}

	private void PreviousImage() {
		images [currentImage].SetActive (false);
		currentImage--;
		if (currentImage < 0) {
			currentImage = images.Count - 1;
		}
		images [currentImage].SetActive (true);
	}

	private void NextImage() {
		images [currentImage].SetActive (false);
		currentImage++;
		if (currentImage >= images.Count) {
			currentImage = 0;
		}
		images [currentImage].SetActive (true);
	}

}
