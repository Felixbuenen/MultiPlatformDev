using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
  string gameSceneName;

  private void Start()
  {
    gameSceneName = "Scenes/Main";
  }

  public void StartGame()
  {
    SceneManager.LoadSceneAsync(gameSceneName);
  }

  public void ExitGame()
  {
    Application.Quit();
  }

}