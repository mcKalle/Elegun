using Assets.Scripts.Data;
using Assets.Scripts.Global;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class ElementalShield : MonoBehaviour
	{
		[HideInInspector]
		public Element Element;
		[HideInInspector]
		public string PlayerId { get; set; }

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		void OnTriggerEnter2D(Collider2D col)
		{
			if (col.CompareTag(Constants.Tags.PROJECTILE))
			{
				var projectile = col.gameObject.GetComponent<Projectile>();

				if (projectile != null)
				{
					// ignore the collision, if the projectile is coming from the same player
					//if (projectile.PlayerId != PlayerId)
					//{
					Debug.Log($"The shield with the element { Element.Name } was hit by the projectile of element type { projectile.Element.Name }");

					// only destroy the shield, if it was hit by an effective element projectile
					if (projectile.Element.ElementId == Element.CounterPartElementId)
					{
						// destroy the shield
						Destroy(gameObject, 0.1f);
						// invoke event
						ShieldDestroyed?.Invoke(this, new ShieldDestroyedEventArgs(this));
						// destroy the projectile
						Destroy(col.gameObject, 0f);
					}
					else
					{
						// only destroy the projectile
						Destroy(col.gameObject, 0f);
					}
					//}
				}
			}
		}

		#region Events
		public event EventHandler<ShieldDestroyedEventArgs> ShieldDestroyed;

		public class ShieldDestroyedEventArgs : EventArgs
		{
			public ShieldDestroyedEventArgs(ElementalShield elementalShield)
			{
				ElementalShield = elementalShield;
			}

			public ElementalShield ElementalShield { get; set; }
		}
		#endregion
	}
}