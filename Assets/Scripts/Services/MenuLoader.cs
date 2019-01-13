using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
	public string MenuName { get; private set; }
	public static MenuLoader Singleton;

	[SerializeField]
	private string pcMenuName;

	[SerializeField]
	private string androidMenuName;

	void Awake()
	{
		/*if (Singleton == null)
		{
			Singleton = this;
		}
		else if (Singleton != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);*/
	}

	// Use this for initialization
	void Start()
	{
		ServiceManager.Singleton.AddService<MenuLoader>(this);
#if UNITY_EDITOR || UNITY_STANDALONE
		MenuName = pcMenuName;
		SceneManager.LoadScene(string.Format("Scenes/{0}", pcMenuName));
#elif UNITY_ANDROID
		MenuName = androidMenuName;
		SceneManager.LoadScene(string.Format("Scenes/{0}", androidMenuName));
#endif
	}
}