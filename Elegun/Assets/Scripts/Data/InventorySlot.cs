using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Data
{
	[System.Serializable]
	public class InventorySlot
	{
		public byte itemID;
		public Image icon;
		public TextMeshProUGUI text;
	}
}
