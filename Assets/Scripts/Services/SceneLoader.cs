using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneLoader
{
	public string MenuName { get; protected set; }
	public string ControlsName { get; protected set; }
}