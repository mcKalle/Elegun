using System.Linq;
using Assets.Scripts.Data;
using UnityEngine;
using static Assets.Scripts.PlayerInventory;

namespace Assets.Scripts
{
	public class Toolbar : MonoBehaviour
	{
		public static Toolbar Instance { get; private set; }

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

		public RectTransform Highlight;

		public InventorySlot[] InventorySlots;

		private int _slotIndex = 0;
		private PlayerInventory _playerInventory;

		private void Start()
		{
			_playerInventory = FindObjectOfType<PlayerInventory>();
			_playerInventory.InventoryUpdated += InventoryUpdatedEvent;
		}

		private void Update()
		{
			float scroll = Input.GetAxis("Mouse ScrollWheel");

			if (scroll != 0)
			{
				// move right
				if (scroll > 0)
				{
					_slotIndex--;
				}
				else
				{
					_slotIndex++;
				}

				if (_slotIndex > InventorySlots.Length - 1)
				{
					_slotIndex = 0;
				}

				if (_slotIndex < 0)
				{
					_slotIndex = InventorySlots.Length - 1;
				}

				Highlight.position = InventorySlots[_slotIndex].icon.transform.position;
				_playerInventory.SelectedMunitionIndex = InventorySlots[_slotIndex].itemID;
			}
		}

		private void UpdateIndex(int index, int value)
		{
			var inventorySlot = InventorySlots.ToList().FirstOrDefault(i => i.itemID == index);

			if (inventorySlot != null)
			{
				inventorySlot.text.text = value.ToString();
			}
		}

		#region EventHandling
		private void InventoryUpdatedEvent(object sender, InventoryUpdatedEventArgs e)
		{
			UpdateIndex(e.InventoryElement.Element.ElementId, e.InventoryElement.Count);
		}
		#endregion
	}
}
