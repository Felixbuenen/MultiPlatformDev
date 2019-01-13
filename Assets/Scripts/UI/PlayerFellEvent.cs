using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFellEvent : MonoBehaviour
{
	private Transform panel;

	// Use this for initialization
	void Start()
	{
		Debug.Log("START CALLED");

		FinishEvent.OnInput += Unsubscribe;
		PlayerFallingState.PlayerFell += ShowPlayerFell;
		PlayerFallingState.PlayerRidingAgain += HidePlayerFell;

		panel = transform.GetChild(0);
	}

	void ShowPlayerFell()
	{
		panel.gameObject.SetActive(true);
	}

	void HidePlayerFell()
	{
		panel.gameObject.SetActive(false);
	}

	void Unsubscribe()
	{
		PlayerFallingState.PlayerFell -= ShowPlayerFell;
		PlayerFallingState.PlayerRidingAgain -= HidePlayerFell;
		FinishEvent.OnInput -= Unsubscribe;
	}
}