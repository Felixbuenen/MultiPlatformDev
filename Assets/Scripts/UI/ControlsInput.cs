using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsInput : MonoBehaviour
{

	IInputSystem input;
	SceneLoader menuLoader;

	// Use this for initialization
	void Start()
	{
		ServiceManager.Singleton.RequestService<IInputSystem>(out input);
		ServiceManager.Singleton.RequestService<SceneLoader>(out menuLoader);
	}

	// Update is called once per frame
	void Update()
	{
		if (input.UIInput.GetBackButton())
		{
			LoadMenu();
		}
	}

	public void LoadMenu()
	{
		SceneManager.LoadSceneAsync(menuLoader.MenuName);
	}
}