using Assets.Scripts.Data;
using Assets.Scripts.Global;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class Projectile : MonoBehaviour
	{
		[HideInInspector] public float ShootingSpeed = 25f;
		[HideInInspector] public string PlayerId { get; set; }

		public Element Element { get; set; }

		private void Start()
		{
			Destroy(gameObject, 3f);

			// reset the parent
			transform.parent = null;
		}

		private void Update()
		{
			Move();
		}

		private void Move()
		{
			transform.position += (transform.up * -1) * Time.deltaTime * ShootingSpeed;
		}

	}
}
