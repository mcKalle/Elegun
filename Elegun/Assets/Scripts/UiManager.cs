using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class UiManager : MonoBehaviour
	{
		public Text moveSpeedText;

		public Text elementalSpeedText;

		public Text munitionItemCountText;

		// private and inspector-hidden fields
		PlayerMovement playerMovement;
		ElementalShieldRotator elementalShield;

		Image fireMunitionImage;
		TextMeshProUGUI fireMunitionInventoryCount;

		Image grassMunitionImage;
		TextMeshProUGUI grassMunitionInventoryCount;

		Image waterMunitionImage;
		TextMeshProUGUI waterMunitionInventoryCount;

		public static UiManager Instance { get; private set; }


		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Debug.Log("Warning: multiple " + this + " in scene!");
			}

			playerMovement = FindObjectOfType<PlayerMovement>();
			elementalShield = FindObjectOfType<ElementalShieldRotator>();

			// init all relevant UI controls
			fireMunitionImage = GameObject.FindGameObjectWithTag("fireMunitionImage").GetComponent<Image>();
			waterMunitionImage = GameObject.FindGameObjectWithTag("waterMunitionImage").GetComponent<Image>();
			grassMunitionImage = GameObject.FindGameObjectWithTag("grassMunitionImage").GetComponent<Image>();
			fireMunitionInventoryCount = GameObject.FindGameObjectWithTag("fireMunitionInventoryCount").GetComponent<TextMeshProUGUI>();
			grassMunitionInventoryCount = GameObject.FindGameObjectWithTag("grassMunitionInventoryCount").GetComponent<TextMeshProUGUI>();
			waterMunitionInventoryCount = GameObject.FindGameObjectWithTag("waterMunitionInventoryCount").GetComponent<TextMeshProUGUI>();
		}

		#region used for debugging and balance testing
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

		public void UpdateSpawnedMunitionCount(int count)
		{
			munitionItemCountText.text = string.Format("Spawned Munition: {0}", count);
		}
		#endregion
	}
}
