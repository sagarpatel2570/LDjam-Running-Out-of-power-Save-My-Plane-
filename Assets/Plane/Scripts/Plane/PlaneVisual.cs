using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneVisual : MonoBehaviour {

	public Transform visual;

	[Space(10)]
	public float maxTilt;
	public float tiltAcceleration;
	public float maxTurn;
	public float turnAccleration;


	float currentTiltAngle;
	float currentTurnAngle;

	PlaneMovement planeMovement;

	void Start () {
		planeMovement = GetComponent<PlaneMovement> ();
		GetComponentInChildren<PlanePhysics> ().onDeath += OnDeath;
	}
	
	void Update () {
		SetTiltAngle ();
		SetTurnAngle ();
		SetRotaton ();
	}

	void SetTiltAngle () {
		currentTiltAngle = Mathf.Lerp (currentTiltAngle, maxTilt * -planeMovement.turnDelta, tiltAcceleration * Time.deltaTime);
	}

	void SetTurnAngle () {
		currentTurnAngle = Mathf.Lerp (currentTurnAngle, maxTurn * planeMovement.turnDelta, turnAccleration * Time.deltaTime);
	}

	void SetRotaton () {
		visual.localEulerAngles = new Vector3 (0, currentTurnAngle, currentTiltAngle);
	}

	void OnDeath () {
		this.enabled = false;
	}
}
