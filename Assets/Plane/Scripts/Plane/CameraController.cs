using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


	public Vector3 normalOffset;
	public Vector3 lookAtDistance;

	Vector3 currentAngle;
	PlaneMovement plane;

	void Start () {
		plane = GameObject.FindObjectOfType<PlaneMovement> ();
		GameObject.FindObjectOfType<PlanePhysics> ().onDeath += OnDeath;

		transform.position = plane.transform.position;
		transform.rotation = Quaternion.identity;

	}

	void LateUpdate () {

		UpdateCameraPos ();
		UpdateCameraRot ();
	}



	void UpdateCameraPos () {

		// get the horizontalforward and not normal forward direction of plane
		transform.position = plane.transform.position + Vector3.Cross(Vector3.up,-plane.transform.right) * normalOffset.z + Vector3.up * normalOffset.y;
	}

	void UpdateCameraRot () {
		transform.LookAt (plane.transform.position + plane.transform.forward * lookAtDistance.z);
	}

	void OnDeath () {
		this.enabled = false;
	}



}
