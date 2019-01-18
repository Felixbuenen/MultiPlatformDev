using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
  string gameSceneName;
  SceneLoader sceneLoader;

  private void Start()
  {
    gameSceneName = "Scenes/Main";
    ServiceManager.Singleton.RequestService<SceneLoader>(out sceneLoader);
  }

  public void StartGame()
  {
    SceneManager.LoadSceneAsync(gameSceneName);
  }

  public void ExitGame()
  {
    Application.Quit();
  }

  public void LoadControlsMenu()
  {
    SceneManager.LoadSceneAsync(sceneLoader.ControlsName);
  }

}