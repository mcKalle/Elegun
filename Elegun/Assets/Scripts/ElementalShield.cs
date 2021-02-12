using Assets.Scripts.Data;
using Assets.Scripts.Global;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts
{
	public class ElementalShield : MonoBehaviour
	{
		[HideInInspector]
		public Element Element;
		[HideInInspector]
		public string PlayerId { get; set; }

		public SpriteRenderer mainSprite;
		public SpriteRenderer materialRenderer;

		/// <summary>
		/// Selects the correct sprite and sets the corresponding background effect.
		/// </summary>
		public void RenderShield()
		{
			// use the corresponding element sprite
			mainSprite.sprite = Element.ShieldSprite;

			// set the correct material for the shield
			materialRenderer.material = Element.ElementBackgroundMaterial;
		}

		private void Update()
		{
			transform.Rotate(new Vector3(0, 0, -1), 3f);
		}

		void OnTriggerEnter2D(Collider2D col)
		{
			if (col.CompareTag(Constants.Tags.PROJECTILE))
			{
				var projectile = col.gameObject.GetComponent<Projectile>();

				if (projectile != null)
				{
					// ignore the collision, if the projectile is coming from the same player
					if (projectile.PlayerId != PlayerId)
					{
						Debug.Log($"The shield with the element { Element.Name } was hit by the projectile of element type { projectile.Element.Name }");

						// only destroy the shield, if it was hit by an effective element projectile
						if (projectile.Element.ElementId == Element.SuperiorElementId)
						{
							// destroy the shield
							Destroy(gameObject, 0.1f);
							// invoke event
							ShieldDestroyed?.Invoke(this, new ShieldDestroyedEventArgs(this));
							// destroy the projectile
							Destroy(col.gameObject, 0f);
						}
						else if (projectile.Element.ElementId == Element.InferiorElementId)
						{
							// projectile will bounce off and can potentially harm the other players
							// set the player id to this player, so the projectile is as if was shot by the player
							projectile.PlayerId = PlayerId;
							// bounce it back
							projectile.Direction *= -1;
						}
						else
						{
							// only destroy the projectile
							Destroy(col.gameObject, 0f);
						}
					}
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