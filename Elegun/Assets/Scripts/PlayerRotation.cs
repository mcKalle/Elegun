using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerRotation : MonoBehaviour
	{
		private Camera cam;

		private Vector2 mousePos;

		private Rigidbody2D rb;

		// Start is called before the first frame update
		void Start()
		{
			cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
			rb = GetComponentInParent<Rigidbody2D>();
		}

		// Update is called once per frame
		void Update()
		{
			mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		}

		void FixedUpdate()
		{
			RotatePlayer();
		}

		void RotatePlayer()
		{
			Vector2 lookDir = mousePos - rb.position;
			float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90;
			transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0,0,1));
		}
	}
}
