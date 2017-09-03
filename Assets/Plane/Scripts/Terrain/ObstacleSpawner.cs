using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

	public int length = 500;
	public GameObject obstacle;
	[Range(0,1)]
	public float density;

	[Header("Volume of Obstacle Height/Width/Length")]
	public int maxHeight;
	public int minHeight;

	public int maxSize;
	public int minSize;

	public List<GameObject> blocks;

	[Header("Enegry Information")]
	public GameObject[] energy;
	public float radiusBetweenNoObjects;


	void Start () {

		foreach (GameObject block in blocks) {
			block.SetActive (false);
		}

		GenerateObstacles ();
		GenerateEnergy ();
	}



	void GenerateObstacles () {

		if (transform.position.x == 0 && transform.position.z == 0) {
			return;
		}

		int numberOfObstaclesActivated = 0;

		for (int i = 0; i < length; i++) {
			for (int j = 0; j < length; j++) {

				if (Random.value > density) {
					continue;
				}


				int obstacleHeight = Random.Range (minHeight, maxHeight);
				int obstacleWidth = Random.Range (minSize, maxSize);
				int obstacleLength = Random.Range (minSize, maxSize);

				Vector3 pos = new Vector3 (i * (length ) - (length * length) * 0.5f  + transform.position.x , obstacleHeight / 2, j * (length ) - (length * length) * 0.5f + transform.position.z );

				pos.x += Random.Range (-length, length);
				pos.z += Random.Range (-length, length);

				GameObject g = blocks [numberOfObstaclesActivated];
				g.SetActive (true);
				g.transform.position = pos;
				g.transform.rotation = Quaternion.Euler (new Vector3 (0, Random.Range (0, 360), 0));

				g.transform.localScale = new Vector3 (obstacleWidth / 2, obstacleHeight, obstacleLength / 2);
				g.transform.parent = this.transform;

				numberOfObstaclesActivated++;
				if (numberOfObstaclesActivated >= blocks.Count - 1) {
					break;
				}
			}
			if (numberOfObstaclesActivated >= blocks.Count - 1) {
				break;
			}
		}

	}

	int numberOfAttempts = 20;

	void GenerateEnergy () {
		int number = 0;
		int indexNo = 0;

		while (number <= numberOfAttempts) {

			if (indexNo > energy.Length -1) {
				break;
			}
			Vector3 pos = Random.onUnitSphere * (length * length) / 2 + transform.position;
			if (!Physics.CheckSphere (pos, radiusBetweenNoObjects)) {
				GameObject g = energy [indexNo];
				pos.y = 5;
				g.SetActive (true);
				g.transform.position = pos;
				g.GetComponent<Energy> ().gameObject.SetActive (true);
				g.GetComponentInChildren<ParticleSystem> ().Play ();
				indexNo++;
			}

			number++;
		}
	}

}
