using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryHandler : MonoBehaviour
{
	SceneLoader sceneLoader;

	// Use this for initialization
	void Start()
	{
		ServiceManager.Singleton.RequestService<SceneLoader>(out sceneLoader);
		SceneManager.LoadSceneAsync(sceneLoader.MenuName);
	}
}