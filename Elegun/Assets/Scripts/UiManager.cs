using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class UiManager : MonoBehaviour
	{
		public Text moveSpeedText;

		public Text elementalSpeedText;

		PlayerMovement playerMovement;
		ElementalShield elementalShield;

		// Start is called before the first frame update
		void Start()
		{
			playerMovement = FindObjectOfType<PlayerMovement>();
			elementalShield = FindObjectOfType<ElementalShield>();
		}

		public void SetMoveSpeedSliderValue(float sliderValue)
		{
			moveSpeedText.text = sliderValue.ToString("0.00");
			if (playerMovement != null)
			{
				playerMovement.UpdateSpeed(sliderValue);
			}
		}

		public void SetElementalSpeedSliderValue(float sliderValue)
		{
			elementalSpeedText.text = sliderValue.ToString("0.000");
			if (elementalShield != null)
			{
				elementalShield.UpdateSpeed(sliderValue);
			}
		}
	}
}
