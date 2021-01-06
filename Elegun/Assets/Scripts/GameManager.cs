using System;
using System.Collections;
using Assets.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.PlayerController;

namespace Assets.Scripts
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance { get; private set; }

		// Settings
		public Element[] Elements;

		[Header("Player Settings")]
		public float PlayerShieldRotationSpeed = 1.9f;
		public float PlayerMoveSpeed = 12f;

		[Header("Com Settings")]
		public float ComShieldRotationSpeed = 1.4f;
		public float ComMoveSpeed = 10f;

		public PlayerController mainPlayer;

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
		}

		// Start is called before the first frame update
		void Start()
		{
			InitSettings();
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void InitSettings()
		{
			var players = FindObjectsOfType<PlayerMovement>();
			foreach (var player in players)
			{
				player.moveSpeed = PlayerMoveSpeed;
				var shield = player.GetComponent<ElementalShieldRotator>();
				if (shield != null)
				{
					shield.ElementalShieldMoveSpeed = PlayerShieldRotationSpeed;
				}

				player.PlayerController.PlayerShieldsDestroyed += MainPlayerShieldsDestroyed;
				player.PlayerController.PlayerDied += MainPlayerDied;
			}

			var coms = FindObjectsOfType<ComMovement>();
			foreach (var com in coms)
			{
				com.moveSpeed = ComMoveSpeed;

				var shield = com.GetComponent<ElementalShieldRotator>();
				if (shield != null)
				{
					shield.ElementalShieldMoveSpeed = ComShieldRotationSpeed;
				}

				com.PlayerController.PlayerShieldsDestroyed += ComPlayerShieldsDestroyed;
				com.PlayerController.PlayerDied += ComPlayerDied;
			}

		}

		#region MainPlayerEvents
		private void MainPlayerShieldsDestroyed(object sender, PlayerShieldsDeactivatedEventArgs e)
		{
			Debug.Log("Main Player all Shields destroyed.");
			Destroy(e.Player.forceField.gameObject, 0f);

			ShowToast(e.PlayerObject.name + " ist jetzt verwundbar...", 1);
		}

		private void MainPlayerDied(object sender, PlayerDiedEventArgs e)
		{
			Debug.Log("Main Player dead.");
		}
		#endregion

		#region ComPlayerEvents
		private void ComPlayerShieldsDestroyed(object sender, PlayerShieldsDeactivatedEventArgs e)
		{
			Debug.Log(e.Player.name + " all Shields destroyed.");
			Destroy(e.Player.forceField.gameObject, 0f);

			ShowToast(e.PlayerObject.name + " ist jetzt verwundbar...", 1);
		}

		private void ComPlayerDied(object sender, PlayerDiedEventArgs e)
		{
			Destroy(e.PlayerObject, 0f);
			ShowToast(e.PlayerObject.name + " hat den Kampf verloren...", 1);
		}
		#endregion

		public Image deathPanel;
		public TextMeshProUGUI deathText;

		void ShowToast(string text,
			int duration)
		{
			StartCoroutine(showToastCOR(text, duration));
		}

		private IEnumerator showToastCOR(string text, int duration)
		{
			Color orginalColor = deathPanel.color;

			deathText.text = text;
			deathPanel.enabled = true;
			deathText.enabled = true;

			//Fade in
			yield return fadeInAndOut(deathPanel, true, 1f);

			//Wait for the duration
			float counter = 0;
			while (counter < duration)
			{
				counter += Time.deltaTime;
				yield return null;
			}

			//Fade out
			yield return fadeInAndOut(deathPanel, false, 4f);

			deathPanel.enabled = false;
			deathText.enabled = false;
			deathPanel.color = orginalColor;
		}

		IEnumerator fadeInAndOut(Image targetImage, bool fadeIn, float duration)
		{
			//Set Values depending on if fadeIn or fadeOut
			float a, b;
			if (fadeIn)
			{
				a = 0f;
				b = 1f;
			}
			else
			{
				a = 1f;
				b = 0f;
			}

			Color currentColor = Color.clear;
			float counter = 0f;

			while (counter < duration)
			{
				counter += Time.deltaTime;
				float alpha = Mathf.Lerp(a, b, counter / duration);

				targetImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
				yield return null;
			}
		}
	}
}
