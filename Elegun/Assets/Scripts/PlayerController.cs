using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerController : MonoBehaviour
	{
		private Camera _cam;

		private Vector2 _mousePos;

		private Rigidbody2D _rb;

		private PlayerInventory _inventory;

		// Start is called before the first frame update
		void Start()
		{
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
			RotatePlayer();
		}

		void RotatePlayer()
		{
			Vector2 lookDir = _mousePos - _rb.position;
			float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90;
			transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0,0,1));
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

	}
}
