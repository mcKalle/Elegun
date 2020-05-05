using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class ElementalShield : MonoBehaviour
	{
		public GameObject ShieldPrefab;
	
		public float ShieldDistanceFromCenter = 1.2f;
		public float ElementalShieldMoveSpeed = 3f;

		[Range(2, 12)]
		public int ShieldCount = 9;

		// Start is called before the first frame update
		void Start()
		{
			PlaceShieldObjects();
		}

		// Update is called once per frame
		void Update()
		{
			transform.Rotate(new Vector3(0, 0, 1), ElementalShieldMoveSpeed);
		}


		void PlaceShieldObjects()
		{
			for (int i = 0; i < ShieldCount; i++)
			{
				float angle = i * Mathf.PI * 2 / ShieldCount;
				float x = Mathf.Cos(angle) * ShieldDistanceFromCenter;
				float y = Mathf.Sin(angle) * ShieldDistanceFromCenter;
				Vector3 pos = transform.position + new Vector3(x, y, 0);
				float angleDegrees = -angle * Mathf.Rad2Deg;
				Instantiate(ShieldPrefab, pos, Quaternion.identity, transform);
			}
		}


		#region external control
		// only used for balance testing & prototyping
		public void UpdateSpeed(float newRotationSpeed)
		{
			ElementalShieldMoveSpeed = newRotationSpeed;
		}
		#endregion
	}
}
