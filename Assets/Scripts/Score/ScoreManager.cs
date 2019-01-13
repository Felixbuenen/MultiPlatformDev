using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
  public static ScoreManager Instance;

  public int TotalNumTricks { get; private set; }
  public int TotalScore { get; private set; }

  // UI elements
  public GameObject scoreParentUI;
  public Text scoreMultiplierText;
  public Text trickSequenceText;
  public Text finalScoreText;

  public float stopSequenceDelaySecs;

  private int currentScore;
  private int currentTrickNum;
  private string tricksInSequence;

  private bool activeSequence;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else if (Instance != this)
    {
      Destroy(gameObject);
    }
  }

  // reset stats when play scene is reloaded
  void OnSceneLoaded()
  {
    scoreParentUI.SetActive(false);

    TotalNumTricks = 0;
    TotalScore = 0;
  }

  private void StartScoreSequence(Trick trick)
  {
    activeSequence = true;

    currentScore = 0;
    currentTrickNum = 0;
    tricksInSequence = "";
    finalScoreText.text = "";

    scoreParentUI.SetActive(true);

    currentTrickNum = 1;
    scoreMultiplierText.text = currentTrickNum + "X";

    currentScore += trick.Score;
    tricksInSequence = trick.Name;
    trickSequenceText.text = tricksInSequence + ": " + currentScore;
  }

  public void AddTrickToSequence(Trick trick)
  {
    if (!activeSequence)
    {
      StartScoreSequence(trick);
      return;
    }
    // update data
    currentScore += trick.Score;
    currentTrickNum++;

    // update trick sequence + score text
    tricksInSequence += " + " + trick.Name;
    trickSequenceText.text = tricksInSequence + ": " + currentScore;

    // update score multiplier text
    scoreMultiplierText.text = currentTrickNum + "X";
  }

  public void StopScoreSequence()
  {
    if (!activeSequence) return;

    TotalNumTricks += currentTrickNum;
    TotalScore += currentScore * currentTrickNum;
    StartCoroutine(StopScoreSequenceInternal());
  }

  private IEnumerator StopScoreSequenceInternal()
  {
    finalScoreText.gameObject.SetActive(true);
    finalScoreText.text = (currentScore * currentTrickNum).ToString();

    activeSequence = false;

    for (float timer = stopSequenceDelaySecs; timer >= 0; timer -= Time.deltaTime)
    {
      if (activeSequence)
      {
        yield break;
      }

      yield return null;
    }

    finalScoreText.gameObject.SetActive(false);
    scoreParentUI.SetActive(false);
  }

  public void PlayerFell()
  {
    currentScore = 0;
    currentTrickNum = 0;
    tricksInSequence = "";

    activeSequence = false;

    scoreParentUI.SetActive(false);
  }
}