using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishEvent : MonoBehaviour
{
  public static Action OnInput;

  public GameObject finishPanel;
  public ScoreManager scoreManager;
  public PlayerStateManager playerControl;
  public IInputSystem input;

  public Text totalScoreText;
  public Text totalNumTricksText;

  public float allowInputDelay;
  private float timer = 0f;
  private bool allowInput = false;

  private MenuLoader menuLoader;

  private void OnCollisionEnter(Collision c)
  {
    if (c.transform.tag == "Player")
    {
      StartCoroutine("FinishRoutine");
    }
  }

  private void Start()
  {
    ServiceManager.Singleton.RequestService<IInputSystem>(out input);
    ServiceManager.Singleton.RequestService<MenuLoader>(out menuLoader);
  }

  private void Update()
  {
    if (allowInput)
    {
      if (input.UIInput.GetBackButton())
      {
        // go to menu
        LoadMenu();
      }
      if (input.UIInput.GetPressButton())
      {
        // reload play scene
        Retry();
      }
    }
  }

  public void LoadMenu()
  {
    OnInput();
    SceneManager.LoadSceneAsync(menuLoader.MenuName);
  }

  public void Retry()
  {
    OnInput();
    SceneManager.LoadSceneAsync("Scenes/Main");
  }

  private IEnumerator FinishRoutine()
  {
    // show text
    finishPanel.SetActive(true);

    playerControl.enabled = false;
    totalScoreText.text = scoreManager.TotalScore.ToString();
    totalNumTricksText.text = scoreManager.TotalNumTricks.ToString();

    for (float timer = allowInputDelay; timer >= 0; timer -= Time.deltaTime)
    {
      yield return null;
    }

    // allow input
    allowInput = true;
  }
}