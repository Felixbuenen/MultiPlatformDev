using UnityEngine;
using UnityEngine.UI;

public class InputDebugger : MonoBehaviour
{
  // Update is called once per frame
  void Update()
  {
    /*    if (Input.GetButtonDown(""))
        {
          Debug.Log("X");
        }
    */
    if (Input.GetAxis("PS4_RightStickX") != 0)
    {
      //Debug.Log(Input.GetAxis("PS4_RightStickX"));
    }

    if (Input.GetAxis("PS4_RightStickY") != 0)
    {
      //Debug.Log(Input.GetAxis("PS4_RightStickY"));
    }

  }
}
