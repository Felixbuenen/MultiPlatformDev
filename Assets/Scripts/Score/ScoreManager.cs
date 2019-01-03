using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
  public static ScoreManager Instance;

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
  private bool isStopping; // true while StopSequence() coroutine is running

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

  private void Start()
  {
    scoreParentUI.SetActive(false);
  }

  private void StartScoreSequence(Trick trick)
  {
    activeSequence = true;

    currentScore = 0;
    currentTrickNum = 0;
    tricksInSequence = "";

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
    tricksInSequence += " " + trick.Name;
    trickSequenceText.text = tricksInSequence + ": " + currentScore;

    // update score multiplier text
    scoreMultiplierText.text = currentTrickNum + "X";
  }

  public void StopScoreSequence()
  {
    if (!activeSequence) return;

    StartCoroutine(StopScoreSequenceInternal());
  }

  private IEnumerator StopScoreSequenceInternal()
  {
    isStopping = true;

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
    isStopping = false;
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
