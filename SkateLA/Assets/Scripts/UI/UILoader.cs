using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoader : MonoBehaviour
{
	public GameObject[] AndroidButtons;
	public GameObject[] PS4Buttons;

	IInputSystem input;

	// Use this for initialization
	void Start()
	{
		ServiceManager.Singleton.RequestService<IInputSystem>(out input);

#if UNITY_EDITOR || UNITY_STANDALONE
		foreach (GameObject gameObject in AndroidButtons)
		{
			Destroy(gameObject);
		}
#else
		foreach (GameObject gameObject in PS4Buttons)
		{
			Destroy(gameObject);
		}
#endif
	}
}