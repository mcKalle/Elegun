using System;
using Assets.Scripts.Data;
using UnityEngine;

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
			}
		}
	}
}
