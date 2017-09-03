using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public InputField inputField;
	public CanvasGroup leftLeaderboard;
	public CanvasGroup rightLeaderboard;

	void Start () {
		leftLeaderboard.alpha = 0;
		rightLeaderboard.alpha = 0;

		inputField.text = "Player" + Random.Range (0, 999).ToString ();

		string name = PlayerPrefs.GetString ("Name",inputField.text);
		float score = PlayerPrefs.GetFloat ("Score");

		inputField.text = name;
		//Debug.Log (inputField.text + " " + name + " " + score);
		Highscores.AddNewHighscore (name, (int)score);


	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}


	public void Load (string name){
		SceneManager.LoadScene (name);
	}

	public void Quit () {
		Application.Quit ();
	}

	public void ShowLeaderboard () {



		leftLeaderboard.alpha = 1;
		rightLeaderboard.alpha = 1;
	}

	public void SetNameToPlayerpref () {
		PlayerPrefs.SetString ("Name", inputField.text);
	}
}
