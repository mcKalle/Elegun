using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts
{
	public class Toolbar : MonoBehaviour
	{
		public RectTransform Highlight;

		public InventorySlot[] InventorySlots;

		private int _slotIndex = 0;
		private PlayerInventory _playerInventory;

		private void Start()
		{
			_playerInventory = FindObjectOfType<PlayerInventory>();
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
	}
}
