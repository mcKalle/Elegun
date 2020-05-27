using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class Projectile : MonoBehaviour
	{
		[HideInInspector] public float ShootingSpeed = 25f;

		private Vector3 maxMoveDistance;

		private void Start()
		{
			Destroy(gameObject, 3f);

			// reset the parent
			transform.parent = null;

			maxMoveDistance = GameObject.FindGameObjectWithTag("GunDistanceMarker").transform.position;
		}

		private void Update()
		{
			if (!(Vector2.Distance(transform.position, maxMoveDistance) < 0.01f))
			{
				Move();
			}
		}

		private void Move()
		{
			transform.position += (transform.up * -1) * Time.deltaTime * ShootingSpeed;
		}
	}
}
