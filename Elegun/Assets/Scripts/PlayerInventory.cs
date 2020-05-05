using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerInventory : MonoBehaviour
	{

		public List<Munition> FireElementMunitions { get; private set; }
		public List<Munition> GrassElementMunitions { get; private set; }
		public List<Munition> WaterElementMunitions { get; private set; }

		public List<object> PowerUps { get; private set; }

		private UiManager _uiManager;

		// Start is called before the first frame update
		private void Start()
		{
			_uiManager = FindObjectOfType<UiManager>();

			FireElementMunitions = new List<Munition>();
			GrassElementMunitions = new List<Munition>();
			WaterElementMunitions = new List<Munition>();
			PowerUps = new List<object>();

			_uiManager.UpdateInventory(this);
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void AddToInventory(Munition munition)
		{
			switch (munition.element)
			{
				case "fire":
					FireElementMunitions.Add(munition);
					break;
				case "grass":
					GrassElementMunitions.Add(munition);
					break;
				case "water":
					WaterElementMunitions.Add(munition);
					break;
			}

			_uiManager.UpdateInventory(this);
		}

	}
}
