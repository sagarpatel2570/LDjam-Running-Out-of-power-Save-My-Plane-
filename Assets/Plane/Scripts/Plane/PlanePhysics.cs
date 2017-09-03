using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlanePhysics : MonoBehaviour {

	public GameObject explositionEffect;
	public MeshRenderer planeVisual;
	public float force = 200;

	public AudioClip[] flightExplosion;
	public AudioClip energySound;

	public Transform visual;

	public event Action onDeath;
	public event Action bonusCallback;

	public bool isTestMode = false;

	PlaneEnergy energy;
	bool isDead;

	void Start () {
		
		onDeath += OnDeath;
		energy = GetComponentInParent<PlaneEnergy> ();
	}



	void OnCollisionEnter (Collision c){

		#if UNITY_EDITOR
			if (isTestMode) {
				return;
			}
		#endif
		
		if (onDeath != null) {
			onDeath ();
		}
		AudioSource.PlayClipAtPoint (flightExplosion [UnityEngine.Random.Range (0, flightExplosion.Length)], Camera.main.transform.position,30);

	

	}

	void OnTriggerEnter (Collider c){

		#if UNITY_EDITOR
			if (isTestMode) {
				return;
			}
		#endif

		AudioSource.PlayClipAtPoint (energySound, Camera.main.transform.position);
		energy.IncreaseEnergy (c.GetComponent<Energy> ().amount);
		c.gameObject.SetActive (false);

		if (visual.transform.eulerAngles.z > 40) {

			if (bonusCallback != null) {
				bonusCallback ();
			}


		}

	}

	void Update () {

		#if UNITY_EDITOR

		if (isTestMode) {
			GetComponent<Collider> ().isTrigger = true;
			return;
		} else {
			GetComponent<Collider> ().isTrigger = false;
		}
		#endif

		if (isDead) {
			Camera.main.transform.LookAt (this.transform);
		}

		if (energy.currentEnergyAmount <= 0) {
			if (onDeath != null) {
				onDeath ();
			}
		}
	}

	void OnDeath () {
		
		if (energy.currentEnergyAmount > 0) {
			GetComponent<Rigidbody> ().AddForce (force * UnityEngine.Random.onUnitSphere, ForceMode.Impulse);
			GetComponent<Rigidbody> ().AddTorque (force * UnityEngine.Random.onUnitSphere, ForceMode.Impulse);
		}
		explositionEffect.SetActive (true);
		planeVisual.enabled = false;
		isDead = true;
	}
}
