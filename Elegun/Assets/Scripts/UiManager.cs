using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static Assets.Scripts.ElementalShieldRotator;

namespace Assets.Scripts
{
	public class UiManager : MonoBehaviour
	{
		public PlayerMovement playerMovement;
		public PlayerController playerController;
		public ElementalShieldRotator shieldRotator;

		public Text moveSpeedText;

		public Text elementalSpeedText;

		public Text munitionItemCountText;

		public Image redShieldImageControl;
		public Image greenShieldImageControl;
		public Image blueShieldImageControl;

		private Sprite[] greenShieldimages;
		private Sprite[] redShieldimages;
		private Sprite[] blueShieldimages;

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

			// init all relevant UI controls
			
			// munition images
			fireMunitionImage = GameObject.FindGameObjectWithTag("fireMunitionImage").GetComponent<Image>();
			waterMunitionImage = GameObject.FindGameObjectWithTag("waterMunitionImage").GetComponent<Image>();
			grassMunitionImage = GameObject.FindGameObjectWithTag("grassMunitionImage").GetComponent<Image>();
			fireMunitionInventoryCount = GameObject.FindGameObjectWithTag("fireMunitionInventoryCount").GetComponent<TextMeshProUGUI>();
			grassMunitionInventoryCount = GameObject.FindGameObjectWithTag("grassMunitionInventoryCount").GetComponent<TextMeshProUGUI>();
			waterMunitionInventoryCount = GameObject.FindGameObjectWithTag("waterMunitionInventoryCount").GetComponent<TextMeshProUGUI>();
			
			// shield images
			var shields = Resources.LoadAll<Sprite>("ShieldImages");
			greenShieldimages = new Sprite[shields.Length / 3];
			blueShieldimages = new Sprite[shields.Length / 3];
			redShieldimages = new Sprite[shields.Length / 3];

			foreach (var shield in shields)
			{
				string name = shield.name.Split('-')[0];
				int number = int.Parse(shield.name.Split('-')[1]);
				if (name.ToLower().Contains("green"))
				{
					greenShieldimages[number - 1] = shield;
				}
				else if (name.ToLower().Contains("blue"))
				{
					blueShieldimages[number - 1] = shield;
				}
				else if (name.ToLower().Contains("red"))
				{
					redShieldimages[number - 1] = shield;
				}
			}

			// bind to event of shield rotator to get updates
			shieldRotator.ShieldsUpdated += ShieldsUpdatedEvent; ;
		}

		private void ShieldsUpdatedEvent(object sender, ShieldUpdatedEventArgs e)
		{
			switch (e.Element.ElementId)
			{
				case 0:
					redShieldImageControl.sprite = redShieldimages[playerController.shieldCounts[e.Element.ElementId] - 1];
					break;
				case 1:
					greenShieldImageControl.sprite = greenShieldimages[playerController.shieldCounts[e.Element.ElementId] - 1];
					break;
				case 2:
					blueShieldImageControl.sprite = blueShieldimages[playerController.shieldCounts[e.Element.ElementId] - 1];
					break;
				default:
					break;
			}
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
			if (shieldRotator != null)
			{
				shieldRotator.UpdateSpeed(sliderValue);
			}
		}

		public void UpdateSpawnedMunitionCount(int count)
		{
			munitionItemCountText.text = string.Format("Spawned Munition: {0}", count);
		}
		#endregion
	}
}
