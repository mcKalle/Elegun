using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
	public class Shooting : MonoBehaviour
	{
		public GameObject ProjectilePrefab;
		public GameObject LoadedProjectile;

		public float ShootingSpeed = 15f;

		// private and inspector-hidden fields
		private PlayerController _player;

		private Transform _gunPos;

		private void Start()
		{
			_gunPos = GetComponent<Transform>();

			_player = GetComponentInParent<PlayerController>();
		}

		private Projectile projectile;

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (!_player.IsCom)
				{
					Shoot();
				}
			}
		}

		private void ChangeProjectileColor(GameObject projectileObject, int elementId)
		{
			MeshRenderer meshRenderer = projectileObject.GetComponentsInChildren<MeshRenderer>()
				.FirstOrDefault(c => c.tag == "InnerProjectile");

			if (meshRenderer != null)
			{
				meshRenderer.material =
					GameManager.Instance.Elements[elementId].ElementBackgroundMaterial;
			}
		}

		public void ChangeProjectileColor(int elementId)
		{
			ChangeProjectileColor(gameObject, elementId);
		}

		/// <summary>
		/// The actual shooting of the projectiles.
		/// </summary>
		/// <param name="amountOfShotProjectiles">The amount controls how many projectiles are shot at once.</param>
		public void Shoot()
		{
			if (_player.Inventory.ShootingWithSelectedMunitionPossible())
			{
				// Projectile is spawned without parent to get the correct size
				projectile = Instantiate(ProjectilePrefab, _gunPos.position, _gunPos.rotation).GetComponent<Projectile>();
				projectile.Direction = projectile.transform.up;

				// this is set because for the collision detection between shield and munition, we want to ignore our own shields
				projectile.PlayerId = _player.PlayerId;

				projectile.Element = _player.Inventory.SelectedInventoryElement.Element;
				// set correct color for the projectile
				ChangeProjectileColor(projectile.gameObject, _player.Inventory.SelectedMunitionIndex);
				// send if off
				projectile.ShootingSpeed = ShootingSpeed;

				// invoke event
				ItemShot?.Invoke(this, new ItemShotEventArgs(projectile));
			}
		}

		#region Events
		public event EventHandler<ItemShotEventArgs> ItemShot;

		public class ItemShotEventArgs : EventArgs
		{
			public ItemShotEventArgs(Projectile projectile)
			{
				Projectile = projectile;
			}

			public Projectile Projectile { get; set; }
		}
		#endregion
	}
}
