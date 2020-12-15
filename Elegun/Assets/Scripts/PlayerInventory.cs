using System;
using System.Collections.Generic;
using Assets.Scripts.Data;
using UnityEngine;
using static Assets.Scripts.Shooting;

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

		public InventoryElement SelectedInventoryElement
		{
			get
			{
				return MunitionItems.Find(item => item.Element.ElementId == _selectedMunitionIndex);
			}
		}

		public List<InventoryElement> MunitionItems { get; private set; }

		public object PowerUp { get; private set; }

		private Shooting _shooting;

		// Start is called before the first frame update
		private void Start()
		{
			_shooting = FindObjectOfType<Shooting>();

			_shooting.ItemShot += ItemShotEvent; ;

			MunitionItems = new List<InventoryElement>();

			for (int i = 0; i < GameManager.Instance.Elements.GetLength(0); i++)
			{
				MunitionItems.Add(new InventoryElement(GameManager.Instance.Elements[i], InitialMunitionCount));
			}

			PowerUp = new object();
		}

		public void AddToInventory(Munition munition)
		{
			var inventoryElement = MunitionItems.Find(item => item.Element.ElementId == munition.elementId);
			if (inventoryElement != null)
			{
				inventoryElement.Count += munition.capacity;
			}

			InventoryUpdated?.Invoke(this, new InventoryUpdatedEventArgs(inventoryElement));

			// make sure to enable the loaded projectile 
			// (it won't be updated when the current item is 0 and it is collected
			if (!_shooting.LoadedProjectile.activeInHierarchy && munition.elementId == _selectedMunitionIndex)
			{
				_shooting.LoadedProjectile.SetActive(true);
				_shooting.ChangeProjectileColor(_selectedMunitionIndex);
			}
		}

		public bool ShootingWithSelectedMunitionPossible()
		{
			return SelectedInventoryElement.Count > 0;
		}


		private void ItemShotEvent(object sender, ItemShotEventArgs e)
		{
			var shotElement = MunitionItems.Find(item => item.Element.ElementId == e.Projectile.Element.ElementId);
			shotElement.Count -= 1;
			InventoryUpdated?.Invoke(this, new InventoryUpdatedEventArgs(shotElement));

			// if the player runs out of munition, deactivate the loaded projectile
			if (shotElement.Count == 0)
			{
				_shooting.LoadedProjectile.SetActive(false);
			}
		}

		#region Events
		public event EventHandler<InventoryUpdatedEventArgs> InventoryUpdated;

		public class InventoryUpdatedEventArgs : EventArgs
		{
			public InventoryUpdatedEventArgs(InventoryElement inventoryElement)
			{
				InventoryElement = inventoryElement;
			}

			public InventoryElement InventoryElement { get; set; }
		}
		#endregion
	}
}
