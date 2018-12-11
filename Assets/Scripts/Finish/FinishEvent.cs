using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishEvent : MonoBehaviour
{

  public Text finishText;

  void OnCollisionEnter(Collision c)
  {
    if (c.transform.tag == "Player")
    {
      StartCoroutine("FinishRoutine");
    }
  }

  IEnumerator FinishRoutine()
  {
    // show text
    finishText.enabled = true;
    yield return new WaitForSeconds(3f);
    finishText.enabled = false;

    // load scene again
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
