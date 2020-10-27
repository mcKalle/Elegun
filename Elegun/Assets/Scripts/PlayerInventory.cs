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
					if (ShootingWithSelectedMunitionPossible())
					{
						_shooting.LoadedProjectile.SetActive(true);
						_shooting.ChangeProjectileColor(_selectedMunitionIndex);
					}
					else
					{
						_shooting.LoadedProjectile.SetActive(false);
					}
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
		}

		public void AddToInventory(Munition munition)
		{
			Inventory[munition.elementId] += munition.capacity;
			Toolbar.Instance.UpdateIndex(munition.elementId, Inventory[munition.elementId]);
			// make sure to enable the loaded projectile 
			// (it won't be updated when the current item is 0 and it is collected
			if (!_shooting.LoadedProjectile.activeInHierarchy && munition.elementId == _selectedMunitionIndex)
			{
				_shooting.LoadedProjectile.SetActive(true);
				_shooting.ChangeProjectileColor(_selectedMunitionIndex);
			}
		}

		public void ReduceItemCapacity(int amount)
		{
			Inventory[SelectedMunitionIndex] -= amount;
			Toolbar.Instance.UpdateIndex(_selectedMunitionIndex, Inventory[SelectedMunitionIndex]);

			// if the player runs out of munition, deactivate the loaded projectile
			if (Inventory[SelectedMunitionIndex] == 0)
			{
				_shooting.LoadedProjectile.SetActive(false);
			}
		}

		public bool ShootingWithSelectedMunitionPossible()
		{
			return Inventory[_selectedMunitionIndex] > 0;
		}
	}
}
