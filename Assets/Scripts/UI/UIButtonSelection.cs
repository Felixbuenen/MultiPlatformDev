using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonSelection : MonoBehaviour
{
	public Button[] buttons;

	private IInputSystem input;
	private int buttonIndex;
	private int maxButtons;

	// Use this for initialization
	void Start()
	{
		buttonIndex = 0;
		maxButtons = buttons.Length;
		ServiceManager.Singleton.RequestService<IInputSystem>(out input);

		buttons[buttonIndex].Select();
	}

	// Update is called once per frame
	void Update()
	{
		// up
		if (input.UIInput.GetUpButton() && buttonIndex > 0)
		{
			buttonIndex--;
			buttons[buttonIndex].Select();
		}
		// down
		if (input.UIInput.GetDownButton() && buttonIndex < maxButtons - 1)
		{
			buttonIndex++;
			buttons[buttonIndex].Select();
		}

		if (input.UIInput.GetPressButton())
		{
			Debug.Log("PRESSED BUTTON");
			buttons[buttonIndex].onClick.Invoke();
		}
	}
}