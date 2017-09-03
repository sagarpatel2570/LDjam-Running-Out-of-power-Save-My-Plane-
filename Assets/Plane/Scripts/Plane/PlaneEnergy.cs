using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaneEnergy : MonoBehaviour {

	public event Action<float> onEnergyChanged;

	public float maxEnergyAmount;
	public float rateAtWhichEnegyLoose;

	public float currentEnergyAmount { get; protected set;}

	void Start () {
		currentEnergyAmount = maxEnergyAmount;
		StartCoroutine (DecreaseEnergy ());
	}

	IEnumerator DecreaseEnergy () {

		while (currentEnergyAmount >= 0) {
			currentEnergyAmount -= Time.deltaTime * rateAtWhichEnegyLoose;
			if (onEnergyChanged != null) {
				onEnergyChanged (currentEnergyAmount/maxEnergyAmount);
			}
			yield return null;
		}
	}

	public void IncreaseEnergy (float amount) {
		currentEnergyAmount += amount;
		if (currentEnergyAmount >= maxEnergyAmount) {
			currentEnergyAmount = maxEnergyAmount;
		}
		if (onEnergyChanged != null) {
			onEnergyChanged (currentEnergyAmount/maxEnergyAmount);
		}
	}
	

}
