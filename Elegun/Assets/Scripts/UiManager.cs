using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
	public Slider moveSpeedSlider;
	public Text moveSpeedText;

	PlayerMovement playerMovement;

	// Start is called before the first frame update
	void Start()
	{
		playerMovement = FindObjectOfType<PlayerMovement>();
	}

	// Update is called once per frame
	void Update()
	{ 
		
	}

	public void SetSliderValue(float sliderValue)
	{
		moveSpeedText.text = sliderValue.ToString("0.00");
		if (playerMovement != null)
		{
			playerMovement.UpdateSpeed(sliderValue);
		}
	}
}
