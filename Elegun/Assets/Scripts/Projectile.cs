using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class Projectile : MonoBehaviour
	{
		[HideInInspector] public float ShootingSpeed = 25f;

		private Vector2 target;

		private bool isReleased = false;

		private void Start()
		{
			
		}

		private void Update()
		{
			if (isReleased)
			{
				Move();
			}

			//if (Vector2.Distance(transform.position, target) < 0.01f)
			//{
			//	Destroy(gameObject, 1f);
			//}
		}

		public void Release()
		{
			Destroy(gameObject, 3f);

			// start the motion
			isReleased = true;

			// reset the parent
			transform.parent = null;

			// calculate the target of the projectile
			//var camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//target = camPos;
		}

		private void Move()
		{
			//old approach 
			//transform.position = Vector2.MoveTowards(transform.position, target, ShootingSpeed * Time.deltaTime);

			transform.position += (transform.up * -1) * Time.deltaTime * ShootingSpeed;
		}
	}
}
