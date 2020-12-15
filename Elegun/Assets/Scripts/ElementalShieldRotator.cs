using Assets.Scripts.Data;
using Assets.Scripts.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.ElementalShield;

namespace Assets.Scripts
{
	public class ElementalShieldRotator : MonoBehaviour
	{
		public GameObject ShieldPrefab;

		public float ShieldDistanceFromCenter = 1f;
		public float ElementalShieldMoveSpeed = 1.9f;

		[Range(3, 12)]
		public int ShieldCount = 6;

		private PlayerController player;

		// Start is called before the first frame update
		void Start()
		{
			player = transform.parent.GetComponentInChildren<PlayerController>();

			PlaceShieldObjects();
		}

		// Update is called once per frame
		void Update()
		{
			transform.Rotate(new Vector3(0, 0, 1), ElementalShieldMoveSpeed);
		}

		void PlaceShieldObjects()
		{
			for (int i = 0; i < ShieldCount; i++)
			{
				float angle = i * Mathf.PI * 2 / ShieldCount;
				float x = Mathf.Cos(angle) * ShieldDistanceFromCenter;
				float y = Mathf.Sin(angle) * ShieldDistanceFromCenter;
				Vector3 pos = transform.position + new Vector3(x, y, 0);
				float angleDegrees = -angle * Mathf.Rad2Deg;

				int elementIndex = i % 3;

				GameObject shieldGameObject = Instantiate(ShieldPrefab, pos, Quaternion.identity, transform);
				ElementalShield shield = shieldGameObject.GetComponent<ElementalShield>();

				// set the element in the shield
				shield.Element = GameManager.Instance.Elements[elementIndex];
				// render it
				shield.RenderShield();

				// assignment to player 
				// this is set because for the collision detection between shield and munition, we want to ignore our own shields
				shield.PlayerId = player.PlayerId;
				player.shieldCounts[shield.Element.ElementId]++;

				// update UI
				ShieldsUpdated?.Invoke(this, new ShieldUpdatedEventArgs(shield.Element));

				shield.ShieldDestroyed += ShieldDestroyedEvent;
			}
		}

		private void ShieldDestroyedEvent(object sender, ShieldDestroyedEventArgs e)
		{
			player.RemoveShield(e.ElementalShield);

			ShieldsUpdated?.Invoke(this, new ShieldUpdatedEventArgs(e.ElementalShield.Element));
		}

		#region Events
		public event EventHandler<ShieldUpdatedEventArgs> ShieldsUpdated;

		public class ShieldUpdatedEventArgs : EventArgs
		{
			public ShieldUpdatedEventArgs(Element element)
			{
				Element = element;
			}

			public Element Element { get; set; }
		}
		#endregion

		#region external control
		// only used for balance testing & prototyping
		public void UpdateSpeed(float newRotationSpeed)
		{
			ElementalShieldMoveSpeed = newRotationSpeed;
		}
		#endregion
	}
}
