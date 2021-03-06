﻿using System.Collections.Generic;
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
				// Projectile is spawned without parent to get the correct size
				projectile = Instantiate(ProjectilePrefab, gunPos.position, gunPos.rotation).GetComponent<Projectile>();
				projectile.ShootingSpeed = ShootingSpeed;
				// then the parent is set to the gun in order to rotate it along with the player (as long as the mouse button is held down)
				projectile.transform.SetParent(gunPos);

				ChangeProjectileColor(projectile.gameObject, inventory.SelectedMunitionIndex);
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
	}
}
