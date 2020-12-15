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

		public float ShieldDistanceFromCenter = 1.2f;
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

				GameObject shieldGameObject = Instantiate(ShieldPrefab, pos, Quaternion.identity, transform);

				// distribute shield elements evenly
				SpriteRenderer spriteRenderer = shieldGameObject.GetComponentsInChildren<SpriteRenderer>().FirstOrDefault(g => g.tag == "BackgroundShader");

				if (spriteRenderer != null)
				{
					int elementIndex = i % 3;

					// set the correct material for the shield
					// TODO: move this to ElementalShield
					spriteRenderer.material = GameManager.Instance.Elements[elementIndex].ElementMaterial;

					ElementalShield shield = shieldGameObject.GetComponent<ElementalShield>();

					// this is set because for the collision detection between shield and munition, we want to ignore our own shields
					shield.PlayerId = player.PlayerId;

					// save the element in the shield as well
					shield.Element = GameManager.Instance.Elements[elementIndex];
					player.shieldCounts[shield.Element.ElementId]++;

					// update UI
					ShieldsUpdated?.Invoke(this, new ShieldUpdatedEventArgs(shield.Element));

					shield.ShieldDestroyed += ShieldDestroyedEvent;
				}
			}
		}

		private void ShieldDestroyedEvent(object sender, ShieldDestroyedEventArgs e)
		{
			int currentCount = player.shieldCounts[e.ElementalShield.Element.ElementId];

			if (currentCount > 0)
			{
				player.shieldCounts[e.ElementalShield.Element.ElementId]--;
			}

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
