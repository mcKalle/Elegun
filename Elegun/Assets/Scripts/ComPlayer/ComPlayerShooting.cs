using Assets.Scripts.Global;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
	public class ComPlayerShooting : MonoBehaviour
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
			// shoot must be done from AI
			// Shoot(1);
		}

		private void ChangeProjectileColor(GameObject projectileObject, int elementId)
		{
			MeshRenderer meshRenderer = projectileObject.GetComponentsInChildren<MeshRenderer>()
				.FirstOrDefault(c => c.tag == Constants.Tags.INNER_PROJ);

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
		private void Shoot(int amountOfShotProjectiles)
		{
			if (_player.Inventory.ShootingWithSelectedMunitionPossible())
			{
				// Projectile is spawned without parent to get the correct size
				projectile = Instantiate(ProjectilePrefab, _gunPos.position, _gunPos.rotation).GetComponent<Projectile>();
				// set correct color for the projectile
				ChangeProjectileColor(projectile.gameObject, _player.Inventory.SelectedMunitionIndex);
				// send if off
				projectile.ShootingSpeed = ShootingSpeed;
				// update the count of the items in the inventory
			}
		}


	}
}
