using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static float score;

	public Slider energySlider;
	public Image eneryColorImage;
	public Text scoreText;
	public GameObject gameOverText;
	public Canvas canvas;

	public GameObject mainMenu;
	public GameObject playAgain;
	public GameObject bonus;

	bool gameOver;
	float best = 0;


	void Start () {
		score = 0;
		GameObject.FindObjectOfType<PlanePhysics> ().onDeath += OnDeath; 
		GameObject.FindObjectOfType<PlanePhysics> ().bonusCallback += OnBonus; 
		GameObject.FindObjectOfType<PlaneEnergy> ().onEnergyChanged += OnEnergyChanged;

		gameOverText.SetActive (false);
		mainMenu.SetActive (false);
		playAgain.SetActive (false);
		scoreText.enabled = true;

		best = PlayerPrefs.GetFloat ("Score");
	}
	
	void Update () {
		if (!gameOver) {
			score += Time.deltaTime;
			scoreText.text = string.Format("{0:0.0}", score );
		}
	}

	void OnDeath () {
		gameOver = true;
		gameOverText.SetActive (true);
		mainMenu.SetActive (true);
		playAgain.SetActive (true);
		energySlider.gameObject.SetActive (false);

		gameOverText.GetComponent<Text> ().text = "GAMEOVER" + "\n" + string.Format ("{0:0.0}", score);
		scoreText.enabled = false;

		if (score > best) {
			PlayerPrefs.SetFloat ("Score", score);
		}
	}

	void OnBonus () {
		score += 20;
		Instantiate (bonus,canvas.transform);
	}

	void OnEnergyChanged (float amount) {
		amount = Mathf.Clamp01 (amount);
		energySlider.value = amount;
		eneryColorImage.color = Color.Lerp (Color.red, Color.green, amount);
	}

	public void LoadLevel (string name){
		SceneManager.LoadScene (name);
	}
}
