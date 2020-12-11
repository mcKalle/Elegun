﻿using System;
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
		private PlayerInventory inventory;

		private Transform gunPos;

		private void Start()
		{
			gunPos = GetComponent<Transform>();
			inventory = FindObjectOfType<PlayerInventory>();
		}

		private Projectile projectile;

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Shoot();
			}
		}

		private void ChangeProjectileColor(GameObject projectileObject, int elementId)
		{
			MeshRenderer meshRenderer = projectileObject.GetComponentsInChildren<MeshRenderer>()
				.FirstOrDefault(c => c.tag == "InnerProjectile");

			if (meshRenderer != null)
			{
				meshRenderer.material =
					GameManager.Instance.Elements[elementId].ElementMaterial;
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
			if (inventory.ShootingWithSelectedMunitionPossible())
			{
				// Projectile is spawned without parent to get the correct size
				projectile = Instantiate(ProjectilePrefab, gunPos.position, gunPos.rotation).GetComponent<Projectile>();
				projectile.elementId = inventory.SelectedMunitionIndex;
				// set correct color for the projectile
				ChangeProjectileColor(projectile.gameObject, inventory.SelectedMunitionIndex);
				// send if off
				projectile.ShootingSpeed = ShootingSpeed;

				// invoke event
				ItemShot?.Invoke(this, new ItemShotEventArgs(projectile));
			}
		}

		#region EventHandling
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
