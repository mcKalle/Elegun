using System.Collections.Generic;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerInventory : MonoBehaviour
	{
		public int InitialMunitionCount;

		private int _selectedMunitionIndex;

		public int SelectedMunitionIndex
		{
			get { return _selectedMunitionIndex; }
			set
			{
				_selectedMunitionIndex = value;
				if (_shooting != null)
				{
					_shooting.ChangeProjectileColor(_selectedMunitionIndex);
				}
			}
		}

		// key = elementId, value = count of elements
		[HideInInspector] public Dictionary<int, int> Inventory;

		public List<object> PowerUps { get; private set; }

		private Shooting _shooting;

		// Start is called before the first frame update
		private void Start()
		{
			_shooting = FindObjectOfType<Shooting>();

			Inventory = new Dictionary<int, int>();

			for (int i = 0; i < GameManager.Instance.Elements.GetLength(0); i++)
			{
				Inventory.Add(GameManager.Instance.Elements[i].ElementId, InitialMunitionCount);
			}

			PowerUps = new List<object>();

			//_uiManager.UpdateInventory(this);
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void AddToInventory(Munition munition)
		{
			Inventory[munition.elementId] += munition.capacity;
			Toolbar.Instance.UpdateIndex(munition.elementId, Inventory[munition.elementId]);
		}

	}
}
