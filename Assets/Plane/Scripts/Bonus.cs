using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour {

	public float scaleTime = 0.2f;
	public float fadeTime = 0.6f;

	Text text;


	void Start () {

		text = GetComponent<Text> ();
		transform.localScale = Vector3.one * 0.1f;

		StartCoroutine (Fade ());
	}



	IEnumerator Fade () {
		float time = 0;
		float speed = 1 / scaleTime;
		while (time < scaleTime) {
			time += Time.deltaTime;
			transform.localScale = Vector3.Lerp (Vector3.zero, Vector3.one, time * speed);
			yield return null;
		}

		time = 0;
		speed = 1 / fadeTime;

		while (time < scaleTime) {
			time += Time.deltaTime;
			text.color = Color.Lerp (Color.white, Color.clear, time * speed);
			yield return null;
		}

		Destroy (gameObject);
	}
}
