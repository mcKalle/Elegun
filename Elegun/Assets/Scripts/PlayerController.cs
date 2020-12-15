using Assets.Scripts.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerController : MonoBehaviour
	{
		public string PlayerId { get; private set; }

		// key: Element ID, value: count of shields
		public Dictionary<int, int> shieldCounts { get; private set; }


		public bool IsPlayerDead
		{
			get
			{
				bool result = false;

				if (shieldCounts != null)
				{
					result = shieldCounts.Values.ToList().TrueForAll(count => count <= 0);
				}

				return result;
			}
			private set { }
		}

		public bool IsCom;

		private Camera _cam;

		private Vector2 _mousePos;

		private Rigidbody2D _rb;

		private PlayerInventory _inventory;

		// Start is called before the first frame update
		void Start()
		{
			PlayerId = Guid.NewGuid().ToString();
			shieldCounts = new Dictionary<int, int>();
			foreach (var element in GameManager.Instance.Elements)
			{
				shieldCounts.Add(element.ElementId, 0);
			}


			_cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
			_rb = GetComponentInParent<Rigidbody2D>();

			_inventory = FindObjectOfType<PlayerInventory>();
		}

		// Update is called once per frame
		void Update()
		{
			_mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
		}

		void FixedUpdate()
		{
			if (!IsCom)
			{
				RotatePlayer();
			}
		}

		void RotatePlayer()
		{
			Vector2 lookDir = _mousePos - _rb.position;
			float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90;
			transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
		}


		// collision methods
		void OnTriggerEnter2D(Collider2D col)
		{
			if (col.tag == "Munition")
			{
				PickUpMunition(col.gameObject);
			}
		}

		private void PickUpMunition(GameObject munitionGameObject)
		{
			// add the element, that was picked up to the "inventory"
			Munition munition = munitionGameObject.GetComponent<Munition>();
			_inventory.AddToInventory(munition);

			Destroy(munitionGameObject);
		}

		public void RemoveShield(ElementalShield shield)
		{
			int currentCount = shieldCounts[shield.Element.ElementId];

			if (currentCount > 0)
			{
				shieldCounts[shield.Element.ElementId]--;
			}

			// check of dead
			if (IsPlayerDead)
			{
				PlayerDied?.Invoke(this, new PlayerDiedEventArgs(transform.parent.gameObject, this));
			}
		}

		#region Events
		public event EventHandler<PlayerDiedEventArgs> PlayerDied;

		public class PlayerDiedEventArgs : EventArgs
		{
			public PlayerDiedEventArgs(GameObject playerObject, PlayerController player)
			{
				PlayerObject = playerObject;
				Player = player;
			}

			public PlayerController Player { get; set; }
			public GameObject PlayerObject { get; set; }
		}
		#endregion
	}
}
