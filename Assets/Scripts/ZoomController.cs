// Project:			Pixel Art Gallery by Anachronic Designs
// Coder(s):		Kevin Afanasiff
// Last Updated:	Dec. 9th, 2016

/*
 * This script allows zooming the camera by pinching with 2 fingers on touch-screen devices, and the mouse scrollwheel
 * */

using UnityEngine;

public class ZoomController : MonoBehaviour {
	public float perspectiveSpeed = 0.5f;  // The rate of change of the field of view in perspective mode.
	public float orthographicSpeed = 0.5f; // The rate of change of the orthographic size in orthographic mode.
	
	// Called each frame
	void Update() {
		// Pinch to zoom on mobile devices
		#if UNITY_ANDROID || UNITY_IOS
		// If there are two touches on the device...
		if (Input.touchCount == 2) {

			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			// Find the position in the last frame of each touch
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
			// Find magnitude of the vector (distance) between touches in each frame
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			// If the camera is orthographic...
			if (GetComponent<Camera>().orthographic) {
				// ... change the orthographic size based on the change in distance between the touches.
				GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthographicSpeed;
				
				// Make sure the orthographic size never drops below zero.
				GetComponent<Camera>().orthographicSize = Mathf.Max(GetComponent<Camera>().orthographicSize, 0.1f);
			}
			// Esle if the camera is perspective...
			else {
				// Otherwise change the field of view based on the change in distance between the touches.
				GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveSpeed;
				
				// Clamp the field of view to make sure it's between 0 and 180.
				GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 0.1f, 179.9f);
			}
		}
		#endif

		// Mouse control
		#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE
		// Scroll forward
		if (Camera.main.orthographicSize > 1 && Input.GetAxis("Mouse ScrollWheel") > 0){
			Camera.main.orthographicSize = Camera.main.orthographicSize-1;
		}
		// Scrll backward
		if (Input.GetAxis("Mouse ScrollWheel") < 0) {
			Camera.main.orthographicSize = Camera.main.orthographicSize+1;
		}
		#endif
	}
}