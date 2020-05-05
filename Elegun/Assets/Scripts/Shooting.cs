using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
	public class Shooting : MonoBehaviour
	{
		public GameObject ProjectilePrefab;
		public float ShootingSpeed = 15f;

		// private and inspector-hidden fields
		private PlayerInventory inventory;

		private Transform gunPos;

		private UiManager _uiManager;

		private void Start()
		{
			_uiManager = FindObjectOfType<UiManager>();
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
			}
			else if (Input.GetMouseButtonUp(0))
			{
				if (projectile != null)
				{
					projectile.Release();

					_uiManager.UpdateInventory(inventory);
				}
			}
		}
	}
}
