using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioClip flightMusic;


	AudioSource audioSource;

	void Start () {

		audioSource = GetComponent<AudioSource> ();
		audioSource.Play ();
	}


}
