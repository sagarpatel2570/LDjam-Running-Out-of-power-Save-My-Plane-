using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour {

	public float normalSpeed;


	[Header("Turn")]
	public float turnSpeed;
	public float turnAccleration;


	// turn local variable
	public float currentTurnAngle {get; protected set;}
	public float turnDelta {get; protected set;}

	public Vector2 input {get; protected set;}

	void Start () {
		GetComponentInChildren<PlanePhysics> ().onDeath += OnDeath;
	}
	
	void Update () {
		
		input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		SetTurnDelta ();
		SetTurnAngle ();
		UpdatePosition ();
		UpdateRotation ();
	}

	void SetTurnDelta () {
		turnDelta = Mathf.Lerp (turnDelta, input.x, turnAccleration * Time.deltaTime);
	}

	void SetTurnAngle () {
		currentTurnAngle += turnSpeed * turnDelta *  Time.deltaTime;
	}



	void UpdatePosition () {
		transform.position += transform.forward * normalSpeed *  Time.deltaTime;
	}

	void UpdateRotation () {
		transform.eulerAngles = new Vector3 (0, currentTurnAngle, 0);
	}

	void OnDeath () {
		this.enabled = false;
	}


}
