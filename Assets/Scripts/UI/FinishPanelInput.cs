using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPanelInput : MonoBehaviour
{
	IInputSystem input;

	// Use this for initialization
	void Start()
	{
		ServiceManager.Singleton.RequestService<IInputSystem>(out input);
	}

	// Update is called once per frame
	void Update()
	{

	}
}